using System.ComponentModel.DataAnnotations;

namespace SistemaGuanajuato.Data.Modelos.Dto
{
    public class RespuestaDto
    {
        public int Id { get; set; }
        public bool isSuccess { get; set; }
        public string? Respuesta { get; set; }
    }
}
