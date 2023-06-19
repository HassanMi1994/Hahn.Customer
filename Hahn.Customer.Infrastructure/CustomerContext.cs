using Hahn.Customers.Infrastructure.EfConfiguratoin;
using Hahn.Customers.Domain.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Hahn.Customers.Domain.Aggregates;

namespace Hahn.Customers.Infrastructure
{
    public class CustomerContext : DbContext, IUnitOfWork
    {
        public string DbPath { get; }

        public CustomerContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "customers.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //  optionsBuilder.UseSqlite($"DataSource={DbPath}");
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Customer> Customers { get; set; }

        private readonly IMediator _mediator;
        private IDbContextTransaction _currentTransaction;

        public CustomerContext(DbContextOptions<CustomerContext> options) : base(options) { }

        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;

        public bool HasActiveTransaction => _currentTransaction != null;

        public CustomerContext(DbContextOptions<CustomerContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerEntityTypeConfiguration());
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await _mediator.DispatchDomainEventsAsync(this);
            await SaveChangesAsync();
            return true;
        }
    }
}

