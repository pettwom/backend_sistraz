using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Globalization;
using backend_trazabilidad.DTOs.Oracle;


namespace backend_trazabilidad.Services.Octano
{
    public class CalidadOctanoService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<CalidadOctanoService> _logger;

        public CalidadOctanoService(
            IConfiguration configuration,
            ILogger<CalidadOctanoService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene las pruebas/parámetros de calidad configurados
        /// en el sistema OCTANO.
        /// </summary>
        public async Task<List<ParametroCalidadDto>> ObtenerParametrosAsync(
            string credencial,
            decimal idTablaEspecificacion,
            decimal idEntidad,
            DateTime fecha,
            string cite = "0")
        {
            var resultado = new List<ParametroCalidadDto>();

            string connectionString =_configuration.GetConnectionString("ConexionOctabo")
                ?? throw new InvalidOperationException(
                    "No existe ConnectionStrings:ConexionOctabo.");

            try
            {
                await using var connection =
                    new OracleConnection(connectionString);

                await connection.OpenAsync();

                _logger.LogInformation(
                    "Conexión Oracle abierta correctamente.");

                await using var command =
                    connection.CreateCommand();

                command.CommandText =
                    "APP_CANTCAL.PUSR_LISTADOS.P_LISTADO_PRUEBAS_ESPEC";

                command.CommandType =
                    CommandType.StoredProcedure;

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
                        OracleDbType = OracleDbType.Varchar2,

                        Direction =
                            ParameterDirection.Input,

                        Value = credencial
                    });

                // ==========================================
                // 2. ID TABLA ESPECIFICACIÓN
                // ==========================================

                command.Parameters.Add(
                    new OracleParameter
                    {
                        OracleDbType = OracleDbType.Decimal,

                        Direction =
                            ParameterDirection.Input,

                        Value = idTablaEspecificacion
                    });

                // ==========================================
                // 3. ID ENTIDAD
                // ==========================================

                command.Parameters.Add(
                    new OracleParameter
                    {
                        OracleDbType = OracleDbType.Decimal,

                        Direction =
                            ParameterDirection.Input,

                        Value = idEntidad
                    });

                // ==========================================
                // 4. FECHA
                // OCTANO la envía como yyyyMMddHHmmss
                // ==========================================

                decimal fechaOracle = Convert.ToDecimal(
                    fecha.ToString(
                        "yyyyMMddHHmmss",
                        CultureInfo.InvariantCulture
                    )
                );

                command.Parameters.Add(
                    new OracleParameter
                    {
                        OracleDbType = OracleDbType.Decimal,

                        Direction =
                            ParameterDirection.Input,

                        Value = fechaOracle
                    });

                // ==========================================
                // 5. CITE
                // ==========================================

                command.Parameters.Add(
                    new OracleParameter
                    {
                        OracleDbType = OracleDbType.Decimal,

                        Direction =
                            ParameterDirection.Input,

                        Value = string.IsNullOrWhiteSpace(cite)
                            ? "0"
                            : cite
                    });

                // ==========================================
                // 6. CURSOR DE SALIDA
                // ==========================================

                command.Parameters.Add(
                    new OracleParameter
                    {
                        OracleDbType = OracleDbType.RefCursor,

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


        // ==================================================
        // MÉTODOS AUXILIARES
        // ==================================================

        private static string? GetString(
            OracleDataReader reader,
            string columnName)
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


        private static decimal? GetDecimal(
            OracleDataReader reader,
            string columnName)
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
    }
}