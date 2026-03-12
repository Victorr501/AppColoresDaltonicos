using System;
using System.Collections.Generic;
using System.Text;

namespace AppColoresDaltonicos.Services.Auth
{
    public class AuthService : IAuthService
    {
        private const string TokenKey = "jwt_auth_token";

        public async Task GuardarTokenAsync(string token)
        {
            await SecureStorage.Default.SetAsync(TokenKey, token);
        }

        public async Task<string?> ObtenerTokenAsync()
        {
            return await SecureStorage.Default.GetAsync(TokenKey);
        }

        public void EliminarToken()
        {
            SecureStorage.Default.Remove(TokenKey);
        }
    }
}
