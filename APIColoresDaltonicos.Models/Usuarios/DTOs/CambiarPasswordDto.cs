using System.ComponentModel.DataAnnotations;

namespace APIColoresDaltonicos.Models.Usuarios.DTOs
{
    public class CambiarPasswordDto
    {
        [Required(ErrorMessage = "La contraseña actual es obligatoria")]
        public string PasswordActual { get; set; }
        [Required(ErrorMessage = "La nueva contraseña es obligatoria")]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        public string PasswordNueva { get; set; }
    }
}
