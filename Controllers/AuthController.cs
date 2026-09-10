using backend_trazabilidad.DTOs;
using backend_trazabilidad.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend_trazabilidad.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController :ControllerBase
    {
        private readonly IAuthService
            _authService;


        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult>Login([FromBody]LoginRequest request)
        {
            Console.Write("holas petter");
            var resultado =await _authService.LoginAsync(request);


            if (!resultado.Exito)
            {
                return Unauthorized(resultado);
            }


            return Ok(resultado);
        }


        /*
         * Endpoint para comprobar JWT
         */

        [Authorize]
        [HttpGet("usuario")]
        public IActionResult Usuario()
        {
            return Ok(
                new
                {
                    idUsuario =User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value,
                    nombre =User.Identity?.Name,
                    correo =User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value,
                    perfiles =User.FindAll(System.Security.Claims.ClaimTypes.Role).Select(x => x.Value)
                }
            );
        }


        [Authorize]
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            /*
             * Con JWT puro no hay Session.Flush().
             *
             * En la siguiente fase podemos implementar
             * sesiones en PostgreSQL para revocar tokens
             * y evitar múltiples sesiones.
             */

            return Ok(
                new
                {
                    exito = true,
                    mensaje =
                        "Sesión cerrada correctamente."
                }
            );
        }
    }
}