using backend_trazabilidad.Models.Postgresql;
using backend_trazabilidad.Services.Postgresql;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Bibliography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend_trazabilidad.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExcelController : ControllerBase
    {
        private readonly PostgresDbContext _context;
        private readonly IExcelService _service;
        public ExcelController(PostgresDbContext context, IExcelService service)
        {
            _context = context;
            _service = service;
        }

        [HttpPost("cisternas")]
        public async Task<IActionResult> ImportarCisternas(IFormFile archivo)
        {
            if (archivo == null || archivo.Length == 0)
            {
                return BadRequest(new
                {
                    mensaje = "Debe Seleccionar un archivo"
                });
            }
            var registros = new List<TbCisternaDetalle>();
            var errores = new List<string>();

            using var stream = archivo.OpenReadStream();
            using var workbook = new XLWorkbook(stream);

            var hoja = workbook.Worksheet("GLP MERCADO INTERNO");//SELECCIONO LA HOJA

            foreach (var fila in hoja.RowsUsed().Where(r => r.RowNumber() >= 12)) //LE INDICO HASTA DONDE SALTARA Y DESDE DONDE INICIARA 
            {
                var numeroFila = fila.RowNumber();
                try
                {
                    // Si no tiene N°, ignoramos la fila
                    if (fila.Cell(1).IsEmpty()) continue;

                    var nro = fila.Cell(1).GetValue<int>();

                    // B - FECHA
                    if (!fila.Cell(2).TryGetValue<DateTime>(out var fecha))
                    {
                        errores.Add($"Fila {numeroFila}: fecha inválida.");
                        continue;
                    }

                    // C - No CRE
                    var nroCre = fila.Cell(3).GetString().Trim().ToUpper();

                    // D - CRE Fenix
                    var nroCreFenix = fila.Cell(4).GetString().Trim().ToUpper();

                    // E - Peso Kg
                    if (!fila.Cell(5).TryGetValue<decimal>(out var pesoKg))
                    {
                        errores.Add($"Fila {numeroFila}: Peso Kg inválido.");
                        continue;
                    }

                    // F - Peso TM
                    if (!fila.Cell(6).TryGetValue<decimal>(out var pesoTm))
                    {
                        errores.Add($"Fila {numeroFila}: Peso TM inválido.");
                        continue;
                    }

                    // G - Volumen m3
                    if (!fila.Cell(7).TryGetValue<decimal>(out var volM3))
                    {
                        errores.Add($"Fila {numeroFila}: Volumen m3 inválido.");
                        continue;
                    }

                    // H - Volumen BBL
                    if (!fila.Cell(8).TryGetValue<decimal>(out var volBbls))
                    {
                        errores.Add($"Fila {numeroFila}: Volumen BBL inválido.");
                        continue;
                    }

                    // I - Gravedad
                    if (!fila.Cell(9).TryGetValue<decimal>(out var gravedad))
                    {
                        errores.Add($"Fila {numeroFila}: Gravedad inválida.");
                        continue;
                    }

                    // J
                    var cliente = fila.Cell(10).GetString().Trim().ToUpper();

                    // K
                    var plantaDescarga = fila.Cell(11).GetString().Trim().ToUpper();

                    // L
                    var conductor = fila.Cell(12).GetString().Trim().ToUpper();

                    // M
                    var placa = fila.Cell(13).GetString().Trim().ToUpper();

                    // N
                    var empresaTrans = fila.Cell(14).GetString().Trim().ToUpper();

                    // O - Inicio carga
                    TimeSpan? inicioCarga = null;

                    if (fila.Cell(15).TryGetValue<TimeSpan>(out var inicio))
                    {
                        inicioCarga = inicio;
                    }

                    // P - Final carga
                    TimeSpan? finalCarga = null;

                    if (fila.Cell(16).TryGetValue<TimeSpan>(out var final))
                    {
                        finalCarga = final;
                    }



                    var existe = await _context.TbCisternaDetalles.AnyAsync(x =>
                                x.Fecha == fecha.Date && x.NroCre == nroCre && x.NroCreFenix == nroCreFenix && x.Placa == placa);
                    if (existe)
                    {
                        errores.Add($"Fila {numeroFila}: ya fue registrada anteriorment." + $"CRE: {nroCre}, Placa {placa}");
                        continue;
                    }

                    var registro = new TbCisternaDetalle
                    {
                        Fecha = fecha,
                        NroCre = nroCre,
                        NroCreFenix = nroCreFenix,

                        PesoKg = pesoKg,
                        PesoTm = pesoTm,

                        VolM3 = volM3,
                        VolBbls = volBbls,

                        Gravedad = gravedad,

                        Cliente = cliente,
                        PlantaDescarga = plantaDescarga,

                        Conductor = conductor,
                        Placa = placa,

                        EmpresaTrans = empresaTrans
                    };
                    registros.Add(registro);
                }
                catch (Exception ex)
                {
                    errores.Add($"Fila {numeroFila}: {ex.Message}");
                }

            }
            if (registros.Count > 0)
            {
                await _context.TbCisternaDetalles.AddRangeAsync(registros);
                await _context.SaveChangesAsync();
            }

            return Ok(new
            {
                exito = true,
                insertados = registros.Count,
                erroresCantidad = errores.Count,
                errores
            });
        }

        [HttpGet("listar")]
        public async Task<IActionResult> Get()
        {
            {
                var listado = await _service.ObtenerListadoAsync();
                return Ok(listado);
            }
        }
        [HttpGet("listSel")]
        public async Task<IActionResult> GetList() 
        {
            var list = await _service.ObtenerListSelectAsync();
            return Ok(list);
        }
    }
}
