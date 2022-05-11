using CrudAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CrudAPI.Services
{
    public class ClientServices
    {
        private readonly IMongoCollection<Client> _clientCollection;

        public ClientServices(IOptions<ClientDatabaseSettings> clientServices)
        {
            var mongoClient = new MongoClient(clientServices.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(clientServices.Value.DatabaseName);

            _clientCollection = mongoDatabase.GetCollection<Client>
                (clientServices.Value.ClientCollectionName);
        }

        public async Task<List<Client>> GetAsync() =>
            await _clientCollection.Find(x => true).ToListAsync();

        public async Task<Client> GetAsync(string id) =>
           await _clientCollection.Find(x => x.ClientId == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Client produto) =>
            await _clientCollection.InsertOneAsync(produto);

        public async Task UpdateAsync(string id, Client produto) =>
           await _clientCollection.ReplaceOneAsync(x => x.ClientId == id, produto);

        public async Task RemoveAsync(string id) =>
            await _clientCollection.DeleteOneAsync(x => x.ClientId == id);
    }
}
