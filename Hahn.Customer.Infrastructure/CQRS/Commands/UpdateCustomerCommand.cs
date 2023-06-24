using Hahn.Customers.Domain.Aggregates;
using MediatR;

namespace Hahn.Customers.Infrastructure.CQRS.Commands
{
    public class UpdateCustomerCommand : IRequest<Customer>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string BankAccountNumber { get; set; }

        public Customer ConvertToCustomer()
        {
            return new Customer(FirstName,LastName, DateOfBirth, Email, BankAccountNumber);
        }
    }
}
