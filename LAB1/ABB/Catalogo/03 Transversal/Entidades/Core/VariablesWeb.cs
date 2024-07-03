using Entidades.Core;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Text.Json;

namespace Entidades.Core
{
    public class VariablesWeb
    {
        private static IHttpContextAccessor _httpContextAccessor;

        // Assign the IHttpContextAccessor instance in a static constructor or property
        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public static List<Opcion> gOpciones
        {
            get
            {
                var session = _httpContextAccessor?.HttpContext?.Session;
                if (session != null && session.GetString("gOpciones") != null)
                {
                    return JsonSerializer.Deserialize<List<Opcion>>(session.GetString("gOpciones"));
                }
                return null;
            }
            set
            {
                var session = _httpContextAccessor?.HttpContext?.Session;
                if (session != null)
                {
                    session.SetString("gOpciones", JsonSerializer.Serialize(value));
                }
            }
        }

        public static Usuarios gUsuario
        {
            get
            {
                var session = _httpContextAccessor?.HttpContext?.Session;
                if (session != null && session.GetString("gUsuario") != null)
                {
                    return JsonSerializer.Deserialize<Usuarios>(session.GetString("gUsuario"));
                }
                return null;
            }
            set
            {
                var session = _httpContextAccessor?.HttpContext?.Session;
                if (session != null)
                {
                    session.SetString("gUsuario", JsonSerializer.Serialize(value));
                }
            }
        }
    }
}
