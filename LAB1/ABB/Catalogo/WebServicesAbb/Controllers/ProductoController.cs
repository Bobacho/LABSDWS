using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Entidades.Core;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Serialization;
using ABB.Catalogo.LogicaNegocio.Core;
using System.Text.Json;
using LogicaNegocio.Core;

namespace ABB.Catalogo.WebServicesABB.Controllers
{
    [ApiController]
    [Route("/Api/Producto")]
    public class ProductoController
    {
        [HttpGet]
        public IEnumerable<Producto> Get()
        {
            List<Producto> productos = new List<Producto>();
            productos = new ProductoLN().ListarProducto();
            return productos;
        }
        [HttpGet("{id}")]
        public Producto GetById(int id)
        {
            return new ProductoLN().GetProductoById(id);
        }
        [HttpPost]
        public void Post([FromBody] Dictionary<string, object> productoParcial)
        {
            try
            {
                Producto producto = new Producto()
                {
                    NomProducto = ((JsonElement)productoParcial["nomProducto"]).GetString(),
                    DescCategoria = ((JsonElement)productoParcial["descCategoria"]).GetString(),
                    MarcaProducto = ((JsonElement)productoParcial["marcaProducto"]).GetString(),
                    ModeloProducto = ((JsonElement)productoParcial["modeloProducto"]).GetString(),
                    GarantiaProducto = ((JsonElement)productoParcial["garantiaProducto"]).GetString(),
                    LineaProducto = ((JsonElement)productoParcial["lineaProducto"]).GetString(),
                    DescripcionTecnica = ((JsonElement)productoParcial["descripcionTecnica"]).GetString(),
                    Precio = ((float)((JsonElement)productoParcial["Precio"]).GetDecimal()),
                };
                new ProductoLN().InsertarProducto(producto);
            }
            catch (Exception ex)
            {
                string innerException = ex.InnerException == null ? "" : ex.InnerException.ToString();
                Console.WriteLine("Controller:" + innerException);
            }
        }
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Dictionary<string, object> productoParcial)
        {
            try
            {
                Producto producto = new Producto()
                {
                    NomProducto = ((JsonElement)productoParcial["nomProducto"]).GetString(),
                    DescCategoria = ((JsonElement)productoParcial["descCategoria"]).GetString(),
                    MarcaProducto = ((JsonElement)productoParcial["marcaProducto"]).GetString(),
                    ModeloProducto = ((JsonElement)productoParcial["modeloProducto"]).GetString(),
                    GarantiaProducto = ((JsonElement)productoParcial["garantiaProducto"]).GetString(),
                    LineaProducto = ((JsonElement)productoParcial["lineaProducto"]).GetString(),
                    DescripcionTecnica = ((JsonElement)productoParcial["descripcionTecnica"]).GetString(),
                    Precio = ((float)((JsonElement)productoParcial["Precio"]).GetDecimal()),
                };
                new ProductoLN().ModificarProducto(id,producto);
            }
            catch (Exception ex)
            {
                string innerException = ex.InnerException == null ? "" : ex.InnerException.ToString();
                Console.WriteLine(innerException);
            }
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            try
            {
                new ProductoLN().BorrarProducto(id);
            }
            catch (Exception ex)
            {
                string innerException = ex.InnerException == null ? "" : ex.InnerException.ToString();
                Console.WriteLine(innerException);
            }
        }
    }
}

