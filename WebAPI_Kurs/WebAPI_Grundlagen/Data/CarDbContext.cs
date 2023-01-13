using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI_Grundlagen.Models;

namespace WebAPI_Grundlagen.Data
{
    public class CarDbContext : DbContext
    {
        public CarDbContext (DbContextOptions<CarDbContext> options)
            : base(options)
        {
        }

        public DbSet<WebAPI_Grundlagen.Models.Car> Cars { get; set; } = default!;
    }
}
