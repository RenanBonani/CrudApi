using CrudAPI.Models;
using CrudAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrudAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerServices _customerServices;

        public CustomerController(CustomerServices customerServices)
        {
            _customerServices = customerServices;
        }

        [HttpGet]
        public async Task<ActionResult<List<Customer>>> GetClient()
            => await _customerServices.GetAsync();

        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer client)
        {
            await _customerServices.CreateAsync(client);

            return client;
        }        

        [HttpPut("{id}")]
        public async Task<ActionResult<Customer>> UpdateCustomer(string id, Customer updateCustomer)
        {
            var customer = await _customerServices.GetAsync(id);

            if (customer == null) 
            { 
                return NotFound(); 
            }

            updateCustomer.Id = customer.Id;

            await _customerServices.UpdateAsync(id, updateCustomer);

            return NoContent();
        }
        

        [HttpDelete("{id}")]
        public async Task<ActionResult<Customer>> DeleteCustomer(string id)
        {
            var costomer = await _customerServices.GetAsync(id);

            if (costomer is null)
            {
                return NotFound();
            }

            await _customerServices.RemoveAsync(id);

            return NoContent();
        }        
    }
}
