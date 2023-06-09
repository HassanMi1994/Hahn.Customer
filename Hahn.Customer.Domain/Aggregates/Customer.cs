﻿using Hahn.Customer.Domain.Abstractions;
using Hahn.Customer.Domain.Exceptions;
using Hahn.Customer.CustomerDomain.Events;

namespace Hahn.Customer.Domain.Aggregates
{
    public class Customer : Entity, IAggregateRoot
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Email { get; private set; }
        public string BankAccountNumber { get; private set; }
        public bool IsDeleted { get; private set; }

        public Customer(string firstName, string lastName, DateTime dateOfBirth, string phoneNumber, string email, string bankAccountNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            PhoneNumber = phoneNumber;
            Email = email;
            BankAccountNumber = bankAccountNumber;

            ValidateModel();
            AddCustomerInitializedDomainEvent();
        }

        public override void ValidateModel()
        {
            if (DateOfBirth.Year < 1950 || DateOfBirth.Year > DateTime.UtcNow.Year)
                throw new CustomerDomainException($"Invalid {nameof(DateOfBirth)}: {DateOfBirth}");

            if (BankAccountNumber.Length < 10)
                throw new CustomerDomainException($"Invalid {nameof(BankAccountNumber)}:{BankAccountNumber}");
        }

        private void AddCustomerInitializedDomainEvent()
        {
            var customerInitializedDomainEvent = new CustomerInitializedDomainEvent(FirstName, LastName, DateOfBirth, PhoneNumber, Email, BankAccountNumber);
            AddDomainEvent(customerInitializedDomainEvent);
        }

        public void SetAsDeleted() => IsDeleted = true;

    }
}