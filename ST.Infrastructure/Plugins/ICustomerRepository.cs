using ST.Core;
using ST.Shared.DTOs.Customer;

namespace ST.Infrastructure.Plugins
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<CustomerListDto>> GetCustomersAsync();
        Task<STCustomer> GetCustomerDetailsAsync(int id);
        Task AddCustomerAsync(CustomerAddDto customer);
        Task UpdateCustomerAsync(int id, STCustomer customer);
        Task DeleteCustomerAsync(int id);

        Task<bool> ExistsAsync();
    }
}