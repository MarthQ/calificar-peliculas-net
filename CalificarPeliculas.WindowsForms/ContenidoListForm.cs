using CalificarPeliculas.Application.DTOs;
using CalificarPeliculas.Application.Interfaces.Contenido;
using System.Windows.Forms;

namespace CalificarPeliculas.WindowsForms
{
    public partial class ContenidoListForm : Form
    {
        private readonly IContenidoServicio _contenidoServicio;
        private readonly IServiceProvider _provider;

        public ContenidoListForm(IContenidoServicio contenidoServicio, IServiceProvider provider)
        {
            _contenidoServicio = contenidoServicio;
            _provider = provider;
            InitializeComponent();
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await LoadContenidosAsync();
        }

        private async Task LoadContenidosAsync()
        {
            var items = await _contenidoServicio.GetAllAsync();
            dgvContenidos.DataSource = items.ToList();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            var form = _provider.GetService(typeof(ContenidoEditForm)) as ContenidoEditForm;
            if (form == null)
            {
                MessageBox.Show("No se puede abrir el editor de contenido.");
                return;
            }
            // Ensure form is initialized for Add (clear previous state if DI returned same instance)
            form.Clear();
            if (form.ShowDialog() == DialogResult.OK)
            {
                await LoadContenidosAsync();
            }
        }

        private async void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvContenidos.CurrentRow == null) return;
            var dto = dgvContenidos.CurrentRow.DataBoundItem as ContenidoDTO;
            if (dto == null) return;

            var form = _provider.GetService(typeof(ContenidoEditForm)) as ContenidoEditForm;
            if (form == null)
            {
                MessageBox.Show("No se puede abrir el editor de contenido.");
                return;
            }
            form.LoadContenido(dto);
            if (form.ShowDialog() == DialogResult.OK)
            {
                await LoadContenidosAsync();
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvContenidos.CurrentRow == null) return;
            var dto = dgvContenidos.CurrentRow.DataBoundItem as ContenidoDTO;
            if (dto == null) return;

            var ok = MessageBox.Show($"Eliminar contenido '{dto.Nombre}'?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ok == DialogResult.Yes)
            {
                await _contenidoServicio.DeleteAsync(dto.Id);
                await LoadContenidosAsync();
            }
        }
    }
}
