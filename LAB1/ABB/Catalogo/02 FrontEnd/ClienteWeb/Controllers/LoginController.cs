using Entidades.Core; // Assuming this namespace contains your 'Usuarios' class
using Utiles.Helpers; // Assuming this namespace contains utility helpers
using System.Security.Claims;
using ClienteWeb.Models; // Assuming this namespace contains your models
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ABB.Catalogo.Utiles.Helpers;
using ABB.Catalogo.LogicaNegocio.Core;
using System.Threading.Tasks;

namespace ClienteWeb.Controllers
{
    public class LoginController : Controller
    {
        public readonly ILogger<LoginController> _log;

        public LoginController(ILogger<LoginController> logger)
        {
            _log = logger;
        }

        public ActionResult Index()
        {
            Usuarios u = new Usuarios(); // Creating a new instance of Usuarios
            Console.WriteLine("Index Get");
            _log.LogDebug("Index Get");
            return View(u); // Returning a view with Usuarios as the model
        }

        [HttpPost]
        public async Task<ActionResult> Index(Usuarios usuario)
        {
            Console.WriteLine(usuario.CodUsuario);
            if (string.IsNullOrEmpty(usuario.CodUsuario) || string.IsNullOrEmpty(usuario.ClaveTxt))
            {
                ModelState.AddModelError("*", "Debe llenar el usuario o clave");
            }
            Console.WriteLine(ModelState.IsValid);
                try
                {
                    usuario.Clave = EncriptacionHelper.EncriptarByte(usuario.ClaveTxt);
                    Usuarios res = new UsuariosLN().BuscarUsuario(usuario);
                    if (res != null && res.IdUsuario > 0)
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, res.CodUsuario)
                        };

                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                        List<Opcion> lista = new OpcionLN().ListaOpciones();
                        ParsearAcciones(lista);
                        VariablesWeb.gOpciones = lista;
                        _log.LogDebug("Llego las opciones. " + VariablesWeb.gOpciones.Count().ToString());
                        VariablesWeb.gUsuario = res;
                        return RedirectToAction("Index","Home");
                    }
                    else
                    {
                        ModelState.AddModelError("*", "Usuario/Clave no validos");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                    ModelState.AddModelError("*", ex.Message);
                    _log.LogError($"Error during login: {ex.Message}");
                }
            return View(usuario);
        }

        [NonAction]
        private void ParsearAcciones(List<Opcion> lista)
        {
            int cantidad = 0;
            foreach (Opcion item in lista)
            {
                if (!string.IsNullOrEmpty(item.UrlOpcion))
                {
                    cantidad = item.UrlOpcion.Split('/').Count();
                    switch (cantidad)
                    {
                        case 3:
                            item.Area = item.UrlOpcion.Split("/")[0];
                            item.Controladora = item.UrlOpcion.Split("/")[1];
                            item.Accion = item.UrlOpcion.Split("/")[2];
                            break;
                        case 2:
                            item.Controladora = item.UrlOpcion.Split("/")[0];
                            item.Accion = item.UrlOpcion.Split("/")[1];
                            break;
                        case 1:
                            item.Controladora = item.UrlOpcion.Split("/")[0];
                            item.Accion = "Index";
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public async Task<IActionResult> CerrarSesion()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
