using System.ComponentModel.DataAnnotations;

namespace APIColoresDaltonicos.Models.Usuarios.DTOs
{
    public class CambiarPasswordDto
    {
        [Required(ErrorMessage = "La contraseña actual es obligatoria")]
        public string PasswordActual { get; set; }
        [Required(ErrorMessage = "La nueva contraseña es obligatoria")]
        public string PasswordNueva { get; set; }
    }
}
