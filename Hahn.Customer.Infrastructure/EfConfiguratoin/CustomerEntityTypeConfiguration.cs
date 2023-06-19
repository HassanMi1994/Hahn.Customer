﻿using Hahn.Customers.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hahn.Customers.Infrastructure.EfConfiguratoin
{
    public class CustomerEntityTypeConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> customerConfiguration)
        {
            customerConfiguration.HasKey(b => b.Id);
            customerConfiguration.Property(b => b.Id).ValueGeneratedOnAdd();

            customerConfiguration.Property(x => x.FirstName).HasMaxLength(30);
            customerConfiguration.Property(x => x.LastName).HasMaxLength(30);

            customerConfiguration.Ignore(b => b.DomainEvents);

            customerConfiguration.HasIndex(x => x.Email).IsUnique();
            customerConfiguration.HasIndex(x => new { x.FirstName, x.LastName, x.DateOfBirth }).IsUnique();

            customerConfiguration.Property(x => x.Email).HasMaxLength(50);

            customerConfiguration.
                Property(x => x.PhoneNumber);
        }
    }
}