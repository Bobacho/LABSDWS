using Entidades.Core;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using System.Linq;

namespace Utiles.Helpers
{
    public static class MenuHelper
    {   /*
        public static IHtmlContent HelperMenu(this IHtmlHelper helper, List<Opcion> listaOpciones, string titulo)
        {
            var principal = new TagBuilder("ul");
            principal.AddCssClass("sidebar-menu");
            principal.Attributes.Add("data-widget", "tree");

            var liHeader = new TagBuilder("li");
            liHeader.AddCssClass("header");
            liHeader.InnerHtml.Append(titulo);
            principal.InnerHtml.AppendHtml(liHeader);

            if (listaOpciones == null || !listaOpciones.Any())
            {
                return principal;
            }

            bool tieneHijo = false;

            foreach (Opcion item in listaOpciones
                .Where(t => t.IdOpcionRef == 0)
                .OrderBy(r => r.NroOrden).ToList())
            {
                tieneHijo = listaOpciones.Any(t => t.IdOpcionRef == item.IdOpcion);

                var itemLista = new TagBuilder("li");
                if (tieneHijo)
                {
                    itemLista.AddCssClass("treeview");
                }

                var linkLista = new TagBuilder("a");
                linkLista.Attributes["href"] = GeneraHRef(helper, item);

                var iLista = new TagBuilder("i");
                iLista.AddCssClass(item.RutaImagen);
                linkLista.InnerHtml.AppendHtml(iLista);

                var spanLista = new TagBuilder("span");
                spanLista.InnerHtml.Append(item.NombreOpcion);
                linkLista.InnerHtml.AppendHtml(spanLista);

                if (tieneHijo)
                {
                    var spanHijo = new TagBuilder("span");
                    spanHijo.AddCssClass("pull-right-container");

                    var iHijo = new TagBuilder("i");
                    iHijo.AddCssClass("fa fa-angle-left pull-right");
                    spanHijo.InnerHtml.AppendHtml(iHijo);

                    linkLista.InnerHtml.AppendHtml(spanHijo);
                }

                itemLista.InnerHtml.AppendHtml(linkLista);
                if (tieneHijo)
                {
                    LlenarOpcionMenu(itemLista, item, listaOpciones, helper);
                }

                principal.InnerHtml.AppendHtml(itemLista);
            }

            return principal;
        }

        private static void LlenarOpcionMenu(TagBuilder itemLista, Opcion item, List<Opcion> listaOpciones, IHtmlHelper helper)
        {
            var ulHijo = new TagBuilder("ul");
            ulHijo.AddCssClass("treeview-menu");

            foreach (Opcion itemOpcion in listaOpciones
                .Where(x => x.IdOpcionRef == item.IdOpcion)
                .ToList())
            {
                bool tieneHijo = listaOpciones.Any(x => x.IdOpcionRef == itemOpcion.IdOpcion);

                var liHijo = new TagBuilder("li");
                if (tieneHijo)
                {
                    liHijo.AddCssClass("treeview");
                }

                var aHijo = new TagBuilder("a");
                aHijo.Attributes["href"] = GeneraHRef(helper, itemOpcion);

                var iHijo = new TagBuilder("i");
                iHijo.AddCssClass(itemOpcion.RutaImagen);
                aHijo.InnerHtml.AppendHtml(iHijo);
                aHijo.InnerHtml.Append(itemOpcion.NombreOpcion);

                if (tieneHijo)
                {
                    var spanHijo = new TagBuilder("span");
                    spanHijo.AddCssClass("pull-right-container");

                    var iHijo2 = new TagBuilder("i");
                    iHijo2.AddCssClass("fa fa-angle-left pull-right");
                    spanHijo.InnerHtml.AppendHtml(iHijo2);

                    aHijo.InnerHtml.AppendHtml(spanHijo);
                }

                liHijo.InnerHtml.AppendHtml(aHijo);

                if (tieneHijo)
                {
                    LlenarOpcionMenu(liHijo, itemOpcion, listaOpciones, helper);
                }

                ulHijo.InnerHtml.AppendHtml(liHijo);
            }

            itemLista.InnerHtml.AppendHtml(ulHijo);
        }

        private static string GeneraHRef(IHtmlHelper helper, Opcion item)
        {
            string rutaUrl = string.Empty;
            var urlHelperFactory = (IUrlHelperFactory)helper.ViewContext.HttpContext.RequestServices.GetService(typeof(IUrlHelperFactory));
            var urlHelper = urlHelperFactory.GetUrlHelper(helper.ViewContext);

            if (!string.IsNullOrEmpty(item.UrlOpcion) && item.UrlOpcion != "#")
            {
                string[] ruta = item.UrlOpcion.Split('/');
                switch (ruta.Length)
                {
                    case 1:
                        rutaUrl = urlHelper.Action("Index", ruta[0], new { });
                        break;
                    case 2:
                        if (ruta[1].Contains("?"))
                        {
                            rutaUrl = urlHelper.Action(ruta[1].Split('?')[0], ruta[0], RetornaObjetoParametros(ruta[1].Split('?')[1]), new { });
                        }
                        else
                        {
                            rutaUrl = urlHelper.Action(ruta[1], ruta[0], new { });
                        }
                        break;
                    case 3:
                        if (ruta[2].Contains("?"))
                        {
                            rutaUrl = urlHelper.Action(ruta[2].Split('?')[0], $"{ruta[0]}/{ruta[1]}", RetornaObjetoParametros(ruta[2].Split('?')[1]), new { });
                        }
                        else
                        {
                            rutaUrl = urlHelper.Action(ruta[2], $"{ruta[0]}/{ruta[1]}", new { });
                        }
                        break;
                    default:
                        rutaUrl = item.UrlOpcion;
                        break;
                }
            }
            else
            {
                rutaUrl = "#";
            }

            return rutaUrl;
        }

        private static RouteValueDictionary RetornaObjetoParametros(string parametro)
        {
            var rvd = new RouteValueDictionary();

            string[] parametros = parametro.Split('&');
            foreach (string item in parametros)
            {
                rvd.Add(item.Split('=')[0], item.Split('=')[1]);
            }

            return rvd;
        }
        */
    }
}

