using backend_trazabilidad.Services.Auth;
using backend_trazabilidad.Services.Hydro;
using backend_trazabilidad.Services.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


/*
 * ========================================
 * CONTROLLERS
 * ========================================
 */
builder.Services.AddControllers();
/*
 * ========================================
 * CORS ANGULAR
 * ========================================
 */

builder.Services
    .AddCors(options =>
    {
        options.AddPolicy(
            "AngularApp",
            policy =>
            {
                policy
                    .WithOrigins(
                        "http://localhost:4200"
                    )
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            }
        );
    });
/*
 * ========================================
 * SERVICIOS
 * ========================================
 */
builder.Services.AddScoped<IAuthService,AuthService>();
builder.Services.AddScoped<IHydroSoapService,HydroSoapService>();
builder.Services.AddScoped<IJwtService,JwtService>();
/*
 * ========================================
 * HTTP CLIENT HYDRO
 * ========================================
 */

builder.Services
    .AddHttpClient<
        IHydroRestService,
        HydroRestService>()
    .ConfigurePrimaryHttpMessageHandler(
        () =>
        {
            return new HttpClientHandler
            {
                /*
                 * SOLO DESARROLLO.
                 *
                 * Tu PHP tenía verify=false.
                 */

                ServerCertificateCustomValidationCallback =
                    HttpClientHandler
                        .DangerousAcceptAnyServerCertificateValidator
            };
        });


/*
 * ========================================
 * JWT
 * ========================================
 */

var jwtKey =
    builder.Configuration[
        "Jwt:Key"
    ] ?? throw new Exception(
        "No se configuró Jwt:Key."
    );


var issuer =
    builder.Configuration[
        "Jwt:Issuer"
    ];


var audience =
    builder.Configuration[
        "Jwt:Audience"
    ];


builder.Services
    .AddAuthentication(
        options =>
        {
            options.DefaultAuthenticateScheme =
                JwtBearerDefaults
                    .AuthenticationScheme;

            options.DefaultChallengeScheme =
                JwtBearerDefaults
                    .AuthenticationScheme;
        }
    )
    .AddJwtBearer(
        options =>
        {
            options.TokenValidationParameters =
                new TokenValidationParameters
                {
                    ValidateIssuer =
                        true,

                    ValidateAudience =
                        true,

                    ValidateLifetime =
                        true,

                    ValidateIssuerSigningKey =
                        true,

                    ValidIssuer =
                        issuer,

                    ValidAudience =
                        audience,

                    IssuerSigningKey =
                        new SymmetricSecurityKey(
                            Encoding.UTF8
                                .GetBytes(
                                    jwtKey
                                )
                        ),

                    ClockSkew =
                        TimeSpan.Zero
                };
        }
    );


/*
 * ========================================
 * AUTHORIZATION
 * ========================================
 */

builder.Services
    .AddAuthorization();


/*
 * ========================================
 * OPENAPI
 * ========================================
 */

builder.Services
    .AddOpenApi();


var app =
    builder.Build();


/*
 * ========================================
 * PIPELINE
 * ========================================
 */

if (app.Environment
    .IsDevelopment())
{
    app.MapOpenApi();
}
/*
 * IMPORTANTE:
 *
 * CORS tiene que estar antes
 * de Authentication/Authorization.
 */

app.UseCors(
    "AngularApp"
);


app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
