using System;
using System.Windows.Forms;
using CalificarPeliculas.Application.Interfaces.Usuario;
using CalificarPeliculas.Application.DTOs;

namespace CalificarPeliculas.WindowsForms
{
    public partial class RegisterForm : Form
    {
        private readonly IUsuarioServicio? _usuarioServicio;

        public RegisterForm(IUsuarioServicio? usuarioServicio)
        {
            _usuarioServicio = usuarioServicio;
            InitializeComponent();
        }

        private async void btnRegister_Click(object sender, EventArgs e)
        {
            btnRegister.Enabled = false;

            if (_usuarioServicio == null)
            {
                MessageBox.Show("Servicio de usuarios no disponible.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRegister.Enabled = true;
                return;
            }

            var nombre = txtNombreUsuario.Text?.Trim();
            var mail = txtMail.Text?.Trim();
            var pass = txtPassword.Text;
            var pass2 = txtPasswordConfirm.Text;

            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(mail) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Complete todos los campos.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnRegister.Enabled = true;
                return;
            }

            if (pass != pass2)
            {
                MessageBox.Show("Las contraseñas no coinciden.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnRegister.Enabled = true;
                return;
            }

            try
            {
                var dto = new RegistrarUsuarioDTO
                {
                    NombreUsuario = nombre,
                    Mail = mail,
                    Password = pass,
                    RolId = 2 // rol por defecto: usuario (asegurarse que exista en la base)
                };

                var usuario = await _usuarioServicio.RegistrarAsync(dto);

                CurrentSession.User = usuario; // autologin

                MessageBox.Show("Registro exitoso.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error registrando: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnRegister.Enabled = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // If embedded, just clear parent; if shown as dialog, close
            if (this.Parent != null)
            {
                var p = this.Parent as Control;
                p?.Controls.Remove(this);
                this.Dispose();
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }
    }
}
