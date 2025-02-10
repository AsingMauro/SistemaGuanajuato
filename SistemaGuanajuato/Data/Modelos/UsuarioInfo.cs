using System.ComponentModel.DataAnnotations;

namespace SistemaGuanajuato.Data.Modelos
{
    public class UsuarioInfo
    {
        [Key]
        public int Id_Usu { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string EstadoNacimiento { get; set; }
        public string CURP { get; set; }
        public int Edad { get; set; }
        public DateTime FechaActualizacion { get; set; }
    }
}
