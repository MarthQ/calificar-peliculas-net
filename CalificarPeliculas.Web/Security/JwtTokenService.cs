using CalificarPeliculas.Application.Interfaces.Usuario;
using CalificarPeliculas.Domain.Users;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CalificarPeliculas.Web.Security
{
    public class JwtTokenService : ITokenService
    {
        private readonly IConfiguration configuration;

        public JwtTokenService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GenerateToken(Usuario usuario)
        {
            var jwt = configuration.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, usuario.Mail),
                new Claim(ClaimTypes.Name, usuario.NombreUsuario),
                new Claim(ClaimTypes.Role, usuario.RolId.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: jwt["Issuer"],
                audience: jwt["Audience"],
                claims: claims,
                expires: GetExpiration(),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public DateTime GetExpiration()
        {
            var minutos = configuration.GetValue<int?>("Jwt:ExpiraEnMinutos") ?? 60;
            return DateTime.UtcNow.AddMinutes(minutos);
        }
    }
}
