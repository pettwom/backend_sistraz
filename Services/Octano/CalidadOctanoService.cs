using backend_trazabilidad.DTOs.Octano;
using backend_trazabilidad.DTOs.Oracle;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Globalization;


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

            string connectionString = _configuration.GetConnectionString("ConexionOctabo")
                ?? throw new InvalidOperationException(
                    "No existe ConnectionStrings:ConexionOctabo.");

            try
            {
                await using var connection =
                    new OracleConnection(connectionString);

                await connection.OpenAsync();

                _logger.LogInformation(
                    "Oracle OCTANO conectado. Tabla={Tabla}, Entidad={Entidad}",
                    idTablaEspecificacion,
                    idEntidad
                );

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

        public async Task<List<ReporteCalidadDto>>
    ObtenerReporteCalidadAsync(
        string credencial,
        string cite)
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

        private static DateTime? GetDateTime(
            OracleDataReader reader,
            string columnName)
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