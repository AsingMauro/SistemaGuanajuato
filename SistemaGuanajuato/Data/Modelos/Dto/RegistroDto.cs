using System.ComponentModel.DataAnnotations;

namespace SistemaGuanajuato.Data.Modelos.Dto
{
    public class RegistroDto
    {

        [Required(ErrorMessage = "Debe ingresar su nombre")]
        [MaxLength(60)]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "Debe ingresar su apellido paterno")]
        [MaxLength(70)]
        public string ApellidoPaterno { get; set; }

        [Required(ErrorMessage = "Debe ingresar su apellido materno")]
        [MaxLength(70)]
        public string ApellidoMaterno { get; set; }
        
        public DateTime FechaNacimiento { get; set; } = DateTime.Now;
        public string EstadoNacimiento { get; set; }

        [Required(ErrorMessage = "Debe ingresar el CURP")]
        [MaxLength(18)]
        [MinLength(18)]
        public string CURP { get; set; }

        [Required(ErrorMessage = "Debe ingresar un correo")]
        [EmailAddress(ErrorMessage = "Debe ingresar un correo con dominio válido (ejemplo: @gmail, @outlook.com)")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "Debe ingresar una contraseña")]
        public string Contrasena { get; set; }

        public int Codigo { get; set; }

    }
}
