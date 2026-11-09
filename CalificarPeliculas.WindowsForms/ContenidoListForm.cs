using CalificarPeliculas.Application.DTOs;
using CalificarPeliculas.Application.Interfaces.Contenido;
using System.Windows.Forms;

namespace CalificarPeliculas.WindowsForms
{
    public partial class ContenidoListForm : Form
    {
        private readonly IContenidoServicio _contenidoServicio;
        private readonly IGeneroServicio _generoServicio;
        private readonly IServiceProvider _provider;
        private List<ContenidoDTO> _contenidos = new();

        public ContenidoListForm(IContenidoServicio contenidoServicio, IGeneroServicio generoServicio, IServiceProvider provider)
        {
            _contenidoServicio = contenidoServicio;
            _generoServicio = generoServicio;
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
            _contenidos = (await _contenidoServicio.GetAllAsync()).ToList();
            var generos = (await _generoServicio.GetAllAsync())
                .OrderBy(g => g.Nombre)
                .Select(g => new ComboItem { Id = g.Id, Nombre = g.Nombre })
                .ToList();
            generos.Insert(0, new ComboItem { Id = 0, Nombre = "Todos los géneros" });
            comboGenero.DataSource = generos;
            comboGenero.SelectedIndex = 0;
            AplicarFiltro();
        }

        private void AplicarFiltro()
        {
            var generoId = comboGenero.SelectedValue is int id ? id : 0;
            var nombre = txtFiltroNombre.Text.Trim();
            dgvContenidos.DataSource = _contenidos
                .Where(c => (generoId == 0 || c.GeneroId == generoId) &&
                            (string.IsNullOrWhiteSpace(nombre) || c.Nombre.Contains(nombre, StringComparison.OrdinalIgnoreCase)))
                .ToList();
        }

        private void comboGenero_SelectedIndexChanged(object sender, EventArgs e)
        {
            AplicarFiltro();
        }

        private void txtFiltroNombre_TextChanged(object sender, EventArgs e)
        {
            AplicarFiltro();
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
