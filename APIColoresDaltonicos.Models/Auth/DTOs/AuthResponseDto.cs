using APIColoresDaltonicos.Models.Usuarios.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace APIColoresDaltonicos.Models.Auth.DTOs
{
    public class AuthResponseDto
    {
        public UsuarioResponseDto Usuario { get; set; }
        public string Token { get; set; }
    }
}
