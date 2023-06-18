using MediatR;

namespace Hahn.Customers.Infrastructure.CQRS.Commands
{
    public class DeleteCustomerCommand : IRequest<bool>
    {
        public DeleteCustomerCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
