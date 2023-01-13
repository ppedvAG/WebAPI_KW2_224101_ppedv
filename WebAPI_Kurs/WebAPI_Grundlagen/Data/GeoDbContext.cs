using Microsoft.EntityFrameworkCore;
using WebAPI_Grundlagen.Models;

namespace WebAPI_Grundlagen.Data
{
    public class GeoDbContext : DbContext
    {
        public GeoDbContext(DbContextOptions<GeoDbContext> options)
            :base(options)
        {

        }

        public DbSet<Continent> Continents { get; set; }    
        public DbSet<Country> Countries { get; set; }
    }
}
