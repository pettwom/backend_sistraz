using backend_trazabilidad.DTOs;
using backend_trazabilidad.Services.Hydro;
using backend_trazabilidad.Services.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace backend_trazabilidad.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IHydroSoapService
            _hydroSoapService;

        private readonly IHydroRestService
            _hydroRestService;

        private readonly IJwtService
            _jwtService;

        private readonly ILogger<AuthService>
            _logger;


        public AuthService(
            IHydroSoapService hydroSoapService,
            IHydroRestService hydroRestService,
            IJwtService jwtService,
            ILogger<AuthService> logger)
        {
            _hydroSoapService = hydroSoapService;

            _hydroRestService = hydroRestService;

            _jwtService = jwtService;

            _logger = logger;
        }


        public async Task<LoginResponse>LoginAsync(LoginRequest request)
        {
            try
            {
                /*
                 * =========================
                 * VALIDACIONES
                 * =========================
                 */

                if (string.IsNullOrWhiteSpace(
                    request.Usuario))
                {
                    return Error(
                        "Debe ingresar el usuario."
                    );
                }


                if (string.IsNullOrWhiteSpace(
                    request.Password))
                {
                    return Error(
                        "Debe ingresar la contraseña."
                    );
                }


                /*
                 * =========================
                 * NORMALIZAR USUARIO
                 * =========================
                 */

                var usuario =
                    NormalizarUsuario(
                        request.Usuario
                    );


                /*
                 * =========================
                 * PASSWORD MD5
                 * =========================
                 */

                var passwordMd5 =
                    GenerarMd5(
                        request.Password
                    );


                /*
                 * =========================
                 * AUTENTICAR SOAP
                 * =========================
                 */

                var auth =
                    await _hydroSoapService
                        .AutenticarAsync(
                            usuario,
                            passwordMd5
                        );


                if (auth == null)
                {
                    return Error(
                        "No se pudo validar el usuario."
                    );
                }


                if (auth.Perfil.Equals(
                        "USUARIO NO VALIDO",
                        StringComparison
                            .OrdinalIgnoreCase))
                {
                    return Error(
                        "Usuario o contraseña inválidos."
                    );
                }


                if (!auth.IdUsuario.HasValue)
                {
                    return Error(
                        "No se pudo obtener el identificador del usuario."
                    );
                }


                if (string.IsNullOrWhiteSpace(
                    auth.NombreCompleto))
                {
                    return Error(
                        "No se pudo obtener el nombre del usuario."
                    );
                }


                var idUsuario =
                    auth.IdUsuario.Value;


                /*
                 * =====================================
                 * DETERMINAR SI ES USUARIO ANH
                 * =====================================
                 */

                bool usuarioAnh =
                    usuario.EndsWith(
                        "@ANH.GOB.BO",
                        StringComparison
                            .OrdinalIgnoreCase
                    );


                var usuarioDto =
                    new UsuarioDto
                    {
                        IdUsuarioHydro =
                            idUsuario,

                        IdEntidad =
                            auth.IdEntidad,

                        NombreCompleto =
                            auth.NombreCompleto,

                        Correo =
                            usuario,

                        CantidadBandeja =
                            0
                    };


                /*
                 * =====================================
                 * USUARIO ANH
                 * =====================================
                 */

                if (usuarioAnh)
                {
                    var funcionarioResponse =
                        await _hydroRestService
                            .ObtenerFuncionarioAsync(
                                idUsuario
                            );


                    var funcionario =
                        funcionarioResponse
                            ?.oResultado;


                    if (funcionario == null)
                    {
                        return Error(
                            "No se encontraron datos del funcionario."
                        );
                    }


                    /*
                     * Funcionario pasivo
                     */

                    if (funcionario.Estado
                        ?.Equals(
                            "P",
                            StringComparison
                                .OrdinalIgnoreCase
                        ) == true)
                    {
                        return Error(
                            "El funcionario está en estado pasivo."
                        );
                    }


                    /*
                     * ==========================
                     * LISTA FUNCIONARIOS
                     * ==========================
                     */

                    var listado =
                        await _hydroRestService
                            .ObtenerFuncionariosActivosAsync();


                    var encontrado =
                        listado?.oResultado
                            .FirstOrDefault(
                                x =>
                                    x.UsuarioHydroId
                                    == idUsuario
                            );


                    if (encontrado == null)
                    {
                        return Error(
                            "No se encontró la unidad del funcionario."
                        );
                    }


                    /*
                     * ==========================
                     * FOTO
                     * ==========================
                     */

                    string? fotografia = null;


                    if (funcionario
                        .FotoDigitalId
                        .HasValue)
                    {
                        fotografia =
                            await _hydroRestService
                                .ObtenerFotosAsync(
                                    funcionario
                                        .FotoDigitalId
                                        .Value
                                );
                    }


                    /*
                     * ==========================
                     * DATOS DEL FUNCIONARIO
                     * ==========================
                     */

                    usuarioDto.Direccion =
                        encontrado.Direccion;

                    usuarioDto
                        .DenominacionUnidad =
                        encontrado
                            .DenominacionUnidad;

                    usuarioDto.Cargo =
                        encontrado.Cargo;

                    usuarioDto.Ci =
                        encontrado.NumIdentidad;

                    usuarioDto
                        .IdOrganigrama =
                        funcionario
                            .IdOrganigrama;

                    usuarioDto
                        .Fotografia =
                        fotografia;

                    usuarioDto
                        .Entidad =
                        "USUARIO";
                }
                else
                {
                    /*
                     * =====================================
                     * USUARIO EXTERNO
                     * =====================================
                     */

                    usuarioDto.Entidad =
                        "ENTIDAD";

                    usuarioDto.Fotografia =
                        null;
                }


                /*
                 * =====================================
                 * OBTENER PERFILES
                 * =====================================
                 */

                var perfiles =
                    await _hydroSoapService
                        .ObtenerPerfilesAsync(
                            idUsuario
                        );


                if (perfiles == null ||
                    perfiles.Count == 0)
                {
                    return Error(
                        "No se encontró perfil asignado."
                    );
                }


                usuarioDto.Perfiles =
                    perfiles;


                /*
                 * =====================================
                 * GENERAR JWT
                 * =====================================
                 */

                var token =
                    _jwtService
                        .GenerarToken(
                            usuarioDto
                        );


                /*
                 * =====================================
                 * RESPUESTA
                 * =====================================
                 */

                return new LoginResponse
                {
                    Exito =
                        true,

                    Mensaje =
                        "Inicio de sesión correcto.",

                    Token =
                        token,

                    Expira =
                        DateTime.UtcNow
                            .AddMinutes(60),

                    Usuario =
                        usuarioDto
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error realizando autenticación."
                );


                return new LoginResponse
                {
                    Exito = false,

                    Mensaje =
                        "Ocurrió un error durante la autenticación."
                };
            }
        }


        /*
         * =====================================
         * NORMALIZAR USUARIO
         * =====================================
         */

        private static string
            NormalizarUsuario(
                string usuario)
        {
            const string dominio =
                "ANH.GOB.BO";


            usuario =
                usuario
                    .Trim()
                    .ToUpperInvariant();


            if (!usuario.Contains("@"))
            {
                usuario +=
                    $"@{dominio}";
            }


            return usuario;
        }


        /*
         * =====================================
         * MD5
         * =====================================
         */

        private static string
            GenerarMd5(
                string password)
        {
            using var md5 =
                MD5.Create();


            byte[] inputBytes =
                Encoding.UTF8
                    .GetBytes(password);


            byte[] hashBytes =
                md5.ComputeHash(
                    inputBytes
                );


            return Convert
                .ToHexString(
                    hashBytes
                )
                .ToUpperInvariant();
        }


        /*
         * =====================================
         * RESPUESTA ERROR
         * =====================================
         */

        private static LoginResponse
            Error(string mensaje)
        {
            return new LoginResponse
            {
                Exito = false,
                Mensaje = mensaje
            };
        }
    }
}