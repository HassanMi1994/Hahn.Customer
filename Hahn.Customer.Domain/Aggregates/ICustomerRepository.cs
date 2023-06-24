namespace Hahn.Customers.Domain.Aggregates
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Customer Add(Customer customer);
        void Update(Customer customer);
        Task<Customer> GetByIdAsync(int customerID);
        Task<bool> DeleteAsync(int customerId);
        Task<PaginationResponse<IEnumerable<Customer>>> GetCustomersAsync(int pageSize, int pageNumber,string search="");
    }
}
