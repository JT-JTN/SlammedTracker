using ST.Shared.DTOs.Customer;

namespace ST.Infrastructure
{
    public interface ICustomerServices
    {
        Task<IEnumerable<CustomerListDto>> GetCustomersAsync();
        Task AddCustomerAsync(CustomerAddDto customer);
    }
}