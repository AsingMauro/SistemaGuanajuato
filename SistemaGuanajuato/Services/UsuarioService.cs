using SistemaGuanajuato.Data;
using SistemaGuanajuato.Data.Modelos.Dto;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using SistemaGuanajuato.Data.Modelos;

namespace SistemaGuanajuato.Services
{
    public class UsuarioService
    {
        private readonly ApplicationDbContext db;
        private readonly CustomAuthStateProvider autenticacion;
        private int idUsuario;
        public UsuarioService(ApplicationDbContext _db, CustomAuthStateProvider _atenticacion)
        {
            db = _db;
            autenticacion = _atenticacion;
        }

        public async Task<UsuarioInfoDto> DatosUsuario()
        {
            try
            {
                idUsuario = await ObtenerIdToken();
                var datos = await db.usuarioInfoDto.FromSqlRaw("select INF.Nombre, INF.ApellidoMaterno, INF.ApellidoPaterno, AD.Foto, AD.ExtensionFoto from UsuarioInfo AS INF " +
                    "INNER JOIN UsuarioAD AS AD ON AD.Id_Usu = INF.Id_Usu WHERE INF.Id_Usu = @Id", new SqlParameter("@Id", idUsuario)).ToListAsync();
                return datos[0];

            }
            catch (Exception)
            {
                return new UsuarioInfoDto();
            }
        }

        public async Task<int> ObtenerIdToken()
        {
            var authState = await autenticacion.GetAuthenticationStateAsync();
            var user = authState.User;
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Convert.ToInt32(userId);
        }

        public async Task<RespuestaDto> ActualizaRegistro(UsuarioInfoDto respuesta)
        {
            try
            {
                idUsuario = await ObtenerIdToken();
                var usuarioInfo = await db.UsuarioInfo.FindAsync(idUsuario);
                var usuarioA = await db.UsuarioAD.FindAsync(idUsuario);

                if (usuarioInfo != null && usuarioA != null)
                {
                    usuarioInfo.Nombre = respuesta.Nombre;
                    usuarioInfo.ApellidoPaterno = respuesta.ApellidoPaterno;
                    usuarioInfo.ApellidoMaterno = respuesta.ApellidoMaterno;
                    usuarioInfo.FechaActualizacion = DateTime.Now;
                    
                    if(respuesta.Foto != null && respuesta.ExtensionFoto != null)
                    {
                        usuarioA.Foto = respuesta.Foto;
                        usuarioA.ExtensionFoto = respuesta.ExtensionFoto;
                    }                                       
                    usuarioA.FechaActualizacion = DateTime.Now;
                    
                    await db.SaveChangesAsync();
                    return new RespuestaDto() { Id = 1, isSuccess = true, Respuesta = "Actualizado" };
                }
                else
                {
                    return new RespuestaDto() { Id = 0, isSuccess = false, Respuesta = "No Actualizado" };
                }
            }
            catch (Exception)
            {
                return new RespuestaDto() { Id = 0, isSuccess = false, Respuesta = "Ocurrio un error" };
            }
        }
        public async Task<List<ControlUsuarioDto>> ObtenListaUsuario()
        {
            try
            {
                var usuario = await db.controlUsuarioDto.FromSqlRaw("select INF.Id_Usu, CONCAT(INF.Nombre,' ',INF.ApellidoPaterno,' ',INF.ApellidoMaterno) As NombreCompleto, AD.Correo, ROL.Rol FROM UsuarioInfo AS INF " +
                    "INNER JOIN UsuarioAD AS AD ON AD.Id_Usu = INF.Id_Usu INNER JOIN UsuarioRol AS ROL ON ROL.Id_Rol = AD.Id_Rol WHERE AD.Id_Rol in (1,2)").ToListAsync();
                return usuario;
            }
            catch (Exception)
            {
                return new List<ControlUsuarioDto> { };
            }
        }

        public async Task<RespuestaDto> EliminaUsuario(int IdUsuario)
        {
            try
            {
                var usuario = await db.UsuarioInfo.FindAsync(IdUsuario);
                var usuarioAd = await db.UsuarioAD.FindAsync(IdUsuario);
                if(usuario != null && usuarioAd != null)
                {
                    db.UsuarioInfo.Remove(usuario);
                    db.UsuarioAD.Remove(usuarioAd);
                    db.SaveChanges();
                    return new RespuestaDto() { Id = 1, isSuccess = true, Respuesta = "Registro Eliminado" };
                    
                }
                return new RespuestaDto() { Id = 0, isSuccess = false, Respuesta = "No se pudo eliminar el registro" };
            }
            catch (Exception)
            {

                return new RespuestaDto() { Id = -1, isSuccess = false, Respuesta = "Ocurrio un error"};
            }
        }

    }
}
