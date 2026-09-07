using CalificarPeliculas.Application.DTOs;
using CalificarPeliculas.Application.Interfaces.Contenido;
using System.Windows.Forms;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CalificarPeliculas.WindowsForms
{
    public partial class GeneroListForm : Form
    {
        private readonly IGeneroServicio _generoServicio;
        private readonly IServiceProvider _provider;

        public GeneroListForm(IGeneroServicio generoServicio, IServiceProvider provider)
        {
            _generoServicio = generoServicio;
            _provider = provider;
            InitializeComponent();
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await LoadGenerosAsync();
        }

        private async Task LoadGenerosAsync()
        {
            var items = await _generoServicio.GetAllAsync();
            dgvGeneros.DataSource = items.ToList();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            var form = _provider.GetService(typeof(GeneroEditForm)) as GeneroEditForm;
            if (form == null)
            {
                MessageBox.Show("No se puede abrir el editor de género.");
                return;
            }
            form.Clear();
            if (form.ShowDialog() == DialogResult.OK)
            {
                await LoadGenerosAsync();
            }
        }

        private async void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvGeneros.CurrentRow == null) return;
            var dto = dgvGeneros.CurrentRow.DataBoundItem as GeneroDTO;
            if (dto == null) return;

            var form = _provider.GetService(typeof(GeneroEditForm)) as GeneroEditForm;
            if (form == null)
            {
                MessageBox.Show("No se puede abrir el editor de género.");
                return;
            }
            form.LoadGenero(dto);
            if (form.ShowDialog() == DialogResult.OK)
            {
                await LoadGenerosAsync();
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvGeneros.CurrentRow == null) return;
            var dto = dgvGeneros.CurrentRow.DataBoundItem as GeneroDTO;
            if (dto == null) return;

            var ok = MessageBox.Show($"Eliminar género '{dto.Nombre}'?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ok == DialogResult.Yes)
            {
                await _generoServicio.DeleteAsync(dto.Id);
                await LoadGenerosAsync();
            }
        }
    }
}
