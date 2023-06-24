using MediatR;

namespace Hahn.Customers.Domain.Events
{
    public class CustomerInitializedDomainEvent : INotification
    {
        public string FirstName { get; }
        public string LastName { get; }
        public DateTime DateOfBirth { get; }
        public string PhoneNumber { get; }
        public string Email { get; }
        public string BankAccountNumber { get; }

        public CustomerInitializedDomainEvent(string firstName, string lastName, DateTime dateOfBirth, string email, string bankAccountNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Email = email;
            BankAccountNumber = bankAccountNumber;
        }
    }
}
