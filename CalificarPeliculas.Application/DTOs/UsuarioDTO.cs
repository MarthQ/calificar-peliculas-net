using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalificarPeliculas.Application.DTOs
{
    public class RegistrarUsuarioDTO
    {
        public required string NombreUsuario { get; set; }
        public required string Mail { get; set; }
        public required string Password { get; set; }
        public int RolId { get; set; }
    }

    public class LoginUsuarioDTO
    {
        public required string Mail { get; set; }
        public required string Password { get; set; }
    }

    public class UsuarioDTO
    {
        public int Id { get; set; }
        public required string NombreUsuario { get; set; }
        public required string Mail { get; set; }
        public int RolId { get; set; }
    }

    public class ActualizarUsuarioDTO
    {
        public int Id { get; set; }
        public required string NombreUsuario { get; set; }
        public required string Mail { get; set; }
        public int RolId { get; set; }
    }
}
