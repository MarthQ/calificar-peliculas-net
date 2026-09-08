using System;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;

namespace CalificarPeliculas.WindowsForms
{
    public partial class Form1 : Form
    {
        private readonly IServiceProvider _provider;

        public Form1(IServiceProvider provider)
        {
            _provider = provider;
            InitializeComponent();
        }

        private void ShowInPanel(Form f)
        {
            if (f == null) return;
            f.TopLevel = false;
            f.FormBorderStyle = FormBorderStyle.None;
            f.Dock = DockStyle.Fill;
            panelMain.Controls.Clear();
            panelMain.Controls.Add(f);
            f.Show();
        }

        private void btnGeneros_Click(object sender, EventArgs e)
        {
            var form = _provider.GetService(typeof(GeneroListForm)) as GeneroListForm;
            if (form == null)
            {
                MessageBox.Show("No se pudo abrir la ventana de géneros.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ShowInPanel(form);
        }

        private void btnContenidos_Click(object sender, EventArgs e)
        {
            var form = _provider.GetService(typeof(ContenidoListForm)) as ContenidoListForm;
            if (form == null)
            {
                MessageBox.Show("No se pudo abrir la ventana de contenidos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ShowInPanel(form);
        }

        private void menuRegisterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = _provider.GetService(typeof(RegisterForm)) as RegisterForm;
            if (form == null)
            {
                MessageBox.Show("No se pudo abrir el formulario de registro.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ShowInPanel(form);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            // Clear session and return to login screen. If login is successful, show main again; otherwise exit.
            CurrentSession.User = null;

            // Hide main form while login dialog is shown
            this.Hide();

            if (Program.ServiceProvider == null)
            {
                System.Windows.Forms.Application.Exit();
                return;
            }

            using var scope = Program.ServiceProvider.CreateScope();
            var provider = scope.ServiceProvider;
            var login = provider.GetService<LoginForm>();
            if (login == null)
            {
                System.Windows.Forms.Application.Exit();
                return;
            }

            var dr = login.ShowDialog();
            if (dr == DialogResult.OK)
            {
                // logged in again
                this.Show();
            }
            else
            {
                System.Windows.Forms.Application.Exit();
            }
        }
    }
}
