namespace CalificarPeliculas.Application.DTOs
{
    public class LoginResponseDTO
    {
        public required string Token { get; set; }
        public DateTime ExpiraEn { get; set; }
        public required UsuarioDTO Usuario { get; set; }
    }
}
