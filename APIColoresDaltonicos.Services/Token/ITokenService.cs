using APIColoresDaltonicos.Models.Usuarios;
using System;
using System.Collections.Generic;
using System.Text;

namespace APIColoresDaltonicos.Services.Token
{
    public interface ITokenService
    {
        string CrearToken(Usuario usuario);
    }
}
