using AppColoresDaltonicos.Models.Usuario;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppColoresDaltonicos.Models.Auht
{
    public class AuthResponseDto
    {
        public UsuarioResponseDto Usuario { get; set; }
        public string Result { get; set; }
    }
}
