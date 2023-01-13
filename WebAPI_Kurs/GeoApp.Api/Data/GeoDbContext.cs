using GeoApp.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using GeoApp.Shared.DTO;

namespace GeoApp.Api.Data
{

    //Wir verwenden Entity Framework Core mit dem 'Code First' - Ansatz
    //Was ist Code First?
    //Wir beschreiben das Datenbank-Schema in C# - Klassen (auch POCOS oder Entities genannt) 
    public class GeoDbContext : DbContext
    {
        public GeoDbContext(DbContextOptions<GeoDbContext> dbContextOptions)
            :base(dbContextOptions)
        {
        }

        public DbSet<Continent> Continents { get; set; }    
        public DbSet<Country> Countries { get; set; }
    }
}
