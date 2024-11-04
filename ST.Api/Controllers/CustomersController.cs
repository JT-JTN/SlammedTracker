using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ST.Core;
using ST.Infrastructure.Plugins;
using ST.Shared.DTOs.Customer;

namespace ST.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ILogger<CustomersController> _logger;
        private readonly ICustomerRepository _repo;

        public CustomersController(ILogger<CustomersController> logger, 
                                    ICustomerRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _repo.GetCustomersAsync();
            if (customers == null || !customers.Any())
            {
                _logger.LogWarning("No Customers Found In Database");
                return NotFound();
            }
            _logger.LogInformation("Customers retrieved successfully");
            return Ok(customers);
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomer([FromBody] CustomerAddDto customer)
        {
            if (!ModelState.IsValid)
                return BadRequest("Bad customer data.");

            await _repo.AddCustomerAsync(customer);
            return NoContent();
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> CustomerDetails(int id)
        {
            var customer = await _repo.GetCustomerDetailsAsync(id);
            if (customer == null)
            {
                _logger.LogWarning("Customer not found in database");
                return NotFound();
            }
            _logger.LogInformation("Customer retrieved successfully");
            return Ok(customer);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] STCustomer customer)
        {
            if (!ModelState.IsValid)
                return BadRequest("Bad customer data.");

            await _repo.UpdateCustomerAsync(id, customer);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest("Bad customer data.");

            await _repo.DeleteCustomerAsync(id);
            return NoContent();
        }
    }
}
