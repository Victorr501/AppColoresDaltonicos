using System;
using System.Collections.Generic;
using System.Text;

namespace APIColoresDaltonicos.Repositories.Repositories.Generic
{
    public interface IGenericRepository<T> where T: class
    {
        Task<IEnumerable<T>> ObtenerTodosAsync();
        Task<T> ObtenerPorIdAsync(int id);
        Task AñadirAsync(T entidad);
        Task ActualizarAsync(T entidad);
        Task BorrarAsync(T entidad);
        Task GuardarCambiosAsync();
    }
}
