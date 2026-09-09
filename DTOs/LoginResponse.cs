namespace backend_trazabilidad.DTOs
{
    public class LoginResponse
    {
        public bool Ok { get; set; }
        public string Mensaje { get; set; }
        public string? Token { get; set; }
        public Object? Usuario { get; set; }
    }
}
