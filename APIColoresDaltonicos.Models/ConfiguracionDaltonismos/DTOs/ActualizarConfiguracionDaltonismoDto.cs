using System;
using System.Collections.Generic;
using System.Text;

namespace APIColoresDaltonicos.Models.ConfiguracionDaltonismos.DTOs
{
    public class ActualizarConfiguracionDaltonismoDto
    {
        public int UsuarioId { get; set; }
        public ConfiguracionDaltonismo Configuracion { get; set; }
    }
}
