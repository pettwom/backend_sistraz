using System.Text;
using backend_trazabilidad.Services.Auth;
using backend_trazabilidad.Services.Hydro;
using backend_trazabilidad.Services.Jwt;
using backend_trazabilidad.Services.Menu;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using backend_trazabilidad;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Oracle.EntityFrameworkCore;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);
// ========================================
// ADICIONA CONFIGURACION DE ORACLE
// ========================================
builder.Services.AddDbContext<AplicationDbContext>(options =>
{
    options.UseOracle(
        builder.Configuration.GetConnectionString("ConexionOracle")
        );
});

// ========================================
// ADICIONA CONFIGURACION DE POSTGRESQL
// ========================================
builder.Services.AddDbContext<PostgresDbContext>(options =>
{
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("PostgreSqlConection")
        );
});

// ========================================
// CONTROLLERS                             
// ========================================

builder.Services.AddControllers();


// ========================================
// CORS ANGULAR
// ========================================

builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularLocal", policy =>
    {
        policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


// ========================================
// SERVICIOS
// ========================================

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IHydroSoapService, HydroSoapService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IMenuService, MenuService>();

builder.Services
    .AddHttpClient<IHydroRestService, HydroRestService>()
    .ConfigurePrimaryHttpMessageHandler(() =>
    {
        return new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback =
                HttpClientHandler
                    .DangerousAcceptAnyServerCertificateValidator
        };
    });


// ========================================
// JWT
// ========================================

var jwtKey =
    builder.Configuration["Jwt:Key"]
    ?? throw new Exception(
        "Jwt:Key no configurado."
    );

var issuer =
    builder.Configuration["Jwt:Issuer"];

var audience =
    builder.Configuration["Jwt:Audience"];


builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme =
            JwtBearerDefaults.AuthenticationScheme;

        options.DefaultChallengeScheme =
            JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = issuer,
                ValidAudience = audience,

                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtKey)
                    ),

                ClockSkew = TimeSpan.Zero
            };
    });


builder.Services.AddAuthorization();

builder.Services.AddOpenApi();


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


// ========================================
// PIPELINE
// ========================================

app.UseHttpsRedirection();

// MUY IMPORTANTE
app.UseCors("AngularLocal");

app.UseRouting();


app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();



//using backend_trazabilidad.Services.Auth;
//using backend_trazabilidad.Services.Hydro;
//using backend_trazabilidad.Services.Jwt;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.IdentityModel.Tokens;
//using System.Text;

//var builder = WebApplication.CreateBuilder(args);


///*
// * ========================================
// * CONTROLLERS
// * ========================================
// */
//builder.Services.AddControllers();
///*
// * ========================================
// * CORS ANGULAR
// * ========================================
// */

//builder.Services.AddCors(options =>
//    {
//        options.AddPolicy("AngularApp", policy =>
//            {
//                policy
//                .WithOrigins("http://localhost:4200")
//                .AllowAnyHeader()
//                .AllowAnyMethod();
//            });
//    });
///*
// * ========================================
// * SERVICIOS
// * ========================================
// */
//builder.Services.AddScoped<IAuthService, AuthService>();
//builder.Services.AddScoped<IHydroSoapService, HydroSoapService>();
//builder.Services.AddScoped<IJwtService, JwtService>();
///*
// * ========================================
// * HTTP CLIENT HYDRO
// * ========================================
// */

//builder.Services.AddHttpClient<IHydroRestService, HydroRestService>()
//    .ConfigurePrimaryHttpMessageHandler(() =>
//        {
//            return new HttpClientHandler
//            {
//                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
//            };
//        });


///*
// * ========================================
// * JWT
// * ========================================
// */

//var jwtKey =builder.Configuration["Jwt:Key"] ?? throw new Exception("No se configuró Jwt:Key.");

//var issuer =builder.Configuration["Jwt:Issuer"];

//var audience =builder.Configuration["Jwt:Audience"];

//builder.Services.AddAuthentication(options =>
//        {
//            options.DefaultAuthenticateScheme =JwtBearerDefaults.AuthenticationScheme;

//            options.DefaultChallengeScheme =JwtBearerDefaults.AuthenticationScheme;
//        }
//    ).AddJwtBearer(options =>
//        {
//            options.TokenValidationParameters =new TokenValidationParameters
//                {
//                    ValidateIssuer =true,
//                    ValidateAudience =true,
//                    ValidateLifetime =true,
//                    ValidateIssuerSigningKey =true,
//                    ValidIssuer =issuer,
//                    ValidAudience =audience,
//                    IssuerSigningKey =new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),

//                    ClockSkew =TimeSpan.Zero
//                };
//        }
//    );


///*
// * ========================================
// * AUTHORIZATION
// * ========================================
// */

//builder.Services.AddAuthorization();


///*
// * ========================================
// * OPENAPI
// * ========================================
// */

//builder.Services.AddOpenApi();


//var app = builder.Build();


///*
// * ========================================
// * PIPELINE
// * ========================================
// */

//if (app.Environment.IsDevelopment())
//{
//    app.MapOpenApi();
//}
///*
// * IMPORTANTE:
// *
// * CORS tiene que estar antes
// * de Authentication/Authorization.
// */

//app.UseHttpsRedirection();
//app.UseCors("AllowAngular");
//app.UseAuthentication();
//app.UseAuthorization();
//app.MapControllers();
//app.Run();