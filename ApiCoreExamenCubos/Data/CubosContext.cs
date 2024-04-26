using ApiCoreExamenCubos.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCoreExamenCubos.Data
{
    public class CubosContext : DbContext
    {
        public CubosContext(DbContextOptions<CubosContext> options) : base(options) { }

        public DbSet<Cubo> Cubos { get; set; }
        public DbSet<CompraCubos> Compracubos { get; set; }
        public DbSet<UsuarioCubo> Usuarioscubos { get; set; }
    }
}
