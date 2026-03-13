using System.IdentityModel.Tokens.Jwt;

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

        public async Task<bool> IsTokenValidateAsync()
        {
            var token = await ObtenerTokenAsync();
            if (string.IsNullOrEmpty(token))
                return false;

            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);

                if (jwtToken.ValidTo > DateTime.UtcNow)
                {
                    return true;
                }
                else
                {
                    EliminarToken();
                    return false;
                }
            }
            catch
            {
                EliminarToken();
                return false;
            }
        }

    }
}
