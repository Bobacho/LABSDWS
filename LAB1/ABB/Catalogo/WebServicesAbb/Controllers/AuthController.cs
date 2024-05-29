using Entidades.Core;
using LogicaNegocio.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace WebServicesAbb.Controllers
{
    [ApiController]
    [Route("/Api/Auth")]
    public class AuthController : Controller
    {
        private const string JWT_AUDIENCE_TOKEN = "AvanzaSoluciones.com";
        private const string JWT_ISSUER_TOKEN = "GrupoMuya.com";
        private const string JWT_SECRET_KEY = "rbzO1Huj5mTP-EcXzcqi7EvGM59E7IZYajJ5aJR4WQGHbjHr1V6wCjWkWTz7dsrPth5axJFTY4hWBiYuoe2Pp0TED45dMpHiOUq1zveKmJTLkGj2WOpJp3TQwi2ONKwDy__D7oozSIUjuFNcohWIBGF57de608bZHmeLAa5g73FunoDGi8_wyZ2EVItEV9uuAO20vfZ1DtuNLva1if4mYhicd0cDCdfljRXbU8Ej67YAq2ewM-wn7QzuGnDw_aZ5vkAoLHXt4dZUFV2ORuuPO309Z65llNgKHzAXfjhlrAPIZfJslKeRmhcsbjY7TccEQuYeIhQFIAMQ7uBnHX-1Vi0";
        private const int JWT_EXPIRE_MINUTES = 30;

        [HttpPost]
        public ActionResult Valida([FromBody] UsuariosApi usuarioAutentica)
        {
            //validaciones antes de procesar
            if (string.IsNullOrEmpty(usuarioAutentica.Clave)
                || string.IsNullOrEmpty(usuarioAutentica.UserName))
            {
                return BadRequest("Debe enviar la clave o codigo");
            }

            //validas los datos
            //validacion contra la bd
            UsuariosApi usuarioapi = new UsuariosApi();
            usuarioapi = new UsuarioApiLN().BuscaUsuarioApi(usuarioAutentica);

            if ((usuarioapi.Codigo <= 0))
            {
                return BadRequest("Credenciales no validas");

            }
            var expireTime = JWT_EXPIRE_MINUTES;

            //crear la semilla
            string clave = JWT_SECRET_KEY;
            byte[] claveEnBytes = Encoding.UTF8.GetBytes(clave);
            //para convertir la clave que esta como un arreglo de bytes en simetrica
            SymmetricSecurityKey key = new SymmetricSecurityKey(claveEnBytes);

            //generar algoritmo de ofuscacion
            SigningCredentials cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //payload
            var _Claims = new[] {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.NameId,  usuarioAutentica.UserName.ToString()),
                //new Claim("nombre", "David"),
                new Claim("nombre", usuarioapi.Nombre),
               // new Claim("apellidos", "Espinoza"),
                new Claim("rol", usuarioapi.Rol),
               // new Claim(JwtRegisteredClaimNames.Email, "despinoza@avanza....com"),
               // new Claim(ClaimTypes.Role, "admin")
            };

            //encriptador
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                audience: JWT_AUDIENCE_TOKEN,
                issuer: JWT_ISSUER_TOKEN,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(expireTime)), //DateTime.Now.AddMinutes(10),
                claims: _Claims,
                signingCredentials: cred
                );


            string token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);


            return Ok(new TokenResponse
            {
                Token = token,
                User = usuarioAutentica.UserName

            });
        }
    }
}
