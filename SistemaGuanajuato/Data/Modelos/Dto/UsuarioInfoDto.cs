namespace SistemaGuanajuato.Data.Modelos.Dto
{
    public class UsuarioInfoDto
    {

        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public byte[]? Foto { get; set; }
        public string? ExtensionFoto {  get; set; }
    }
}
