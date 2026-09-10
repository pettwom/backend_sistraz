using backend_trazabilidad.DTOs;

namespace backend_trazabilidad.Services.Auth
{
    public interface IAuthService
    {
        Task<LoginResponse>LoginAsync(LoginRequest request);
    }
}
