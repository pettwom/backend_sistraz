using backend_trazabilidad.DTOs.Postgresql;

namespace backend_trazabilidad.Services.Postgresql
{
    public interface IEventoService
    {
        Task<EventoRequestDto> AlmacenarEvento(EventoRequestDto erd);
        Task<long> CrearEvento(EventoRequestDto erd);
    }
}
