using Customer.Microservice.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Customer.Microservice.Controllers
{
    using Customer.Microservice.Models;

    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomersService _customersService;

        public CustomerController(CustomersService customerService) => _customersService = customerService;
        [HttpGet]
        public async Task<List<Customer>> Get() => await _customersService.GetCustomersAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Customer>> Get(string id)
        {
            var customers = await _customersService.GetCustomersAsync(id);
            if (customers == null)
                return NotFound();

            return Ok(customers);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Customer _customer)
        {
            await _customersService.CreaterCustomer(_customer);

            return CreatedAtAction(nameof(Get), new { id = _customer.Id }, _customer);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Put(string id, Customer _customer)
        {
            var customer = await _customersService.GetCustomersAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            customer.Id = _customer.Id;
            await _customersService.UpdateCustomer(id, _customer);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Del(string id)
        {
            var book = await _customersService.GetCustomersAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            await _customersService.DeleteCustomer(id);
            return NoContent();
        }
    }
}
