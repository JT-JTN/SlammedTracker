using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ST.Core;
using ST.Infrastructure.Plugins;
using ST.Shared.DTOs.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST.DataAccess.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly STDataContext _context;
        private readonly ILogger<CustomerRepository> _logger;

        public CustomerRepository(STDataContext context, 
                                    ILogger<CustomerRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddCustomerAsync(CustomerAddDto customer)
        {
            var emailCheck = await _context.Customers.AnyAsync(c => c.EmailAddress == customer.EmailAddress);
            if (emailCheck.Equals(true))
            {
                _logger.LogInformation("Email address already exists: {customer.EmailAddress}", customer.EmailAddress);
                throw new Exception("Email address already exists");
            }

            var customerToAdd = new STCustomer
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                EmailAddress = customer.EmailAddress,
                IsBusinessCustomer = customer.IsBusinessCustomer
            };

            try
            {
                _logger.LogInformation("Starting addition of customer: {customerToAdd}", customerToAdd);
                await _context.Customers.AddAsync(customerToAdd);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Finished addition of customer: {customerToAdd}", customerToAdd);

                return;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogWarning("Failed to add customer: {ex.Message}", ex.Message);
                throw new Exception("Failed to add customer");
            }
        }

        public async Task DeleteCustomerAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Id out of range when calling update: {id}", id);
                throw new ArgumentOutOfRangeException(nameof(id), "Id must be greater than 0");
            }

            var customerToDelete = await _context.Customers.FindAsync(id);

            if (customerToDelete == null)
            {
                _logger.LogWarning("Customer to delete not found in database: {id}", id);
                throw new Exception("Customer not found");
            }

            try
            {
                _logger.LogInformation("Starting deletion of customer: {id}", id);
                _context.Customers.Remove(customerToDelete);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Finished deletion of customer: {id}", id);

                return;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogWarning("Deletion of customer: {id} failed", id);
                throw new Exception("Failed to delete customer");
            }
        }

        public async Task<bool> ExistsAsync()
        {
            var result = await _context.Customers.AnyAsync();
            return result;
        }

        public async Task<STCustomer> GetCustomerDetailsAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Id out of range when calling update: {id}", id);
                throw new ArgumentOutOfRangeException(nameof(id), "Id must be greater than 0");
            }

            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                _logger.LogWarning("Customer data invalid when calling update: {id} | {customer}", id, customer);
                throw new ArgumentOutOfRangeException(nameof(customer), "Customer data invalid");
            }

            return customer;
        }

        public async Task<IEnumerable<CustomerListDto>> GetCustomersAsync()
        {
            return await _context.Customers
                .Select(c => new CustomerListDto
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    EmailAddress = c.EmailAddress,
                    PhoneNumber = c.PhoneNumber,
                    IsBusinessCustomer = c.IsBusinessCustomer,
                    BusinessName = c.BusinessName
                })
                .ToListAsync();
        }

        public async Task UpdateCustomerAsync(int id, STCustomer customer)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Id out of range when calling update: {id}", id);
                throw new ArgumentOutOfRangeException(nameof(id), "Id must be greater than 0");
            }

            if (customer == null)
            {
                _logger.LogWarning("Customer data invalid when calling update: {id} | {customer}", id, customer);
                throw new ArgumentOutOfRangeException(nameof(customer), "Customer data invalid");
            }

            if (id != customer.Id)
            {
                _logger.LogWarning("Customer id mismatch when calling update: {id}, {customer.Id}", id, customer.Id);
                throw new Exception("Customer Id mismatch");
            }

            var customerToUpdate = await _context.Customers.FindAsync(id);

            if (customerToUpdate == null)
            {
                _logger.LogWarning("Customer to update not found in database: {id}", id);
                throw new Exception("Customer not found");
            }           

            if (customerToUpdate.EmailAddress != customer.EmailAddress)
            {
                var emailCheck = await _context.Customers.AnyAsync(c => c.EmailAddress == customer.EmailAddress);
                if (emailCheck.Equals(true))
                {
                    _logger.LogInformation("Email address already exists: {customer.EmailAddress}", customer.EmailAddress);
                    throw new Exception("Email address already exists");
                }
            }

            if (customerToUpdate.PhoneNumber != customer.PhoneNumber)
            {
                var phoneCheck = await _context.Customers.AnyAsync(c => c.PhoneNumber == customer.PhoneNumber);
                if (phoneCheck.Equals(true))
                {
                    _logger.LogInformation("Phone number already exists: {customer.PhoneNumber}", customer.PhoneNumber);
                    throw new Exception("Phone number already exists");
                }
            }

            if (customerToUpdate.BusinessName != customer.BusinessName)
            {
                var businessNameCheck = await _context.Customers.AnyAsync(c => c.BusinessName == customer.BusinessName);
                if (businessNameCheck.Equals(true))
                {
                    _logger.LogInformation("Business name already exists: {customer.BusinessName}", customer.BusinessName);
                    throw new Exception("Business name already exists");
                }
            }

            customerToUpdate.FirstName = customer.FirstName;
            customerToUpdate.LastName = customer.LastName;
            customerToUpdate.EmailAddress = customer.EmailAddress;
            customerToUpdate.PhoneNumber = customer.PhoneNumber;
            customerToUpdate.IsBusinessCustomer = customer.IsBusinessCustomer;

            if (customer.IsBusinessCustomer.Equals(false))
            {
                customerToUpdate.BusinessName = null!;
            }
            else
            {
                customerToUpdate.BusinessName = customer.BusinessName;
            }

            try
            {
                _logger.LogInformation("Starting update of customer: {id}", id);
                _context.Customers.Update(customerToUpdate);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Finished update of customer: {id}", id);
                return;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogWarning("Failed to update customer: {id}, {ex.Message}", id, ex.Message);
                throw new Exception("Failed to update customer");
            }
        }
    }
}
