using backend_trazabilidad.Converters;
using backend_trazabilidad.DTOs.Octano;
using backend_trazabilidad.DTOs.Oracle;
using DocumentFormat.OpenXml.Spreadsheet;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Net.Http.Json;
using System.Text.Json;
using static System.Net.WebRequestMethods;


namespace backend_trazabilidad.Services.Octano
{
    public class CalidadOctanoService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<CalidadOctanoService> _logger;
        private readonly HttpClient _http;

        public CalidadOctanoService(IConfiguration configuration, ILogger<CalidadOctanoService> logger, HttpClient http)
        {
            _configuration = configuration;
            _logger = logger;
            _http = http;
        }

        public async Task<List<CertificadoAlertaOctanoDto>>
    ListarCertificadosPrincipalAsync(
        string credencial,
        DateTime desde,
        DateTime hasta,
        decimal idEntidad,
        decimal idUsuario,
        bool esSuperAdministrador,
        CancellationToken cancellationToken = default)
        {
            try
            {
                decimal entidadFiltro;
                decimal usuarioFiltro;

                // ============================================
                // 1. DETERMINAR ENTIDAD / USUARIO
                // ============================================

                if (idEntidad == 0)
                {
                    if (esSuperAdministrador)
                    {
                        entidadFiltro = 0;
                        usuarioFiltro = 0;
                    }
                    else
                    {
                        entidadFiltro = 0;
                        usuarioFiltro = idUsuario;
                    }
                }
                else
                {
                    entidadFiltro = 0;
                    usuarioFiltro = 0;
                }

                Debug.WriteLine(
                    $"Entidad={entidadFiltro} - Usuario={usuarioFiltro}"
                );

                // ============================================
                // 2. FORMATEAR FECHAS PARA OCTANO
                // ============================================

                string fechaInicial = desde.ToString(
                    "dd-MM-yyyy",
                    CultureInfo.InvariantCulture
                );

                string fechaFinal = hasta.ToString(
                    "dd-MM-yyyy",
                    CultureInfo.InvariantCulture
                );

                // ============================================
                // 3. CONSTRUIR URL
                // ============================================

                string ruta =
                    $"ReportarCalidadPrincipal/" +
                    $"{Uri.EscapeDataString(credencial)}/" +
                    $"{entidadFiltro}/" +
                    $"{usuarioFiltro}/" +
                    $"{fechaInicial}/" +
                    $"{fechaFinal}/" +
                    $"1?format=json";

                Debug.WriteLine($"Ruta Octano: {ruta}");

                // ============================================
                // 4. OBTENER JSON SIN DESERIALIZAR
                // ============================================

                using var response =
                    await _http.GetAsync(
                        ruta,
                        cancellationToken
                    );

                response.EnsureSuccessStatusCode();

                var json =
                    await response.Content.ReadAsStringAsync(
                        cancellationToken
                    );

                Debug.WriteLine($"JSON OCTANO: {json}");

                // ============================================
                // 5. CONFIGURAR DESERIALIZACIÓN
                // ============================================

                var opciones = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                opciones.Converters.Add(
                    new OctanoDateTimeConverter()
                );

                // ============================================
                // 6. CONVERTIR JSON
                // ============================================

                var certificados =
                    JsonSerializer.Deserialize<
                        List<CertificadoAlertaOctanoDto>
                    >(
                        json,
                        opciones
                    ) ?? [];

                // ============================================
                // 7. FILTRAR ENTIDAD SI CORRESPONDE
                // ============================================
                System.Diagnostics.Debug.WriteLine($"el resultado es : {entidadFiltro}");
                if (entidadFiltro > 0)
                {
                    certificados = certificados
                        .Where(
                            x => x.IdEntidad == entidadFiltro
                        )
                        .ToList();
                }
                System.Diagnostics.Debug.WriteLine($"el resultado es : {certificados}");
                return certificados;
            }
            catch (OperationCanceledException ex)
            {
                throw new TimeoutException(
                    "El servicio Octano no respondió dentro del tiempo permitido.",
                    ex
                );
            }
            catch (JsonException ex)
            {
                throw new Exception(
                    $"Error al convertir la respuesta JSON de Octano: {ex.Message}",
                    ex
                );
            }
        }

        public async Task<List<ParametroCalidadDto>> ObtenerParametrosAsync(string credencial, decimal idTablaEspecificacion, decimal idEntidad, DateTime fecha, string cite = "0")
        {
            var resultado = new List<ParametroCalidadDto>();

            string connectionString = _configuration.GetConnectionString("ConexionOctabo")
                ?? throw new InvalidOperationException(
                    "No existe ConnectionStrings:ConexionOctabo.");

            try
            {
                await using var connection = new OracleConnection(connectionString);

                await connection.OpenAsync();

                _logger.LogInformation(
                    "Oracle OCTANO conectado. Tabla={Tabla}, Entidad={Entidad}",
                    idTablaEspecificacion,
                    idEntidad
                );

                await using var command = connection.CreateCommand();

                command.CommandText = "APP_CANTCAL.PUSR_LISTADOS.P_LISTADO_PRUEBAS_ESPEC";

                command.CommandType = CommandType.StoredProcedure;

                /*
                 * IMPORTANTE:
                 *
                 * Por ahora usamos posición porque todavía debemos
                 * verificar los nombres exactos con ALL_ARGUMENTS.
                 */
                command.BindByName = false;

                // ==========================================
                // 1. CREDENCIAL
                // ==========================================

                command.Parameters.Add(
                        new OracleParameter
                        {
                            ParameterName = "I_CREDENCIAL",
                            OracleDbType = OracleDbType.Varchar2,
                            Direction = ParameterDirection.Input,
                            Value = credencial
                        }

                  );

                // ==========================================
                // 2. ID TABLA ESPECIFICACIÓN
                // ==========================================

                command.Parameters.Add(
                        new OracleParameter
                        {
                            ParameterName = "I_ID_TABLA_ESPEC",
                            OracleDbType = OracleDbType.Decimal,
                            Direction = ParameterDirection.Input,
                            Value = idTablaEspecificacion
                        }
                   );

                // ==========================================
                // 3. ID ENTIDAD
                // ==========================================

                command.Parameters.Add(
                    new OracleParameter
                    {
                        ParameterName = "I_ID_ENTIDAD",
                        OracleDbType = OracleDbType.Decimal,
                        Direction = ParameterDirection.Input,
                        Value = idEntidad
                    }
                    );

                // ==========================================
                // 4. FECHA
                // OCTANO la envía como yyyyMMddHHmmss
                // ==========================================

                decimal fechaOracle =
                    Convert.ToDecimal(
                        fecha.ToString(
                            "yyyyMMddHHmmss",
                            CultureInfo.InvariantCulture
                        )
                    );


                command.Parameters.Add(
                    new OracleParameter
                    {
                        ParameterName = "I_FECHA",
                        OracleDbType = OracleDbType.Decimal,
                        Direction = ParameterDirection.Input,
                        Value = fechaOracle
                    }
                );

                // ==========================================
                // 5. CITE
                // ==========================================

                command.Parameters.Add(
                    new OracleParameter
                    {
                        ParameterName = "I_CITE",
                        OracleDbType = OracleDbType.Varchar2,
                        Direction = ParameterDirection.Input,
                        Value = string.IsNullOrWhiteSpace(cite)
                        ? "0"
                        : cite
                    }
                    );

                // ==========================================
                // 6. CURSOR DE SALIDA
                // ==========================================



                command.Parameters.Add(
                     new OracleParameter
                     {
                         ParameterName =
            "O_TABLA_ESPEC_FORMULARIO_CTY",

                         OracleDbType =
            OracleDbType.RefCursor,

                         Direction =
            ParameterDirection.Output
                     });

                // ==========================================
                // EJECUTAR PROCEDIMIENTO
                // ==========================================

                await using var reader =
                    await command.ExecuteReaderAsync();

                // ==========================================
                // LEER RESULTADO
                // ==========================================
                System.Diagnostics.Debug.WriteLine("================================ ");
                System.Diagnostics.Debug.WriteLine(reader);
                System.Diagnostics.Debug.WriteLine("================================ ");

                while (await reader.ReadAsync())
                {
                    var item = new ParametroCalidadDto
                    {
                        IdPruebaCalidad =
                            GetDecimal(
                                reader,
                                "ID_PRUEBA_CALIDAD"
                            ) ?? 0,

                        IdPruebaCalidadPadre =
                            GetDecimal(
                                reader,
                                "ID_PRUEBA_CALIDAD_PADRE"
                            ),

                        Descripcion =
                            GetString(
                                reader,
                                "DESCRIPCION"
                            ) ?? string.Empty,

                        EspecMinima =
                            GetString(
                                reader,
                                "ESPEC_MINIMA"
                            ),

                        EspecMaxima =
                            GetString(
                                reader,
                                "ESPEC_MAXIMA"
                            ),

                        EspecAlfanumerico =
                            GetString(
                                reader,
                                "ESPEC_ALFANUMERICO"
                            ),

                        MetodoAstm =
                            GetString(
                                reader,
                                "METODO_ASTM"
                            ),

                        UnidadMedida =
                            GetString(
                                reader,
                                "UNIDAD_MEDIDA"
                            ),

                        RangosMultiples =
                            GetString(
                                reader,
                                "RANGOS_MULTIPLES"
                            ),

                        EspecMinimaRango =
                            GetString(
                                reader,
                                "ESPEC_MINIMA_RANGO"
                            ),

                        EspecMaximaRango =
                            GetString(
                                reader,
                                "ESPEC_MAXIMA_RANGO"
                            )
                    };

                    resultado.Add(item);
                }

                _logger.LogInformation(
                    "Se recuperaron {Cantidad} parámetros de calidad.",
                    resultado.Count
                );

                return resultado;
            }
            catch (OracleException ex)
            {
                _logger.LogError(
                    ex,
                    "Error Oracle obteniendo parámetros de calidad. " +
                    "Número Oracle: {Numero}",
                    ex.Number
                );

                throw new Exception(
                    $"Error consultando OCTANO/Oracle. " +
                    $"ORA-{ex.Number}: {ex.Message}",
                    ex
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error obteniendo parámetros de calidad."
                );

                throw;
            }
        }

        public async Task<List<ReporteCalidadDto>> ObtenerReporteCalidadAsync(string credencial, string cite)
        {
            var resultado =
                new List<ReporteCalidadDto>();

            string connectionString =
                _configuration
                    .GetConnectionString("ConexionOctabo")
                ?? throw new InvalidOperationException(
                    "No existe ConexionOctabo."
                );

            await using var connection =
                new OracleConnection(connectionString);

            await connection.OpenAsync();

            await using var command =
                connection.CreateCommand();

            command.CommandText =
                "APP_CANTCAL.PUSR_REPORTES.P_REPORTE_CALIDAD";

            command.CommandType =
                CommandType.StoredProcedure;

            command.BindByName = true;


            // 1. CREDENCIAL

            command.Parameters.Add(
                new OracleParameter(
                    "I_CREDENCIAL",
                    OracleDbType.Varchar2
                )
                {
                    Direction =
                        ParameterDirection.Input,

                    Value =
                        credencial
                }
            );


            // 2. CITE

            command.Parameters.Add(
                new OracleParameter(
                    "I_CITE_GENERADO",
                    OracleDbType.Varchar2
                )
                {
                    Direction =
                        ParameterDirection.Input,

                    Value =
                        cite
                }
            );


            // 3. CURSOR

            command.Parameters.Add(
                new OracleParameter(
                    "O_REPORTE_CALIDAD",
                    OracleDbType.RefCursor
                )
                {
                    Direction =
                        ParameterDirection.Output
                }
            );


            await using var reader =
                await command.ExecuteReaderAsync();


            while (await reader.ReadAsync())
            {
                resultado.Add(
                        new ReporteCalidadDto
                        {
                            IdRegistroCalidad =
                            GetDecimal(
                                reader,
                                "ID_REGISTRO_CALIDAD"
                            ),

                            IdTablaEspec =
                            GetDecimal(
                                reader,
                                "ID_TABLA_ESPEC"
                            ),

                            IdPruebaCalidad =
                            GetDecimal(
                                reader,
                                "ID_PRUEBA_CALIDAD"
                            ),

                            Descripcion =
                            GetString(
                                reader,
                                "DESCRIPCION"
                            ),

                            CodigoAstm =
                            GetString(
                                reader,
                                "CODIGO_ASTM"
                            ),

                            CodigoUnidad =
                            GetString(
                                reader,
                                "CODIGO_UNIDAD"
                            ),

                            ValorAlfanumerico =
                            GetString(
                                reader,
                                "VALOR_ALFANUMERICO"
                            ),

                            IdPuntoCustodio =
                            GetDecimal(
                                reader,
                                "ID_PUNTO_CUSTODIO"
                            ),

                            PuntoCustodio =
                            GetString(
                                reader,
                                "PUNTO_CUSTODIO"
                            ),

                            Volumen =
                            GetDecimal(
                                reader,
                                "VOLUMEN_OP_DEBE"
                            ),

                            IdProducto =
                            GetDecimal(
                                reader,
                                "ID_PRODUCTO"
                            ),

                            Producto =
                            GetString(
                                reader,
                                "PRODUCTO"
                            ),

                            CodigoUnidadVolumen =
                            GetString(
                                reader,
                                "CODIGO_UNIDAD_VOL"
                            ),

                            CiteDocumento =
                            GetString(
                                reader,
                                "CITE_DOCUMENTO"
                            ),

                            IdEntidad =
                            GetDecimal(
                                reader,
                                "ID_ENTIDAD"
                            ),

                            Entidad =
                            GetString(
                                reader,
                                "ENTIDAD"
                            ),

                            FechaOperacion =
                            GetDateTime(
                                reader,
                                "FECHA_OPERACION"
                            ),

                            ValorLote =
                            GetString(
                                reader,
                                "VALOR_LOTE"
                            ),

                            Observaciones =
                            GetString(
                                reader,
                                "OBSERVACIONES"
                            ),

                            ProductoPadre =
                            GetString(
                                reader,
                                "PRODUCTO_PADRE"
                            ),

                            IdTipoActividad =
                            GetDecimal(
                                reader,
                                "ID_TIPO_ACTIVIDAD"
                            ),

                            TipoActividad =
                            GetString(
                                reader,
                                "TIPO_ACTIVIDAD"
                            ),

                            RutaInternacion =
                            GetString(
                                reader,
                                "RUTA_INTERNACION"
                            ),

                            EmpresaProveedora =
                            GetString(
                                reader,
                                "EMPRESA_PROVEEDORA"
                            ),

                            TanqueExterno =
                            GetString(
                                reader,
                                "TK_ORIG_EXTERNO"
                            ),

                            NroLoteVerif =
                            GetString(
                                reader,
                                "NRO_LOTE_VERIF"
                            )
                        }
                );
            }

            return resultado;
        }

        public async Task<List<CertificadoAlertaOctanoDto>> ListarCertificadosAlertaAsync(string credencial, DateTime desde, DateTime hasta, decimal idEntidad, decimal idUsuario, bool esSuperAdministrador, CancellationToken cancellationToken = default)
        {
            // Reproduce los casos del código antiguo:
            // sin entidad y superadministrador: 0/0
            // sin entidad y usuario común: 0/idUsuario
            // con entidad: idEntidad/idUsuario
            decimal entidadOctano = idEntidad;
            decimal usuarioOctano =
                idEntidad == 0 && esSuperAdministrador ? 0 : idUsuario;

            var fechaInicial = desde.Date.ToString(
                "yyyyMMddHHmmss", CultureInfo.InvariantCulture);

            var fechaFinal = hasta.Date.AddDays(1).AddSeconds(-1).ToString(
                "yyyyMMddHHmmss", CultureInfo.InvariantCulture);

            var ruta =
                $"ReportarAlertaCertificado/{Uri.EscapeDataString(credencial)}" +
                $"/{entidadOctano}/{usuarioOctano}" +
                $"/{fechaInicial}/{fechaFinal}?format=json";

            return await _http.GetFromJsonAsync<List<CertificadoAlertaOctanoDto>>(
                ruta, cancellationToken) ?? [];
        }

        // ==================================================
        // MÉTODOS AUXILIARES
        // ==================================================

        private static string? GetString(OracleDataReader reader, string columnName)
        {
            int ordinal;

            try
            {
                ordinal =
                    reader.GetOrdinal(columnName);
            }
            catch (IndexOutOfRangeException)
            {
                return null;
            }

            if (reader.IsDBNull(ordinal))
                return null;

            return Convert.ToString(
                reader.GetValue(ordinal)
            );
        }


        private static decimal? GetDecimal(OracleDataReader reader, string columnName)
        {
            int ordinal;

            try
            {
                ordinal =
                    reader.GetOrdinal(columnName);
            }
            catch (IndexOutOfRangeException)
            {
                return null;
            }

            if (reader.IsDBNull(ordinal))
                return null;

            return Convert.ToDecimal(
                reader.GetValue(ordinal)
            );
        }

        private static DateTime? GetDateTime(OracleDataReader reader, string columnName)
        {
            try
            {
                int ordinal =
                    reader.GetOrdinal(columnName);

                if (reader.IsDBNull(ordinal))
                    return null;

                return Convert.ToDateTime(
                    reader.GetValue(ordinal)
                );
            }
            catch
            {
                return null;
            }
        }
    }
}