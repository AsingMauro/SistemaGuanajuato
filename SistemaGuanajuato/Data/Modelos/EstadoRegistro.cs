using System.ComponentModel.DataAnnotations;

namespace SistemaGuanajuato.Data.Modelos
{
    public class EstadoRegistro
    {
        [Key]
        public int Id_Estado { get; set; }
        public string Codigo { get; set; }
        public string Estado { get; set; }
    }
}
