
using APIColoresDaltonicos.Repositories.Repositories.Generic;
using APIColoresDaltonicos.Models.ConfiguracionDaltonismos;
using System;
using System.Collections.Generic;
using System.Text;

namespace APIColoresDaltonicos.Repositories.Repositories.ConfiguracionDaltonismos
{
    public interface IConfiguracionDaltonismoRepository : IGenericRepository<ConfiguracionDaltonismo>
    {
        Task<ConfiguracionDaltonismo> ObtenerConfiguracionPorUsuarioIdAsync(int usuarioId);
    }
}
