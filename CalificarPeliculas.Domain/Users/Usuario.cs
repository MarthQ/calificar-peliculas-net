using System;

namespace CalificarPeliculas.Domain.Users
{
    public class Usuario
    {
        private Rol? _rol;
        private int _rolId;

        public int Id { get; private set; }
        public string NombreUsuario { get; private set; } = null!;
        public string Mail { get; private set; } = null!;

        // Solo almacena el hash generado por la capa de aplicación, nunca la contraseña plana.
        public string PasswordHash { get; private set; } = null!;

        public int RolId => _rolId;
        public Rol? Rol => _rol;

        protected Usuario() { }

        public Usuario(int id, string nombreUsuario, string mail, int rolId)
        {
            SetId(id);
            SetNombreUsuario(nombreUsuario);
            SetMail(mail);
            SetRolId(rolId);
        }

        public void SetId(int id)
        {
            if (id < 0)
                throw new ArgumentException("El id debe ser mayor o igual a 0.", nameof(id));

            Id = id;
        }

        public void SetNombreUsuario(string nombreUsuario)
        {
            if (string.IsNullOrWhiteSpace(nombreUsuario))
                throw new ArgumentException("El nombre de usuario no puede estar vacío.", nameof(nombreUsuario));

            NombreUsuario = nombreUsuario.Trim();
        }

        public void SetMail(string mail)
        {
            var mailNormalizado = mail?.Trim();

            if (string.IsNullOrWhiteSpace(mailNormalizado) ||
                !System.Net.Mail.MailAddress.TryCreate(mailNormalizado, out var direccion) ||
                !string.Equals(direccion.Address, mailNormalizado, StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException("El mail no tiene un formato válido.", nameof(mail));
            }

            Mail = direccion.Address;
        }

        public void SetPasswordHash(string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new ArgumentException("El hash de la contraseña no puede estar vacío.", nameof(passwordHash));

            PasswordHash = passwordHash;
        }

        public void SetRol(Rol rol)
        {
            ArgumentNullException.ThrowIfNull(rol);

            if (rol.Id <= 0)
                throw new ArgumentException("El rol debe tener un id válido.", nameof(rol));

            _rol = rol;
            _rolId = rol.Id;
        }

        public void SetRolId(int rolId)
        {
            if (rolId <= 0)
                throw new ArgumentException("El id del rol debe ser mayor a 0.", nameof(rolId));

            _rolId = rolId;

            if (_rol?.Id != rolId)
                _rol = null;
        }
    }
}
