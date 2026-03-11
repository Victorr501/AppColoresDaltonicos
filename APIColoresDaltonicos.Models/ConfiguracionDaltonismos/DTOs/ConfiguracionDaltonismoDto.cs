using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace APIColoresDaltonicos.Models.ConfiguracionDaltonismos.DTOs
{
    public class ConfiguracionDaltonismoDto
    {
        [Required(ErrorMessage = "El tipo de daltonismo es obligatoria")]
        public string TipoDaltonismo { get; set; }
        [Required(ErrorMessage = "La correcion de daltonismo es obligatoria")]
        public bool Correccion { get; set; }
    }
}
