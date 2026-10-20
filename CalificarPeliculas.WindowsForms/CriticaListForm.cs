using CalificarPeliculas.Application.DTOs;
using CalificarPeliculas.Application.Interfaces.Contenido;
using CalificarPeliculas.Application.Interfaces.Criticas;
using CalificarPeliculas.Application.Interfaces.Usuario;
using System.ComponentModel;
using System.Windows.Forms;

namespace CalificarPeliculas.WindowsForms
{
    // Maestro / Detalle: los maestros son Usuario y Contenido (filtros superiores),
    // el detalle es la grilla de Críticas que pertenecen a los maestros seleccionados.
    public partial class CriticaListForm : Form
    {
        private readonly ICriticaServicio _criticaServicio;
        private readonly IUsuarioServicio _usuarioServicio;
        private readonly IContenidoServicio _contenidoServicio;
        private readonly IServiceProvider _provider;

        private List<UsuarioDTO> _usuarios = new();
        private List<ContenidoDTO> _contenidos = new();
        private bool _cargandoMaestros;

        public CriticaListForm(ICriticaServicio criticaServicio, IUsuarioServicio usuarioServicio,
            IContenidoServicio contenidoServicio, IServiceProvider provider)
        {
            _criticaServicio = criticaServicio;
            _usuarioServicio = usuarioServicio;
            _contenidoServicio = contenidoServicio;
            _provider = provider;
            InitializeComponent();
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await LoadMaestrosAsync();
            await LoadCriticasAsync();
        }

        // Carga los maestros (Usuarios y Contenidos) en los combos de filtro
        private async Task LoadMaestrosAsync()
        {
            _cargandoMaestros = true;
            try
            {
                _usuarios = (await _usuarioServicio.GetAllAsync()).ToList();
                _contenidos = (await _contenidoServicio.GetAllAsync()).ToList();

                var itemsUsuario = new List<ComboItem> { new ComboItem { Id = 0, Nombre = "(Todos)" } };
                itemsUsuario.AddRange(_usuarios
                    .OrderBy(u => u.NombreUsuario)
                    .Select(u => new ComboItem { Id = u.Id, Nombre = u.NombreUsuario }));

                var itemsContenido = new List<ComboItem> { new ComboItem { Id = 0, Nombre = "(Todos)" } };
                itemsContenido.AddRange(_contenidos
                    .OrderBy(c => c.Nombre)
                    .Select(c => new ComboItem { Id = c.Id, Nombre = $"{c.Nombre} ({c.Tipo})" }));

                comboUsuario.DisplayMember = "Nombre";
                comboUsuario.ValueMember = "Id";
                comboUsuario.DataSource = itemsUsuario;

                comboContenido.DisplayMember = "Nombre";
                comboContenido.ValueMember = "Id";
                comboContenido.DataSource = itemsContenido;
            }
            finally
            {
                _cargandoMaestros = false;
            }
        }

        // Carga el detalle (Críticas) filtrado según los maestros seleccionados
        private async Task LoadCriticasAsync()
        {
            var usuarioId = UsuarioSeleccionadoId;
            var contenidoId = ContenidoSeleccionadoId;

            var items = await _criticaServicio.GetAllAsync();
            var filas = items
                .Where(c => usuarioId == 0 || c.UsuarioId == usuarioId)
                .Where(c => contenidoId == 0 || c.ContenidoId == contenidoId)
                .OrderByDescending(c => c.FechaPublicacion)
                .Select(c => new CriticaRow(c,
                    _usuarios.FirstOrDefault(u => u.Id == c.UsuarioId)?.NombreUsuario ?? c.UsuarioId.ToString(),
                    _contenidos.FirstOrDefault(x => x.Id == c.ContenidoId)?.Nombre ?? c.ContenidoId.ToString()))
                .ToList();

            dgvCriticas.DataSource = filas;
            lblTotal.Text = $"Críticas: {filas.Count}";
        }

        private int UsuarioSeleccionadoId => comboUsuario.SelectedValue is int id ? id : 0;
        private int ContenidoSeleccionadoId => comboContenido.SelectedValue is int id ? id : 0;

        private CriticaDTO? CriticaSeleccionada =>
            (dgvCriticas.CurrentRow?.DataBoundItem as CriticaRow)?.Dto;

        private async void comboMaestro_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_cargandoMaestros) return;
            await LoadCriticasAsync();
        }

        private async void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            _cargandoMaestros = true;
            comboUsuario.SelectedIndex = 0;
            comboContenido.SelectedIndex = 0;
            _cargandoMaestros = false;
            await LoadCriticasAsync();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            var form = _provider.GetService(typeof(CriticaEditForm)) as CriticaEditForm;
            if (form == null)
            {
                MessageBox.Show("No se puede abrir el editor de críticas.");
                return;
            }
            // La nueva crítica hereda los maestros seleccionados en los filtros
            form.Clear(UsuarioSeleccionadoId, ContenidoSeleccionadoId);
            if (form.ShowDialog() == DialogResult.OK)
            {
                await LoadCriticasAsync();
            }
        }

        private async void btnEdit_Click(object sender, EventArgs e)
        {
            var dto = CriticaSeleccionada;
            if (dto == null) return;

            var form = _provider.GetService(typeof(CriticaEditForm)) as CriticaEditForm;
            if (form == null)
            {
                MessageBox.Show("No se puede abrir el editor de críticas.");
                return;
            }
            form.LoadCritica(dto);
            if (form.ShowDialog() == DialogResult.OK)
            {
                await LoadCriticasAsync();
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            var row = dgvCriticas.CurrentRow?.DataBoundItem as CriticaRow;
            if (row == null) return;

            var ok = MessageBox.Show($"Eliminar la crítica de '{row.Usuario}' sobre '{row.Contenido}'?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ok == DialogResult.Yes)
            {
                await _criticaServicio.DeleteAsync(row.Dto.Id);
                await LoadCriticasAsync();
            }
        }

        private void dgvCriticas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) btnEdit_Click(sender, e);
        }

        // Fila de la grilla: muestra los nombres de los maestros en lugar de sus IDs
        private class CriticaRow
        {
            public CriticaRow(CriticaDTO dto, string usuario, string contenido)
            {
                Dto = dto;
                Usuario = usuario;
                Contenido = contenido;
            }

            [Browsable(false)]
            public CriticaDTO Dto { get; }

            public int Id => Dto.Id;
            public string Usuario { get; }
            public string Contenido { get; }
            public DateOnly Fecha => Dto.FechaPublicacion;
            public float Calificacion => Dto.Calificacion;
            public int Reacciones => Dto.CantReacciones;
            public string Texto => Dto.Texto;
        }
    }
}
