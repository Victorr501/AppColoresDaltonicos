using System;
using System.Collections.Generic;
using System.Text;
using APIColoresDaltonicos.Models.ConfiguracionDaltonismos;

namespace APIColoresDaltonicos.Models.Usuarios
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public ConfiguracionDaltonismo? ConfiguracionDaltonismo { get; set; }
    }
}
