using APIColoresDaltonicos.Models.Usuarios;
using APIColoresDaltonicos.Repositories.Repositories.Generic;
using APIColoresDaltonicos.Repositories.Repositories.Usuarios;
using Microsoft.EntityFrameworkCore;


namespace APIColoresDaltonicos.Repositories.Repositories.Usuarios
{
    public class UsuarioRepository : GenericRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(AppDbContext context) : base(context) 
        {
        }

        public async Task<Usuario?> ObtenerPorEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
