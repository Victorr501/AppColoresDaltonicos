using System;
using System.Collections.Generic;
using System.Text;

namespace AppColoresDaltonicos.Services.Auth
{
    public interface IAuthService
    {
        Task GuardarTokenAsync(string token);
        Task<string?> ObtenerTokenAsync();
        void EliminarToken();
        Task<bool> IsTokenValidateAsync();
    }
}
