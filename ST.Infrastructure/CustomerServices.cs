using ST.Infrastructure.Plugins;
using ST.Shared.DTOs.Customer;
using ST.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST.Infrastructure
{
    public class CustomerServices : ICustomerServices
    {
        private readonly ICustomerRepository _repo;

        public CustomerServices(ICustomerRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<CustomerListDto>> GetCustomersAsync()
        {
            return await _repo.GetCustomersAsync();
        }

        public async Task AddCustomerAsync(CustomerAddDto customer)
        {
            await _repo.AddCustomerAsync(customer);
        }
    }
}
