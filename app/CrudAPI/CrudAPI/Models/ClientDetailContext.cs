using Microsoft.EntityFrameworkCore;

namespace CrudAPI.Models
{
    public class ClientDetailContext: DbContext
    {
        public ClientDetailContext(DbContextOptions<ClientDetailContext> options):base(options)
        {

        }

        public DbSet<ClientDetail> ClientDetail { get; set; }
    }
}
