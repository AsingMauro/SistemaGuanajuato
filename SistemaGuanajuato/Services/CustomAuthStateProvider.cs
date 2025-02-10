using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using SistemaGuanajuato.Data.Modelos;
using System.Security.Claims;

namespace SistemaGuanajuato.Services
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ProtectedSessionStorage sessionStorage;
        private ClaimsPrincipal usuarioActual = new ClaimsPrincipal(new ClaimsIdentity());

        public CustomAuthStateProvider(ProtectedSessionStorage _sessionStorage)
        {
            sessionStorage = _sessionStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var storedUser = await sessionStorage.GetAsync<UsuarioEstatus>("usuario");

                if (storedUser.Success && storedUser.Value != null)
                {
                    var identity = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, Convert.ToString(storedUser.Value.Id_User)),
                        new Claim(ClaimTypes.Name, storedUser.Value.Nombre),
                        new Claim(ClaimTypes.Email, storedUser.Value.Correo),
                        new Claim(ClaimTypes.Role, storedUser.Value.Rol)
                    }, "apiauth");

                    usuarioActual = new ClaimsPrincipal(identity);
                }
                else
                {
                    // No hay usuario en sesión, usuario invitado
                    usuarioActual = new ClaimsPrincipal(new ClaimsIdentity());
                }
            }
            catch
            {
                // En caso de error usuario invitado
                usuarioActual = new ClaimsPrincipal(new ClaimsIdentity());
            }

            return new AuthenticationState(usuarioActual);
        }


        public async Task Login(UsuarioEstatus usuarioEstatus)
        {
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, Convert.ToString(usuarioEstatus.Id_User)),
                new Claim(ClaimTypes.Name, usuarioEstatus.Nombre),
                new Claim(ClaimTypes.Email, usuarioEstatus.Correo),
                new Claim(ClaimTypes.Role, usuarioEstatus.Rol)
            }, "apiauth");

            usuarioActual = new ClaimsPrincipal(identity);

            //Guardar sesión
            await sessionStorage.SetAsync("usuario", usuarioEstatus);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(usuarioActual)));
        }

        public async Task Logout()
        {
            usuarioActual = new ClaimsPrincipal(new ClaimsIdentity());

            // Eliminar sesión
            await sessionStorage.DeleteAsync("usuario");

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(usuarioActual)));
        }
    }
}
    
