using CrudAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CrudAPI.Services
{
    public class CustomerServices
    {
        private readonly IMongoCollection<Customer> _customerCollection;

        public CustomerServices(IOptions<CustomerDatabaseSettings> customerServices)
        {
            var mongoCustomer = new MongoClient(customerServices.Value.ConnectionString);
            var mongoDatabase = mongoCustomer.GetDatabase(customerServices.Value.DatabaseName);

            _customerCollection = mongoDatabase.GetCollection<Customer>
                (customerServices.Value.CustomerCollectionName);
        }

        public async Task<List<Customer>> GetAsync() =>
            await _customerCollection.Find(x => true).ToListAsync();

        public async Task<Customer> GetAsync(string id) =>
           await _customerCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Customer customer) =>
            await _customerCollection.InsertOneAsync(customer);

        public async Task UpdateAsync(string id, Customer customer) =>
           await _customerCollection.ReplaceOneAsync(x => x.Id == id, customer);

        public async Task RemoveAsync(string id) =>
            await _customerCollection.DeleteOneAsync(x => x.Id == id);
    }
}
