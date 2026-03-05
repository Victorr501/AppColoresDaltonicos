using Microsoft.EntityFrameworkCore;

namespace APIColoresDaltonicos.Repositories.Repositories.Generic
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> ObtenerTodosAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> ObtenerPorIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AñadirAsync(T entidad)
        {
            await _dbSet.AddAsync(entidad);
        }

        public async Task ActualizarAsync(T entidad)
        {
            _dbSet.Update(entidad);
            await Task.CompletedTask;
        }

        public async Task BorrarAsync(T entidad)
        {
            _dbSet.Remove(entidad);
            await Task.CompletedTask;
        }

        public async Task GuardarCambiosAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
