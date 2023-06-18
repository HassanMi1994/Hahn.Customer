namespace Hahn.Customers.Domain.Aggregates
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Customer Add(Customer customer);
        void Update(Customer customer);
        Task<IEnumerable<Customer>> GetCustomersAsync();
        Task<Customer> GetByIdAsync(int customerID);
        Task<bool> DeleteAsync(int customerId);
    }
}
