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
        string RutaApi = "https://localhost:60433/Api/";
        string jsonMediaType = "application/json";
        public readonly HttpClient httpClient = new HttpClient();

        public string GetToken()
        {
            string controladora = "Auth";
            string metodo = "Post";
            string token = "";
            using (WebClient request = new WebClient())
            {
                request.Headers.Clear();
                request.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
                Dictionary<string, object> usuario = new Dictionary<string, object>()
                {
                    {"Codigo",1},
                    {"UserName","jayqipa"},
                    {"Clave","xyz1234"},
                    {"Nombre","sample string 4"},
                    {"Rol","sample string 5"}
                };
                request.Encoding = UTF8Encoding.UTF8;
                var usuarioJson = JsonConvert.SerializeObject(usuario);
                string rutaCompleta = RutaApi + controladora;
                var tokenJson = request.UploadString(new Uri(rutaCompleta), usuarioJson);
                token = JsonConvert.DeserializeObject<Dictionary<string, object>>(tokenJson)["token"].ToString();
            }
            return token;
        }

        public ActionResult Index()
        {
            string controladora = "Usuario";
            string metodo = "Get";
            List<Usuarios> listausuarios = new List<Usuarios>();
            string token = GetToken();
            using (WebClient usuario = new WebClient())
            {
                usuario.Headers.Clear();//borra datos anteriores
                                        //establece el tipo de dato de tranferencia
                usuario.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
                //typo de decodificador reconocimiento carecteres especiales
                usuario.Headers[HttpRequestHeader.Authorization] = "Bearer " + token;
                usuario.Encoding = UTF8Encoding.UTF8;

                string rutacompleta = RutaApi + controladora;
                //ejecuta la busqueda en la web api usando metodo GET
                var data = usuario.DownloadString(new Uri(rutacompleta));
                // convierte los datos traidos por la api a tipo lista de usuarios
                listausuarios = JsonConvert.DeserializeObject<List<Usuarios>>(data);
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
            listaRol.Add(new Rol() { IdRol = 0, DesRol = "[Seleccione Rol ...]" });
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
                string token = GetToken();
                string controladora = "Usuario";
                using (WebClient usuario = new WebClient())
                {
                    usuario.Headers.Clear();
                    usuario.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
                    usuario.Encoding = UTF8Encoding.UTF8;
                    usuario.Headers[HttpRequestHeader.Authorization] = "Bearer " + token;
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
            string token = GetToken();
            string controladora = "Usuario";
            Usuarios users = new Usuarios();
            Console.WriteLine("AEAAEA");
            Console.WriteLine(users);
            using (WebClient usuario = new WebClient())
            {
                usuario.Headers.Clear();//borra datos anteriores
                //establece el tipo de dato de tranferencia
                usuario.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
                usuario.Headers[HttpRequestHeader.Authorization] = "Bearer " + token;
                //typo de decodificador reconocimiento carecteres especiales
                usuario.Encoding = UTF8Encoding.UTF8;
                string rutacompleta = RutaApi + controladora + "/" + id;
                //ejecuta la busqueda en la web api usando metodo GET
                var data = usuario.DownloadString(new Uri(rutacompleta));

                // convierte los datos traidos por la api a tipo lista de usuarios
                users = JsonConvert.DeserializeObject<Usuarios>(data);
            }
            Console.WriteLine("AEAAEA");
            Console.WriteLine(users.IdUsuario);
            List<Rol> listaRol = new List<Rol>();
            listaRol = new RolLN().ListaRol();
            listaRol.Add(new Rol() { IdRol = 0, DesRol = "[Seleccione Rol ...]" });
            ViewBag.listaRoles = listaRol;
            ViewBag.users = users;
            return View(users);
        }

        // POST: UsuariosControllers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Usuarios collection)
        {
            try
            {
                string token = GetToken();
                string controladora = "Usuario";
                string rutaCompleta = RutaApi + controladora + "/" + id;
                Dictionary<string, object> usuarioDict = new Dictionary<string, object>()
                {
                        { "nombres", collection.Nombres},
                        { "idRol",collection.IdRol},
                        { "codUsuario",collection.CodUsuario},
                        { "clave",collection.ClaveTxt}
                };

                var usuarioJson = JsonConvert.SerializeObject(usuarioDict);
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                HttpContent contenido = new StringContent(usuarioJson, System.Text.Encoding.UTF8, jsonMediaType);
                var httpResponse = await httpClient.PutAsync(rutaCompleta, contenido);
                Console.WriteLine("Realizando put");
                if (!httpResponse.IsSuccessStatusCode)
                {
                    throw new Exception("Peticion rechazada");
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return View();
            }
        }

        // GET: UsuariosControllers/Delete/5
        public ActionResult Delete(int id)
        {
            string token = GetToken();
            string controladora = "Usuario";
            Usuarios users = new Usuarios();
            using (WebClient usuario = new WebClient())
            {
                usuario.Headers.Clear();//borra datos anteriores
                //establece el tipo de dato de tranferencia
                usuario.Headers[HttpRequestHeader.Authorization] = "Bearer " + token;
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
            ViewBag.listaRoles = listaRol[users.IdRol].DesRol;
            Console.WriteLine("AEAEAE");
            return View(users);
        }

        // POST: UsuariosControllers/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            string token = GetToken();
            try
            {
                string controladora = "Usuario";
                string rutaCompleta = RutaApi + controladora + "/" + id;
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                var httpResponse = await httpClient.DeleteAsync(rutaCompleta);
                Console.WriteLine("Realizando delete");
                if (!httpResponse.IsSuccessStatusCode)
                {
                    throw new Exception("Peticion rechazada");
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return View();
            }
        }
    }
}
