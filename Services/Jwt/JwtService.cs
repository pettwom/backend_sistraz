using backend_trazabilidad.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace backend_trazabilidad.Services.Jwt
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration
            _configuration;


        public JwtService(
            IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public string GenerarToken(
            UsuarioDto usuario)
        {
            var jwtKey =
                _configuration["Jwt:Key"]
                ?? throw new Exception(
                    "Jwt:Key no configurado.");

            var issuer =
                _configuration["Jwt:Issuer"];

            var audience =
                _configuration["Jwt:Audience"];

            var expireMinutes =
                int.Parse(
                    _configuration[
                        "Jwt:ExpireMinutes"
                    ] ?? "60"
                );


            var claims =
                new List<Claim>
                {
                    new(
                        JwtRegisteredClaimNames.Sub,
                        usuario.IdUsuarioHydro.ToString()
                    ),

                    new(
                        ClaimTypes.NameIdentifier,
                        usuario.IdUsuarioHydro.ToString()
                    ),

                    new(
                        ClaimTypes.Name,
                        usuario.NombreCompleto
                    ),

                    new(
                        ClaimTypes.Email,
                        usuario.Correo
                    ),

                    new(
                        "entidad",
                        usuario.Entidad
                    )
                };


            foreach (
                var perfil in usuario.Perfiles)
            {
                claims.Add(
                    new Claim(
                        ClaimTypes.Role,
                        perfil
                    )
                );
            }


            var key =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                        jwtKey
                    )
                );


            var credentials =
                new SigningCredentials(
                    key,
                    SecurityAlgorithms
                        .HmacSha256
                );


            var token =
                new JwtSecurityToken(
                    issuer: issuer,
                    audience: audience,
                    claims: claims,
                    expires:
                        DateTime.UtcNow
                            .AddMinutes(
                                expireMinutes
                            ),
                    signingCredentials:
                        credentials
                );


            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }
    }
}