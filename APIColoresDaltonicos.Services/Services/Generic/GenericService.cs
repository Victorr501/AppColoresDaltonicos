using APIColoresDaltonicos.Repositories.Repositories.Generic;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace APIColoresDaltonicos.Services.Services.Generic
{
    public class GenericService<T> : IGenericService<T> where T : class
    {
        protected readonly IGenericRepository<T> _repository;
        protected readonly ILogger _logger;

        public GenericService(IGenericRepository<T> repository, ILogger<GenericService<T>> logger) 
        {
            _repository = repository;
            _logger = logger;
        }

        public virtual async Task<IEnumerable<T>> ObtenerTodosAsync()
        {
            return await _repository.ObtenerTodosAsync();
        }

        public virtual async Task<T> ObtenerPorIdAsync(int id)
        {
            return await _repository.ObtenerPorIdAsync(id);
        }

        public virtual async Task<T> AñadirAsync(T entidad)
        {
            await _repository.AñadirAsync(entidad);
            await _repository.GuardarCambiosAsync();
            return entidad;
        }

        public virtual async Task ActualizarAsync(T entidad)
        {
            await _repository.ActualizarAsync(entidad);
            await _repository.GuardarCambiosAsync();
        }

        public virtual async Task BorrarAsync(T entidad)
        {
            await _repository.BorrarAsync(entidad);
            await _repository.GuardarCambiosAsync();
        }
    }
}
