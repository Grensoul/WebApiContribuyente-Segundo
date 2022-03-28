using Microsoft.EntityFrameworkCore;
using WebApiContribuyente_Segundo.Entidades;

namespace WebApiContribuyente_Segundo
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Contribuyente> Contribuyentes { get; set; }
        public DbSet<Declaracion> Declaraciones { get; set; }
    }
}
