using SistemaGuanajuato.Data;
using SistemaGuanajuato.Data.Modelos;
using SistemaGuanajuato.Data.Modelos.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;


namespace SistemaGuanajuato.Services
{
    public class LogueoService
    {
        private readonly ApplicationDbContext db;

        public LogueoService(ApplicationDbContext _db)
        {
            db = _db;
        }

        public async Task<RespuestaDto> RegistroUsuario(RegistroDto registro)
        {
            try
            {
                var parametros = new[]
                {
                    new SqlParameter("@Nombre", registro.Nombres.ToUpper()),
                    new SqlParameter("@ApellidoPaterno", registro.ApellidoPaterno.ToUpper()),
                    new SqlParameter("@ApellidoMaterno", registro.ApellidoMaterno.ToUpper()),
                    new SqlParameter("@FechaNacimiento", registro.FechaNacimiento),
                    new SqlParameter("@EstadoNacimiento", registro.EstadoNacimiento),
                    new SqlParameter("@CURP", registro.CURP.ToUpper()),
                    new SqlParameter("@Correo", registro.Correo),
                    //new SqlParameter("@Contrasena", EncriptaPSW(registro.Contrasena)),
                    new SqlParameter("@Contrasena", registro.Contrasena),
                    new SqlParameter("@Codigo", registro.Codigo)
                };

                var Usuario = await db.respuestaDto.FromSqlRaw("EXEC SPCreateUsuario @Nombre, @ApellidoPaterno, @ApellidoMaterno, @FechaNacimiento, @EstadoNacimiento, @CURP, @Correo, @Contrasena, @Codigo;", 
                                parametros).ToListAsync();
                return Usuario[0];
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<RespuestaDto> ValidaCuenta(ValidacionDto validacion)
        {
            try
            {
                if(validacion.Correo != null)
                {
                    validacion.Id = await db.UsuarioAD.Where(r => r.Correo == validacion.Correo).Select(r => r.Id_Usu).SingleOrDefaultAsync();
                }

                var parametros = new[]
                {
                    new SqlParameter("@Id", validacion.Id),
                    new SqlParameter("@Codigo", validacion.Codigo),
                    new SqlParameter("@Correo", validacion.Correo ?? "")
                };

                var respuesta = await db.respuestaDto.FromSqlRaw("EXEC SPValidaUsuario @Id, @Codigo, @Correo", parametros).ToListAsync();

                return respuesta[0];
            }
            catch (Exception)
            {
                return new RespuestaDto() { Id = -1, isSuccess = false, Respuesta = "Ocurrio un error" };
            }

        }

        

        public async Task<RespuestaDto> BuscaRegistro(RegistroDto registro)
        {
            try
            {
                var Busqueda = await db.respuestaDto.FromSqlRaw("").ToListAsync();
                if (Busqueda != null)
                {
                    return new RespuestaDto() { Id = 1, isSuccess=true, Respuesta = "Procede"};
                }
                return new RespuestaDto() { Id = 0, isSuccess = false, Respuesta = "Existe"};
            }
            catch (Exception)
            {
                return new RespuestaDto() { Id = -1, isSuccess = false, Respuesta = "Error" };
            }
        }

        //Funcion que nos permite encriptar la contraseña del usuario
        public string EncriptaPSW(string contrasena)
        {
            return BCrypt.Net.BCrypt.HashPassword(contrasena, workFactor: 12);
        }

        //Funcion que nos valida el PSW al hacer loggin
        /*public static bool ValidaPSW(string pswIngresada, string pswAlmacenada)
        {
            return BCrypt.Net.BCrypt.Verify(pswIngresada, pswAlmacenada);
        }*/

        

        public async Task<UsuarioEstatus> BusquedaLogueo(LoginDto login)
        {
            try
            {
                var parametros = new[]
                {
                    new SqlParameter("@Correo", login.Correo),
                    //new SqlParameter("@Contrasena", EncriptaPSW(login.Contrasena))
                    new SqlParameter("@Contrasena", login.Contrasena)
                };

                var Busqueda = await db.usuarioEstatus.FromSqlRaw("EXEC SPLogueoPlataforma @Correo, @Contrasena", parametros).ToListAsync();

                return Busqueda[0];
            }
            catch (Exception)
            {
                return new UsuarioEstatus() { Id_User = -1, Nombre = "", Correo = "", Rol = ""};
            }
        }

        public async Task<List<EstadoRegistro>> EstadosMexico()
        {
            try
            {
                return await db.Estados.ToListAsync();
            }
            catch (Exception)
            {
                return new List<EstadoRegistro>();
            }
        }
    }
}
