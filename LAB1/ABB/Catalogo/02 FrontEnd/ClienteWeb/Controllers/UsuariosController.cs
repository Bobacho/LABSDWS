using Entidades.Core;
using LogicaNegocio.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Mime;
using System.Text;

namespace ClienteWeb.Controllers
{
    public class UsuariosController : Controller
    {
        string RutaApi = "http://localhost:5202/Api/";
        string jsonMediaType = "application/json";
        public ActionResult Index()
        {
            string controladora ="Usuario";
            string metodo ="Get";
            List<Usuarios>listausuarios = new List<Usuarios>();
            using (WebClient usuario = new WebClient())
            {
                usuario.Headers.Clear();//borra datos anteriores
                                        //establece el tipo de dato de tranferencia
                usuario.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
                //typo de decodificador reconocimiento carecteres especiales
                usuario.Encoding = UTF8Encoding.UTF8;
                string rutacompleta = RutaApi + controladora;
                //ejecuta la busqueda en la web api usando metodo GET
                var data = usuario.DownloadString(new Uri(rutacompleta));
                // convierte los datos traidos por la api a tipo lista de usuarios
                listausuarios = JsonConvert.DeserializeObject<List<Usuarios>> (data);
            }
            return View(listausuarios);
        }

        // GET: UsuariosControllers/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UsuariosControllers/Create
        public ActionResult Create()
        {
            Usuarios usuario = new Usuarios();
            List<Rol> listaRol = new List<Rol>();
            listaRol = new RolLN().ListaRol();
            listaRol.Add(new Rol() { IdRol = 0, DesRol = "[Seleccione Rol ...]"});
            ViewBag.listaRoles = listaRol;
            return View(usuario);
        }

        // POST: UsuariosControllers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Usuarios collection)
        {
            try
            {
                string controladora = "Usuario";
                using (WebClient usuario= new WebClient())
                {
                    usuario.Headers.Clear();
                    usuario.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
                    usuario.Encoding = UTF8Encoding.UTF8;
                    Dictionary<string, object> usuariosDict = new Dictionary<string, object>() 
                    {
                        {"nombres",collection.Nombres },
                        {"idRol",collection.IdRol},
                        {"clave",collection.ClaveTxt },
                        {"codUsuario",collection.CodUsuario}
                    };
                    var usuarioJson = JsonConvert.SerializeObject(usuariosDict);
                    string rutacompleta = RutaApi + controladora;
                    var resultado = usuario.UploadString(new Uri(rutacompleta), usuarioJson);
                    Console.WriteLine(resultado);
                }
                    return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsuariosControllers/Edit/5
        public ActionResult Edit(int id)
        {
            string controladora = "Usuario";
            Usuarios users = new Usuarios();
            using (WebClient usuario = new WebClient())
            {
                usuario.Headers.Clear();//borra datos anteriores
                //establece el tipo de dato de tranferencia
                usuario.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
                //typo de decodificador reconocimiento carecteres especiales
                usuario.Encoding = UTF8Encoding.UTF8;
                string rutacompleta = RutaApi + controladora + "/" + id;
                //ejecuta la busqueda en la web api usando metodo GET
                var data = usuario.DownloadString(new Uri(rutacompleta));

                // convierte los datos traidos por la api a tipo lista de usuarios
                users = JsonConvert.DeserializeObject<Usuarios>(data);
            }
            List<Rol> listaRol = new List<Rol>();
            listaRol = new RolLN().ListaRol();
            listaRol.Add(new Rol() { IdRol = 0, DesRol = "[Seleccione Rol ...]" });
            ViewBag.listaRoles = listaRol;
            return View(users);
        }

        // POST: UsuariosControllers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Usuarios collection)
        {
            try
            {
                using (WebClient usuario = new WebClient())
                {
                    usuario.Headers.Clear();
                    usuario.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
                    usuario.Encoding = UTF8Encoding.UTF8;
                    string controladora = "Usuario";
                    string rutaCompleta = RutaApi+controladora+"/"+id;
                    Dictionary<string, object> usuarioDict = new Dictionary<string, object>()
                    {
                        { "nombres", collection.Nombres},
                        { "idRol",collection.IdRol},
                        { "codUsuario",collection.CodUsuario},
                        { "clave",collection.ClaveTxt}
                    };
                    var usuarioJson=JsonConvert.SerializeObject(usuarioDict);
                    var resultado=usuario.UploadString(rutaCompleta,"PUT",usuarioJson);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsuariosControllers/Delete/5
        public ActionResult Delete(int id)
        {
            string controladora = "Usuario";
            Usuarios users = new Usuarios();
            using (WebClient usuario = new WebClient())
            {
                usuario.Headers.Clear();//borra datos anteriores
                //establece el tipo de dato de tranferencia
                usuario.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
                //typo de decodificador reconocimiento carecteres especiales
                usuario.Encoding = UTF8Encoding.UTF8;
                string rutacompleta = RutaApi + controladora + "/" + id;
                //ejecuta la busqueda en la web api usando metodo GET
                var data = usuario.DownloadString(new Uri(rutacompleta));

                // convierte los datos traidos por la api a tipo lista de usuarios
                users = JsonConvert.DeserializeObject<Usuarios>(data);
            }
            return View(users);
        }

        // POST: UsuariosControllers/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                using (WebClient usuario = new WebClient())
                {
                    usuario.Headers.Clear();
                    usuario.Headers[HttpResponseHeader.ContentType] = jsonMediaType;
                    usuario.Encoding = Encoding.UTF8;
                    string controladora = "Usuario";
                    string rutaCompleta = RutaApi + controladora + "/" + id;
                    var resultado = usuario.UploadString(rutaCompleta, "DELETE", "");
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
