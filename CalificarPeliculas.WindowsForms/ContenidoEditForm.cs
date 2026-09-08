using CalificarPeliculas.Application.DTOs;
using CalificarPeliculas.Application.Interfaces.Contenido;
using CalificarPeliculas.Domain;
using System.Windows.Forms;

namespace CalificarPeliculas.WindowsForms
{
    public partial class ContenidoEditForm : Form
    {
        private readonly IContenidoServicio _contenidoServicio;
        private readonly IGeneroServicio _generoServicio;
        private ContenidoDTO? _current;

        public ContenidoEditForm(IContenidoServicio contenidoServicio, IGeneroServicio generoServicio)
        {
            _contenidoServicio = contenidoServicio;
            _generoServicio = generoServicio;
            InitializeComponent();

            comboTipo.Items.AddRange(Enum.GetNames(typeof(TipoContenido)));
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await LoadGenerosAsync();
        }

        private async Task LoadGenerosAsync()
        {
            var generos = await _generoServicio.GetAllAsync();
            comboGenero.DisplayMember = "Nombre";
            comboGenero.ValueMember = "Id";
            comboGenero.DataSource = generos.ToList();
        }

        public void LoadContenido(ContenidoDTO dto)
        {
            _current = dto;
            txtNombre.Text = dto.Nombre;
            txtDescripcion.Text = dto.Descripcion;
            dateFecha.Value = DateOnlyToDateTime(dto.FechaLanzamiento);
            txtDuracion.Text = dto.Duracion.ToString();
            comboTipo.SelectedItem = dto.Tipo.ToString();
            comboGenero.SelectedValue = dto.GeneroId;
            // set UI to Edit mode
            this.Text = "Editar Contenido";
            this.btnOk.Text = "Guardar";
        }

        // Prepare the form for creating a new entity
        public void Clear()
        {
            _current = null;
            txtNombre.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtDirector.Text = string.Empty;
            txtUrlImagen.Text = string.Empty;
            txtPuntuacion.Text = string.Empty;
            numNumEpisodio.Value = 0;
            numNumTemporada.Value = 0;
            numCantTemporadas.Value = 0;
            dateFecha.Value = DateTime.Today;
            txtDuracion.Text = string.Empty;
            comboTipo.SelectedIndex = -1;
            comboGenero.SelectedIndex = -1;
            // set UI to Add mode
            this.Text = "Agregar Contenido";
            this.btnOk.Text = "Agregar";
            // enable all fields by default
            SetAllFieldsEnabled(true);
        }

        private void SetAllFieldsEnabled(bool enabled)
        {
            txtNombre.Enabled = enabled;
            txtDescripcion.Enabled = enabled;
            dateFecha.Enabled = enabled;
            txtDuracion.Enabled = enabled;
            comboTipo.Enabled = enabled;
            comboGenero.Enabled = enabled;
            txtDirector.Enabled = enabled;
            txtUrlImagen.Enabled = enabled;
            txtPuntuacion.Enabled = enabled;
            numNumEpisodio.Enabled = enabled;
            numNumTemporada.Enabled = enabled;
            numCantTemporadas.Enabled = enabled;
        }

        private void UpdateFieldAvailability(TipoContenido tipo)
        {
            // Default: name and description always enabled
            txtNombre.Enabled = true;
            txtDescripcion.Enabled = true;
            comboGenero.Enabled = true;
            txtDirector.Enabled = false;

            switch (tipo)
            {
                case TipoContenido.PELICULA:
                    dateFecha.Enabled = true;
                    txtDuracion.Enabled = true;
                    txtDirector.Enabled = true;
                    txtUrlImagen.Enabled = true;
                    txtPuntuacion.Enabled = true;
                    numNumEpisodio.Enabled = false;
                    numNumTemporada.Enabled = false;
                    numCantTemporadas.Enabled = false;
                    break;
                case TipoContenido.EPISODIO:
                    dateFecha.Enabled = true;
                    txtDuracion.Enabled = true;
                    txtDirector.Enabled = true;
                    txtUrlImagen.Enabled = true;
                    txtPuntuacion.Enabled = true;
                    numNumEpisodio.Enabled = true;
                    numNumTemporada.Enabled = true;
                    numCantTemporadas.Enabled = false;
                    break;
                case TipoContenido.SERIE:
                    dateFecha.Enabled = true;
                    txtDuracion.Enabled = true;
                    txtDirector.Enabled = true;
                    txtUrlImagen.Enabled = true;
                    txtPuntuacion.Enabled = true;
                    numNumEpisodio.Enabled = false;
                    numNumTemporada.Enabled = false;
                    numCantTemporadas.Enabled = true;
                    break;
                default:
                    dateFecha.Enabled = true;
                    txtDuracion.Enabled = true;
                    break;
            }
        }

        private void comboTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboTipo.SelectedItem == null)
            {
                SetAllFieldsEnabled(true);
                return;
            }

            if (Enum.TryParse<TipoContenido>(comboTipo.SelectedItem.ToString(), out var tipo))
            {
                UpdateFieldAvailability(tipo);
            }
            else
            {
                SetAllFieldsEnabled(true);
            }
        }

        private DateTime DateOnlyToDateTime(DateOnly d)
        {
            return new DateTime(d.Year, d.Month, d.Day);
        }

        private DateOnly DateTimeToDateOnly(DateTime dt)
        {
            return DateOnly.FromDateTime(dt);
        }

        private TimeOnly ParseTimeOnly(string s)
        {
            if (TimeOnly.TryParse(s, out var t)) return t;
            return TimeOnly.MinValue;
        }

        private async void btnOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Nombre requerido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (comboTipo.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un tipo de contenido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDirector.Text))
            {
                MessageBox.Show("Nombre del director requerido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtUrlImagen.Text) || !Uri.IsWellFormedUriString(txtUrlImagen.Text, UriKind.Absolute))
            {
                MessageBox.Show("Ingrese una URL de imagen válida.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!float.TryParse(txtPuntuacion.Text, out var ptu) || ptu < 0f || ptu > 10f)
            {
                MessageBox.Show("Ingrese una puntuación válida entre 0 y 10.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var tipoSeleccionado = Enum.Parse<TipoContenido>(comboTipo.SelectedItem.ToString());

            // Duración
            var dur = ParseTimeOnly(txtDuracion.Text);
            if (dur == TimeOnly.MinValue)
            {
                MessageBox.Show("Duración inválida o vacía. Ingrese en formato HH:mm.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Fecha
            var fecha = DateTimeToDateOnly(dateFecha.Value);
            if (fecha == default)
            {
                MessageBox.Show("La fecha de lanzamiento no puede ser la fecha por defecto.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var dto = _current ?? new ContenidoDTO();
            dto.Nombre = txtNombre.Text.Trim();
            dto.NombreDirector = txtDirector.Text.Trim();
            dto.UrlImagen = txtUrlImagen.Text.Trim();
            dto.PuntuacionPromedio = ptu;
            dto.Descripcion = txtDescripcion.Text;
            dto.FechaLanzamiento = fecha;
            dto.Duracion = dur;
            dto.Tipo = tipoSeleccionado;
            dto.GeneroId = comboGenero.SelectedValue != null ? (int)comboGenero.SelectedValue : 0;

            // episode/season values: set according to type requirements
            if (tipoSeleccionado == TipoContenido.EPISODIO)
            {
                dto.NumeroEpisodio = (int)numNumEpisodio.Value;
                dto.NumeroTemporada = (int)numNumTemporada.Value;
                dto.CantTemporadas = null;
            }
            else if (tipoSeleccionado == TipoContenido.SERIE)
            {
                dto.CantTemporadas = (int)numCantTemporadas.Value;
                dto.NumeroEpisodio = null;
                dto.NumeroTemporada = null;
            }
            else
            {
                dto.NumeroEpisodio = null;
                dto.NumeroTemporada = null;
                dto.CantTemporadas = null;
            }

            if (_current == null)
            {
                await _contenidoServicio.AddAsync(dto);
            }
            else
            {
                await _contenidoServicio.UpdateAsync(dto);
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
