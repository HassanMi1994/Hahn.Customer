using MediatR;

namespace Hahn.Customers.CustomerDomain.Events
{
    public class CustomerUpdatedDomainEvent : INotification
    {
        public int Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public DateTime DateOfBirth { get; }
        public string PhoneNumber { get; }
        public string Email { get; }
        public string BankAccountNumber { get; }

        public CustomerUpdatedDomainEvent(int id, string firstName, string lastName, DateTime dateOfBirth, string email, string bankAccountNumber)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Email = email;
            BankAccountNumber = bankAccountNumber;
        }
    }
}
