using Hahn.Customers.Domain.Aggregates;
using Hahn.Customers.Infrastructure.CQRS.Commands;
using MediatR;

namespace Hahn.Customers.Infrastructure.CQRS.Commands.Handlers
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, bool>
    {
        private readonly ICustomerRepository _customerRepository;

        public DeleteCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<bool> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            await _customerRepository.DeleteAsync(request.Id);
            return await _customerRepository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}
