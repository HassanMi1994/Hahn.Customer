using Hahn.Customers.Domain;
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

        public async Task<PaginationResponse<IEnumerable<Customer>>> GetCustomersAsync(int pageSize, int pageNumber, string search = "")
        {
            var result = new PaginationResponse<IEnumerable<Customer>>();
            var data = _context.Customers.Where(x => !x.IsDeleted);
            if (!string.IsNullOrEmpty(search))
            {
                data = data.Where(x =>
                x.FirstName.ToLower().Contains(search.ToLower()) ||
                x.LastName.ToLower().Contains(search.ToLower()) ||
                x.BankAccountNumber.ToLower().Contains(search.ToLower()) ||
                x.Email.ToLower().Contains(search.ToLower()));
            }
            result.Data = data
                .OrderByDescending(x => x.CreatedAt)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize);
            result.TotalSize = await data.CountAsync();
            return result;
        }

        public async Task<(bool exist, string emailOwner)> DoesCustomerEmailExistAsync(Customer customer)
        {
            var exisit = await _context.Customers.FirstOrDefaultAsync(x => x.Email.ToLower() == customer.Email.ToLower() && x.Id != customer.Id);
            return (exisit != null, $"{exisit?.FirstName} {exisit?.LastName}");
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
            var customer = _context.Customers.FirstOrDefault(x => x.Id == customerId);
            if (customer == null) return Task.FromResult(false);
            customer.SetAsDeleted();
            return Task.FromResult(true);
        }
    }
}
