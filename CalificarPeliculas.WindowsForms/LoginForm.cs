using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using CalificarPeliculas.Application.Interfaces.Usuario;
using CalificarPeliculas.Application.DTOs;

namespace CalificarPeliculas.WindowsForms
{
    public partial class LoginForm : Form
    {
        private readonly IUsuarioServicio? _usuarioServicio;

        public LoginForm(IUsuarioServicio? usuarioServicio)
        {
            _usuarioServicio = usuarioServicio;
            InitializeComponent();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            await HandleLoginAsync();
        }

        private async Task HandleLoginAsync()
        {
            btnLogin.Enabled = false;

            if (_usuarioServicio == null)
            {
                MessageBox.Show("El servicio de usuarios no está configurado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnLogin.Enabled = true;
                this.DialogResult = DialogResult.Cancel;
                this.Close();
                return;
            }

            var mail = txtMail.Text.Trim();
            var password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(mail) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Complete mail y contraseña.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnLogin.Enabled = true;
                return;
            }

            var dto = new LoginUsuarioDTO { Mail = mail, Password = password };

            UsuarioDTO? usuario = null;
            try
            {
                usuario = await _usuarioServicio.LoginAsync(dto);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al autenticar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnLogin.Enabled = true;
                return;
            }

            if (usuario == null)
            {
                MessageBox.Show("Credenciales inválidas.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnLogin.Enabled = true;
                return;
            }

            // Guardar sesión
            CurrentSession.User = usuario;

            // Verificar rol superadmin (RolId == 1)
            if (usuario.RolId != 1)
            {
                MessageBox.Show("Acceso denegado. Se requiere rol de administrador.", "No autorizado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnLogin.Enabled = true;
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
