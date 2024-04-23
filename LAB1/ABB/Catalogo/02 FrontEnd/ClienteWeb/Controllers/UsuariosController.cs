using Entidades.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
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
            return View();
        }

        // POST: UsuariosControllers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
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
            return View();
        }

        // POST: UsuariosControllers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
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
            return View();
        }

        // POST: UsuariosControllers/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
