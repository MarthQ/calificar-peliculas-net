using CalificarPeliculas.Application.DTOs;
using CalificarPeliculas.Application.Interfaces.Usuario;
using CalificarPeliculas.Domain.Users;

namespace CalificarPeliculas.Application.Services
{
    public class UsuarioServicio : IUsuarioServicio
    {
        private readonly IUsuarioRepository usuarioRepository;
        private readonly IPasswordHashService passwordHashService;

        public UsuarioServicio(
            IUsuarioRepository usuarioRepository,
            IPasswordHashService passwordHashService)
        {
            this.usuarioRepository = usuarioRepository;
            this.passwordHashService = passwordHashService;
        }

        public async Task<UsuarioDTO> RegistrarAsync(RegistrarUsuarioDTO dto)
        {
            ValidarPassword(dto.Password);

            var usuario = new Usuario(0, dto.NombreUsuario, dto.Mail, dto.RolId);

            if (await usuarioRepository.ExistsByMailAsync(usuario.Mail))
                throw new InvalidOperationException("Ya existe un usuario registrado con ese mail.");

            if (await usuarioRepository.ExistsByNombreUsuarioAsync(usuario.NombreUsuario))
                throw new InvalidOperationException("El nombre de usuario ya está en uso.");

            if (!await usuarioRepository.RolExistsAsync(usuario.RolId))
                throw new ArgumentException("El rol indicado no existe.", nameof(dto.RolId));

            usuario.SetPasswordHash(passwordHashService.HashPassword(usuario, dto.Password));
            await usuarioRepository.AddAsync(usuario);

            return MapToDTO(usuario);
        }

        public async Task<UsuarioDTO?> GetByIdAsync(int id)
        {
            var usuario = await usuarioRepository.GetByIdAsync(id);
            return usuario == null ? null : MapToDTO(usuario);
        }

        public async Task<IEnumerable<UsuarioDTO>> GetAllAsync()
        {
            var usuarios = await usuarioRepository.GetAllAsync();
            return usuarios.Select(MapToDTO).ToList();
        }

        public async Task<bool> UpdateAsync(ActualizarUsuarioDTO dto)
        {
            var usuario = await usuarioRepository.GetByIdAsync(dto.Id);
            if (usuario == null)
                return false;

            if (!await usuarioRepository.RolExistsAsync(dto.RolId))
                throw new ArgumentException("El rol indicado no existe.", nameof(dto.RolId));

            usuario.SetNombreUsuario(dto.NombreUsuario);
            usuario.SetMail(dto.Mail);
            usuario.SetRolId(dto.RolId);

            var usuarioConMail = await usuarioRepository.GetByMailAsync(usuario.Mail);
            if (usuarioConMail != null && usuarioConMail.Id != dto.Id)
                throw new InvalidOperationException("Ya existe un usuario registrado con ese mail.");

            var usuarioConNombre = await usuarioRepository.GetByNombreUsuarioAsync(usuario.NombreUsuario);
            if (usuarioConNombre != null && usuarioConNombre.Id != dto.Id)
                throw new InvalidOperationException("El nombre de usuario ya está en uso.");

            return await usuarioRepository.UpdateAsync(usuario);
        }

        public Task<bool> DeleteAsync(int id)
        {
            return usuarioRepository.DeleteAsync(id);
        }

        public async Task<UsuarioDTO?> LoginAsync(LoginUsuarioDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Mail) || string.IsNullOrEmpty(dto.Password))
                return null;

            var usuario = await usuarioRepository.GetByMailAsync(dto.Mail);
            if (usuario == null || !passwordHashService.VerifyPassword(usuario, dto.Password))
                return null;

            return MapToDTO(usuario);
        }

        private static void ValidarPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
                throw new ArgumentException("La contraseña debe tener al menos 8 caracteres.", nameof(password));
        }

        private static UsuarioDTO MapToDTO(Usuario usuario)
        {
            return new UsuarioDTO
            {
                Id = usuario.Id,
                NombreUsuario = usuario.NombreUsuario,
                Mail = usuario.Mail,
                RolId = usuario.RolId
            };
        }
    }
}
