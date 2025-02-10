using System.ComponentModel.DataAnnotations;

namespace SistemaGuanajuato.Data.Modelos.Dto
{
    public class ValidacionDto
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "El codigo es obligatorio")]
        [MaxLength(4)]
        [MinLength(4)]
        public string Codigo {  get; set; }

        [EmailAddress(ErrorMessage = "Debe ingresar un correo con dominio válido (ejemplo: @gmail, @outlook.com)")]
        public string? Correo { get; set; }
    }
}
