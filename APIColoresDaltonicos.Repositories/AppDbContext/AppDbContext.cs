using System;
using System.Collections.Generic;
using System.Text;
using APIColoresDaltonicos.Models.ConfiguracionDaltonismos;
using APIColoresDaltonicos.Models.Usuarios;
using Microsoft.EntityFrameworkCore;

namespace APIColoresDaltonicos.Repositories
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<ConfiguracionDaltonismo> ConfiguracionesDaltonismo { get; set; }
    }
}
