using backend_trazabilidad.Models.Hydro;


namespace backend_trazabilidad.Services.Hydro
{
    public class HydroSoapService : IHydroSoapService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<HydroSoapService> _logger;


        public HydroSoapService(IConfiguration configuration, ILogger<HydroSoapService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        private string Credencial => _configuration["Hydro:Credencial"] ?? throw new Exception("Hydro:Credencial no configurado");

        private decimal IdModulo
        {
            get
            {
                var valor = _configuration["Hydro:IdModulo"];
                if (!decimal.TryParse(valor, out var idModulo))
                {
                    throw new Exception("Hydro:IdModulo inválido");
                }
                return idModulo;
            }
        }


        public async Task<HydroAuthResult?> AutenticarAsync(string usuario, string password)
        {
            try
            {
                /*
                 * Cambia ServicioHydroSesionClient
                 * por el nombre exacto que genere
                 * Connected Services.
                 */
                var client = new HydroSoapReference.ServicioHydroSesionClient();

                var request =
                            new HydroSoapReference.AutenticarRequest
                            {
                                strCredencial = Credencial,
                                strUsuario = usuario,
                                strClave = password,
                                strMensajeError = ""
                            };
                /*
                 * Dependiendo del WSDL,
                 * Visual Studio podría generar
                 * una firma ligeramente diferente.
                 */
                var respuesta = await client.AutenticarAsync(request);
                /*
                 * ADAPTAR estos nombres
                 * solamente si Connected Services
                 * los genera diferentes.
                 */
                var resultado = respuesta.AutenticarResult;
                if (resultado == null)
                {
                    await client.CloseAsync();
                    return null;
                }

                var authResult =
             new HydroAuthResult
             {
                 IdUsuario =
                     resultado.ID_USUARIO,

                 IdEntidad =
                     resultado.ID_ENTIDAD,

                 NombreCompleto =
                     resultado.NOMBRE_COMPLETO
                     ?? string.Empty,

                 Perfil =
                     resultado.PERFIL
                     ?? string.Empty
             };

                await client.CloseAsync();

                return authResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error SOAP al autenticar {Usuario}", usuario);
                throw;
            }
        }


        public async Task<List<string>>ObtenerPerfilesAsync(decimal idUsuario)
        {
            try
            {
                var client =
                    new HydroSoapReference
                        .ServicioHydroSesionClient();


                var request =
                    new HydroSoapReference
                        .ObternerPerfilesUsuarioRequest
                    {
                        decIdUsuario =
                            idUsuario,

                        decIdModulo =
                            IdModulo,

                        credencial =
                            Credencial,

                        mensajeError =
                            ""
                    };


                var respuesta =
                    await client
                        .ObternerPerfilesUsuarioAsync(
                            request
                        );


                var resultado =
                    respuesta
                        .ObternerPerfilesUsuarioResult;


                var perfiles =
                    new List<string>();


                if (resultado == null)
                {
                    return perfiles;
                }


                foreach (var perfil in resultado)
                {
                    if (!string.IsNullOrWhiteSpace(
                            perfil.DESCRIPCION))
                    {
                        perfiles.Add(
                            perfil.DESCRIPCION
                        );
                    }
                }


                await client.CloseAsync();


                return perfiles;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error obteniendo perfiles HYDRO para usuario {IdUsuario}",
                    idUsuario
                );

                throw;
            }
        }
    }
}
