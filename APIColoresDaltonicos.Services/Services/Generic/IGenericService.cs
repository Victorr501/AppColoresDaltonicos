using System;
using System.Collections.Generic;
using System.Text;

namespace APIColoresDaltonicos.Services.Services.Generic
{
    public interface IGenericService<T> where T : class
    {
        Task<IEnumerable<T>> ObtenerTodosAsync();
        Task<T> ObtenerPorIdAsync(int id);
        Task<T> AñadirAsync(T entidad);
        Task ActualizarAsync(T entidad);
        Task BorrarAsync(T entidad);
    }
}
