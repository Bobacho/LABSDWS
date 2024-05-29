using ABB.Catalogo.WebServicesABB.Controllers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebServicesAbb.Filters;

const string JWT_AUDIENCE_TOKEN = "AvanzaSoluciones.com";
const string JWT_ISSUER_TOKEN = "GrupoMuya.com";
const string JWT_SECRET_KEY = "rbzO1Huj5mTP-EcXzcqi7EvGM59E7IZYajJ5aJR4WQGHbjHr1V6wCjWkWTz7dsrPth5axJFTY4hWBiYuoe2Pp0TED45dMpHiOUq1zveKmJTLkGj2WOpJp3TQwi2ONKwDy__D7oozSIUjuFNcohWIBGF57de608bZHmeLAa5g73FunoDGi8_wyZ2EVItEV9uuAO20vfZ1DtuNLva1if4mYhicd0cDCdfljRXbU8Ej67YAq2ewM-wn7QzuGnDw_aZ5vkAoLHXt4dZUFV2ORuuPO309Z65llNgKHzAXfjhlrAPIZfJslKeRmhcsbjY7TccEQuYeIhQFIAMQ7uBnHX-1Vi0";
const int JWT_EXPIRE_MINUTES = 30;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers().AddXmlSerializerFormatters();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = JWT_ISSUER_TOKEN,
            ValidAudience = JWT_AUDIENCE_TOKEN,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWT_SECRET_KEY))
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
