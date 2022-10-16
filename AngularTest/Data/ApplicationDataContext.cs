using AngularTest.Models;
using Microsoft.EntityFrameworkCore;

namespace AngularTest.Data
{
    public class ApplicationDataContext : DbContext
    {
        public ApplicationDataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Contacto> Contactos { get; set; }
    }
}
