using Hahn.Customers.Domain.Abstractions;
using Hahn.Customers.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace Hahn.Customers.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public CustomerRepository(CustomerContext context)
        {
            _context = context;
        }

        public Customer Add(Customer customer)
        {
            return _context.Customers.Add(customer).Entity;
        }

        public async Task<bool> Delete(int customerId)
        {
            var customer = await GetAsync(customerId);
            _context.Remove(customer);
            return true;
        }

        public async Task<Customer> GetAsync(int customerId)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == customerId);
            if (customer == null)
            {
                customer = _context.Customers.Local.FirstOrDefault(x => x.Id == customerId);
            }
            return customer;
        }

        public void Update(Customer customer)
        {
            var attched = _context.Customers.Attach(customer);
            _context.Entry(attched).State = EntityState.Modified;
        }

        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            return await _context.Customers.Where(x => !x.IsDeleted).ToListAsync();
        }

        public Task<Customer> GetByIdAsync(int customerID)
        {
            return _context.Customers.Where(x => x.Id == customerID && !x.IsDeleted).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Soft delete the entity!
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public Task<bool> DeleteAsync(int customerId)
        {
          var customer=  _context.Customers.FirstOrDefault(x => x.Id == customerId);
            if (customer == null) return Task.FromResult(false);
            customer.SetAsDeleted();
            return Task.FromResult(true);
        }
    }
}
