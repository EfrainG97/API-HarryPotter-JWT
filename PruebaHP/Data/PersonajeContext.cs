using Microsoft.EntityFrameworkCore;
using PruebaHP.Model;

namespace PruebaHP.Data
{
    public class PersonajeContext : DbContext
    {
        public PersonajeContext(DbContextOptions<PersonajeContext> options) : base(options) { }

        public DbSet<Personaje> Personajes { get; set; }
    }
}
