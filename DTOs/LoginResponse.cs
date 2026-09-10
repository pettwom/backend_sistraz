namespace backend_trazabilidad.DTOs
{
    public class LoginResponse
    {
        public bool Exito { get; set; }
        public string Mensaje { get; set; } = string.Empty;
        public string? Token { get; set; }
        public UsuarioDto? Usuario { get; set; }
        public DateTime? Expira { get; set; }
    }
}
