using GalponIndustrial_API.Models;
using Microsoft.EntityFrameworkCore;

namespace GalponIndustrial_API.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Galpon> Galpons { get; set; }

        public DbSet<NumeroGalpon> NumeroGalpones { get; set; }
    }
}
