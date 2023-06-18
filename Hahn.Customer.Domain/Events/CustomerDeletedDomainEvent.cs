using MediatR;

namespace Hahn.Customers.Domain.Events
{
    public class CustomerDeletedDomainEvent : INotification
    {
        public int Id { get; }
        public CustomerDeletedDomainEvent(int id)
        {
            Id = id;
        }
    }
}
