using CalificarPeliculas.Application.DTOs;
using CalificarPeliculas.Application.Interfaces.Contenido;
using CalificarPeliculas.Application.Interfaces.Criticas;
using CalificarPeliculas.Application.Interfaces.Usuario;
using System.Windows.Forms;

namespace CalificarPeliculas.WindowsForms
{
    public partial class CriticaEditForm : Form
    {
        private readonly ICriticaServicio _criticaServicio;
        private readonly IUsuarioServicio _usuarioServicio;
        private readonly IContenidoServicio _contenidoServicio;
        private CriticaDTO? _current;

        // Maestros a preseleccionar una vez cargados los combos
        private int _usuarioIdInicial;
        private int _contenidoIdInicial;

        public CriticaEditForm(ICriticaServicio criticaServicio, IUsuarioServicio usuarioServicio, IContenidoServicio contenidoServicio)
        {
            _criticaServicio = criticaServicio;
            _usuarioServicio = usuarioServicio;
            _contenidoServicio = contenidoServicio;
            InitializeComponent();
        }

        // Se recargan los maestros cada vez que se muestra el diálogo
        // (el formulario se reutiliza desde el contenedor de DI).
        protected override async void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (this.Visible)
            {
                await LoadMaestrosAsync();
            }
        }

        private async Task LoadMaestrosAsync()
        {
            var usuarios = await _usuarioServicio.GetAllAsync();
            comboUsuario.DisplayMember = "Nombre";
            comboUsuario.ValueMember = "Id";
            comboUsuario.DataSource = usuarios
                .OrderBy(u => u.NombreUsuario)
                .Select(u => new ComboItem { Id = u.Id, Nombre = u.NombreUsuario })
                .ToList();

            var contenidos = await _contenidoServicio.GetAllAsync();
            comboContenido.DisplayMember = "Nombre";
            comboContenido.ValueMember = "Id";
            comboContenido.DataSource = contenidos
                .OrderBy(c => c.Nombre)
                .Select(c => new ComboItem { Id = c.Id, Nombre = $"{c.Nombre} ({c.Tipo})" })
                .ToList();

            SeleccionarMaestro(comboUsuario, _usuarioIdInicial);
            SeleccionarMaestro(comboContenido, _contenidoIdInicial);
        }

        private static void SeleccionarMaestro(ComboBox combo, int id)
        {
            if (id > 0)
                combo.SelectedValue = id;
            else
                combo.SelectedIndex = -1;
        }

        public void LoadCritica(CriticaDTO dto)
        {
            _current = dto;
            _usuarioIdInicial = dto.UsuarioId;
            _contenidoIdInicial = dto.ContenidoId;
            dateFecha.Value = DateOnlyToDateTime(dto.FechaPublicacion);
            numCalificacion.Value = (decimal)dto.Calificacion;
            numReacciones.Value = dto.CantReacciones;
            txtTexto.Text = dto.Texto;
            // En edición no se cambian los maestros de la crítica
            comboUsuario.Enabled = false;
            comboContenido.Enabled = false;
            // set UI to Edit mode
            this.Text = "Editar Crítica";
            this.btnOk.Text = "Guardar";
        }

        // Prepare the form for creating a new entity.
        // Opcionalmente recibe los maestros (Usuario / Contenido) seleccionados en el listado.
        public void Clear(int usuarioId = 0, int contenidoId = 0)
        {
            _current = null;
            _usuarioIdInicial = usuarioId > 0 ? usuarioId : (CurrentSession.User?.Id ?? 0);
            _contenidoIdInicial = contenidoId;
            dateFecha.Value = DateTime.Today;
            numCalificacion.Value = 0;
            numReacciones.Value = 0;
            txtTexto.Text = string.Empty;
            comboUsuario.Enabled = true;
            comboContenido.Enabled = true;
            // set UI to Add mode
            this.Text = "Agregar Crítica";
            this.btnOk.Text = "Agregar";
        }

        private DateTime DateOnlyToDateTime(DateOnly d)
        {
            return new DateTime(d.Year, d.Month, d.Day);
        }

        private async void btnOk_Click(object sender, EventArgs e)
        {
            if (comboUsuario.SelectedValue is not int usuarioId || usuarioId <= 0)
            {
                MessageBox.Show("Seleccione un usuario.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (comboContenido.SelectedValue is not int contenidoId || contenidoId <= 0)
            {
                MessageBox.Show("Seleccione un contenido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var texto = txtTexto.Text?.Trim();
            if (string.IsNullOrWhiteSpace(texto))
            {
                MessageBox.Show("Ingrese el texto de la crítica.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var fecha = DateOnly.FromDateTime(dateFecha.Value);
            if (fecha > DateOnly.FromDateTime(DateTime.Today))
            {
                MessageBox.Show("La fecha de publicación no puede ser futura.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var dto = _current ?? new CriticaDTO();
            dto.UsuarioId = usuarioId;
            dto.ContenidoId = contenidoId;
            dto.FechaPublicacion = fecha;
            dto.Texto = texto;
            dto.Calificacion = (float)numCalificacion.Value;
            dto.CantReacciones = (int)numReacciones.Value;

            try
            {
                if (_current == null)
                {
                    await _criticaServicio.AddAsync(dto);
                }
                else
                {
                    await _criticaServicio.UpdateAsync(dto);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo guardar la crítica: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
