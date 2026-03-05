using APIColoresDaltonicos.Models.Usuarios;
using APIColoresDaltonicos.Repositories.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Text;

namespace APIColoresDaltonicos.Repositories.Repositories.Usuarios
{
    public interface IUsuarioRepository : IGenericRepository<Usuario>
    {
        Task<Usuario?> ObtenerPorEmailAsync(string email);
    }
}
