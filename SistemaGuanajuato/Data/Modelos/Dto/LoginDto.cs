using System.ComponentModel.DataAnnotations;

namespace SistemaGuanajuato.Data.Modelos.Dto
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Debe ingresar su correo")]
        [EmailAddress(ErrorMessage = "Debe ingresar un correo válido")]
        public string Correo { get; set; }
        [Required(ErrorMessage = "Debe ingresar su contraseña")]
        public string Contrasena { get; set; }
    }
}
