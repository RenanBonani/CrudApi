using CrudAPI.Models;
using CrudAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrudAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ClientServices _clientServices;

        public ClientController(ClientServices clientServices)
        {
            _clientServices = clientServices;
        }

        [HttpGet]
        public async Task<List<Client>> GetClient()
            => await _clientServices.GetAsync();

        [HttpPost("")]
        public async Task<Client> PostClient(Client client)
        {
            await _clientServices.CreateAsync(client);

            return client;
        }

        //[HttpDelete("{id}")]
        //public async Task<Client> DeleteClient(string id)
        //{
        //    await _clientServices.RemoveAsync(id);

        //    return true;
        //}

        [HttpPut("{id}")]
        public async Task<Client> UpdateClient(string id, Client client)
        {
            await _clientServices.UpdateAsync(id, client);

            return client;
        }
    }
}
