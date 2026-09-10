using backend_trazabilidad.DTOs;

namespace backend_trazabilidad.Services.Jwt
{
    public interface IJwtService
    {
        string GenerarToken(UsuarioDto usario);
    }
}
