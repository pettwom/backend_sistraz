using backend_trazabilidad.DTOs.Postgresql;
using backend_trazabilidad.Models.Postgresql;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using System.Text.Json.Nodes;

namespace backend_trazabilidad.Services.Postgresql
{
    public class TrazabilidadViewService : ITrazabilidadViewService
    {
        private readonly PostgresDbContext _context; 
        public TrazabilidadViewService(PostgresDbContext context)
        {
            _context = context;
        }

        public async Task<List<TrazabilidadViewResponseDto>> ObtenerTrazabilidadAsync(TrazabilidadViewRequestDto tvr)
        {
            var datos = await _context.TrazabilidadViews
                .AsNoTracking()
                .Where(tv => tv.CodigoTrazabilidad == tvr.codigoTrazabilidad)
                .OrderBy(tv => tv.IdEvento)
                .ToListAsync();

            var traz = datos.Select(tv => new TrazabilidadViewResponseDto
            {
                IdLote = tv.IdLote,
                CodigoTrazabilidad = tv.CodigoTrazabilidad,
                IdEvento = tv.IdEvento,
                TipoEvento = tv.TipoEvento,
                FechaEvento = tv.FechaEvento,
                Estado = tv.Estado,
                Origenes = ParseJsonArray(tv.Origenes),
                Destinos = ParseJsonArray(tv.Destinos),
                Certificados = ParseJsonArray(tv.Certificados)
            }).ToList();
            return traz;
        }
        private static JsonArray ParseJsonArray(string? json)
        {
            if (string.IsNullOrWhiteSpace(json)) return new JsonArray();
            return JsonNode.Parse(json)?.AsArray() ?? new JsonArray();
        }
    }
}
