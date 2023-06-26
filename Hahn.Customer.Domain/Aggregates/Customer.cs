using Hahn.Customers.CustomerDomain.Events;

namespace Hahn.Customers.Domain.Aggregates
{
    public class Customer : Entity, IAggregateRoot
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public string Email { get; private set; }
        public string BankAccountNumber { get; private set; }
        public bool IsDeleted { get; private set; }

        public Customer(string firstName, string lastName, DateTime dateOfBirth, string email, string bankAccountNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Email = email;
            BankAccountNumber = bankAccountNumber;

            ValidateModel();
            AddCustomerInitializedDomainEvent();
        }

        public void UpdateValues(int id, string firstName, string lastName, DateTime dateOfBirth, string phoneNumber, string email, string bankAccountNumber)
        {
            base.Id = id;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Email = email;
            BankAccountNumber = bankAccountNumber;
            ValidateModel();
            AddCustomerUpdatedDomainEvent();
        }

        public override void ValidateModel()
        {
            if (DateOfBirth.Year < 1950 || DateOfBirth.Year > DateTime.UtcNow.Year)
                throw new CustomerDomainException($"Invalid {nameof(DateOfBirth)}: {DateOfBirth}");

            if (BankAccountNumber.Length < 10)
                throw new CustomerDomainException($"Invalid {nameof(BankAccountNumber)}:{BankAccountNumber}");
        }

        private void AddCustomerInitializedDomainEvent()
        {
            var customerInitializedDomainEvent = new CustomerCreatedDomainEvent(Id,FirstName, LastName, DateOfBirth, Email, BankAccountNumber);
            AddDomainEvent(customerInitializedDomainEvent);
        }

        private void AddCustomerUpdatedDomainEvent()
        {
            var cu = new CustomerUpdatedDomainEvent(Id, FirstName, LastName, DateOfBirth, Email, BankAccountNumber);
            AddDomainEvent(cu);
        }

        public void SetAsDeleted()
        {
            IsDeleted = true;
            AddDomainEvent(new CustomerDeletedDomainEvent(Id));
        }

    }
}
