using APIColoresDaltonicos.Repositories.Repositories.Generic;
using APIColoresDaltonicos.Models.ConfiguracionDaltonismos;
using APIColoresDaltonicos.Models.ConfiguracionDaltonismos;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace APIColoresDaltonicos.Repositories.Repositories.ConfiguracionDaltonismos
{
    public class ConfiguracionDaltonismoRepository : GenericRepository<ConfiguracionDaltonismo>, IConfiguracionDaltonismoRepository
    {
        public ConfiguracionDaltonismoRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<ConfiguracionDaltonismo> ObtenerConfiguracionPorUsuarioIdAsync(int usuarioId)
        {
            return await _dbSet.FirstOrDefaultAsync(c => c.UsuarioId == usuarioId);
        }
    }
}
