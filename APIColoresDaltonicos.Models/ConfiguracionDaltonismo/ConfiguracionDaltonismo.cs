using System;
using System.Collections.Generic;
using System.Text;

namespace APIColoresDaltonicos.Models
{
    public class ConfiguracionDaltonismo
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }

        // Tipo de daltonismo: Protanopia, Deuteranopia, Tritanopia, etc.
        public string TipoDaltonismo { get; set; }

        // Es la intesidad de la corrección, un valor entre 0 y 100
        public bool Correccion { get; set; }
        public Usuario? Usuario { get; set; }
    }
}
