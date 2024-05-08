using Entidades.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace ClienteWeb.Controllers
{
    public class ProductoController : Controller
    {
        string RutaApi = "http://localhost:5202/Api/";
        string jsonMediaType = "application/json";
        // GET: ProductoController
        public ActionResult Index()
        {
            string controladora = "Producto";
            List<Producto> listaproducto = new List<Producto>();
            using (WebClient producto = new WebClient())
            {
                producto.Headers.Clear();//borra datos anteriores
                                         //establece el tipo de dato de tranferencia
                producto.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
                //typo de decodificador reconocimiento carecteres especiales
                producto.Encoding = UTF8Encoding.UTF8;
                string rutacompleta = RutaApi + controladora;
                //ejecuta la busqueda en la web api usando metodo GET
                var data = producto.DownloadString(new Uri(rutacompleta));
                // convierte los datos traidos por la api a tipo lista de usuarios
                listaproducto = JsonConvert.DeserializeObject<List<Producto>>(data);
            }
            return View(listaproducto);
        }

        // GET: ProductoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductoController/Create
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

        // GET: ProductoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductoController/Edit/5
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

        // GET: ProductoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductoController/Delete/5
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
