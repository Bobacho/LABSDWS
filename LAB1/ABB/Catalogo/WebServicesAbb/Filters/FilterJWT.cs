using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace WebServicesAbb.Filters
{
    public class FilterJWT : DelegatingHandler
    {
        private const string JWT_AUDIENCE_TOKEN = "AvanzaSoluciones.com";
        private const string JWT_ISSUER_TOKEN = "GrupoMuya.com";
        private const string JWT_SECRET_KEY = "rbzO1Huj5mTP-EcXzcqi7EvGM59E7IZYajJ5aJR4WQGHbjHr1V6wCjWkWTz7dsrPth5axJFTY4hWBiYuoe2Pp0TED45dMpHiOUq1zveKmJTLkGj2WOpJp3TQwi2ONKwDy__D7oozSIUjuFNcohWIBGF57de608bZHmeLAa5g73FunoDGi8_wyZ2EVItEV9uuAO20vfZ1DtuNLva1if4mYhicd0cDCdfljRXbU8Ej67YAq2ewM-wn7QzuGnDw_aZ5vkAoLHXt4dZUFV2ORuuPO309Z65llNgKHzAXfjhlrAPIZfJslKeRmhcsbjY7TccEQuYeIhQFIAMQ7uBnHX-1Vi0";
        private const int JWT_EXPIRE_MINUTES = 30;

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpStatusCode statusCode;
            string token;

            if (!TryRetrieveToken(request, out token))
            {
                statusCode = HttpStatusCode.Unauthorized;
                return base.SendAsync(request, cancellationToken);
            }

            try
            {

                string clave = JWT_SECRET_KEY;
                byte[] claveEnBytes = Encoding.UTF8.GetBytes(clave);
                SymmetricSecurityKey key = new SymmetricSecurityKey(claveEnBytes);


                //var claveSecreta = ConfigurationManager.AppSettings["JwtSecret"];
                var issuerToken = JWT_ISSUER_TOKEN;
                var audienceToken = JWT_AUDIENCE_TOKEN;

                //var securityKey = new SymmetricSecurityKey(
                //    System.Text.Encoding.Default.GetBytes(claveSecreta));

                SecurityToken securityToken;
                var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
                TokenValidationParameters validationParameters = new TokenValidationParameters()
                {
                    ValidAudience = audienceToken,
                    ValidIssuer = issuerToken,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key
                };

                // COMPRUEBA LA VALIDEZ DEL TOKEN
                Thread.CurrentPrincipal = tokenHandler.ValidateToken(token,
                                                                     validationParameters,
                                                                     out securityToken);
 

                return base.SendAsync(request, cancellationToken);
            }
            catch (SecurityTokenValidationException)
            {
                statusCode = HttpStatusCode.Unauthorized;
            }
            catch (Exception)
            {
                statusCode = HttpStatusCode.InternalServerError;
            }

            return Task<HttpResponseMessage>.Factory.StartNew(() =>
                        new HttpResponseMessage(statusCode) { });
        }

        // RECUPERA EL TOKEN DE LA PETICIÓN
        private static bool TryRetrieveToken(HttpRequestMessage request, out string token)
        {
            token = null;
            IEnumerable<string> authzHeaders;
            if (!request.Headers.TryGetValues("Authorization", out authzHeaders) ||
                                              authzHeaders.Count() > 1)
            {
                return false;
            }
            var bearerToken = authzHeaders.ElementAt(0);
            token = bearerToken.StartsWith("Bearer ") ?
                    bearerToken.Substring(7) : bearerToken;
            return true;
        }


    }
}
