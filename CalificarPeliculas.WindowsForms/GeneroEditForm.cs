using CalificarPeliculas.Application.DTOs;
using CalificarPeliculas.Application.Interfaces.Contenido;
using System.Windows.Forms;

namespace CalificarPeliculas.WindowsForms
{
    public partial class GeneroEditForm : Form
    {
        private readonly IGeneroServicio _generoServicio;
        private GeneroDTO? _current;

        public GeneroEditForm(IGeneroServicio generoServicio)
        {
            _generoServicio = generoServicio;
            InitializeComponent();
        }

        public void LoadGenero(GeneroDTO dto)
        {
            _current = dto;
            txtNombre.Text = dto.Nombre;
            // set UI to Edit mode
            this.Text = "Editar Genero";
            this.btnOk.Text = "Guardar";
        }

        // Prepare the form for creating a new entity
        public void Clear()
        {
            _current = null;
            txtNombre.Text = string.Empty;
            // set UI to Add mode
            this.Text = "Agregar Genero";
            this.btnOk.Text = "Agregar";
        }

        private async void btnOk_Click(object sender, EventArgs e)
        {
            var nombre = txtNombre.Text?.Trim();
            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("Ingrese un nombre.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_current == null)
            {
                var dto = new GeneroDTO { Nombre = nombre };
                await _generoServicio.AddAsync(dto);
            }
            else
            {
                _current.Nombre = nombre;
                await _generoServicio.UpdateAsync(_current);
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
