using System.ComponentModel.DataAnnotations;

namespace SistemaGuanajuato.Data.Modelos
{
    public class UsuarioAD
    {
        [Key]
        public int Id_Usu { get; set; }
        public byte[]? Foto { get; set; }
        public string Correo { get; set; }
        public string Contrasena { get; set; }
        public bool Validacion { get; set; }
        public int Codigo {  get; set; }
        public int Id_Rol {  get; set; }
        public DateTime FechaActualizacion { get; set; }
        public string? ExtensionFoto { get; set; }
    }
}
