using System.Windows.Forms;
using CalificarPeliculas.Application.DTOs;
using CalificarPeliculas.Application.Interfaces.Grupo;

namespace CalificarPeliculas.WindowsForms;

public sealed class GrupoListForm : Form
{
    private readonly IGrupoServicio servicio;
    private readonly IServiceProvider provider;
    private readonly DataGridView grid = new() { Dock = DockStyle.Fill, ReadOnly = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect, AutoGenerateColumns = true };
    private List<GrupoDTO> grupos = new();

    public GrupoListForm(IGrupoServicio servicio, IServiceProvider provider)
    {
        this.servicio = servicio; this.provider = provider; Text = "Grupos"; Width = 900; Height = 500;
        var buttons = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 42 };
        foreach (var item in new[] { ("Agregar", (EventHandler)Add), ("Editar", Edit), ("Eliminar", Delete), ("Cerrar", (_, _) => Close()) }) { var b = new Button { Text = item.Item1, Width = 100 }; b.Click += item.Item2; buttons.Controls.Add(b); }
        Controls.Add(grid); Controls.Add(buttons); Load += async (_, _) => await LoadData();
    }
    private async Task LoadData() { grupos = (await servicio.GetAllAsync()).ToList(); grid.DataSource = grupos; }
    private GrupoDTO? Selected => grid.CurrentRow?.DataBoundItem as GrupoDTO;
    private void Add(object? s, EventArgs e) { var form = provider.GetService(typeof(GrupoEditForm)) as GrupoEditForm; form?.Clear(); if (form?.ShowDialog() == DialogResult.OK) _ = LoadData(); }
    private void Edit(object? s, EventArgs e) { var item = Selected; if (item == null) return; var form = provider.GetService(typeof(GrupoEditForm)) as GrupoEditForm; form?.LoadGrupo(item); if (form?.ShowDialog() == DialogResult.OK) _ = LoadData(); }
    private async void Delete(object? s, EventArgs e) { var item = Selected; if (item != null && MessageBox.Show("¿Eliminar el grupo?", "Confirmar", MessageBoxButtons.YesNo) == DialogResult.Yes) { await servicio.DeleteAsync(item.Id); await LoadData(); } }
}

public sealed class GrupoEditForm : Form
{
    private readonly IGrupoServicio servicio; private GrupoDTO? current;
    private readonly TextBox nombre = new(); private readonly TextBox descripcion = new(); private readonly DateTimePicker fecha = new() { Format = DateTimePickerFormat.Short };
    public GrupoEditForm(IGrupoServicio servicio) { this.servicio = servicio; Text = "Grupo"; Width = 420; Height = 260; var layout = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(12), RowCount = 5, ColumnCount = 2 }; layout.Controls.Add(new Label { Text = "Nombre" }, 0, 0); layout.Controls.Add(nombre, 1, 0); layout.Controls.Add(new Label { Text = "Descripción" }, 0, 1); layout.Controls.Add(descripcion, 1, 1); layout.Controls.Add(new Label { Text = "Fecha" }, 0, 2); layout.Controls.Add(fecha, 1, 2); var ok = new Button { Text = "Guardar" }; ok.Click += async (_, _) => await Save(); layout.Controls.Add(ok, 1, 3); Controls.Add(layout); }
    public void Clear() { current = null; nombre.Text = descripcion.Text = string.Empty; fecha.Value = DateTime.Today; }
    public void LoadGrupo(GrupoDTO dto) { current = dto; nombre.Text = dto.Nombre; descripcion.Text = dto.Descripcion; fecha.Value = dto.FechaCreacion.ToDateTime(TimeOnly.MinValue); }
    private async Task Save() { var dto = new GrupoDTO { Id = current?.Id ?? 0, Nombre = nombre.Text.Trim(), Descripcion = descripcion.Text.Trim(), FechaCreacion = DateOnly.FromDateTime(fecha.Value) }; if (string.IsNullOrWhiteSpace(dto.Nombre) || string.IsNullOrWhiteSpace(dto.Descripcion)) return; var ok = current == null ? await servicio.AddAsync(dto) != null : await servicio.UpdateAsync(dto); if (ok) { DialogResult = DialogResult.OK; Close(); } }
}
