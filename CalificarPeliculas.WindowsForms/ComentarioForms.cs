using System.Windows.Forms;
using CalificarPeliculas.Application.DTOs;
using CalificarPeliculas.Application.Interfaces.Comentarios;
using CalificarPeliculas.Application.Interfaces.Contenido;
using CalificarPeliculas.Application.Interfaces.Criticas;
using CalificarPeliculas.Application.Interfaces.Usuario;

namespace CalificarPeliculas.WindowsForms;

public sealed class ComentarioListForm : Form
{
    private readonly IComentarioServicio servicio; private readonly IServiceProvider provider;
    private readonly DataGridView grid = new() { Dock = DockStyle.Fill, ReadOnly = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect, AutoGenerateColumns = true };
    private List<ComentarioDTO> comentarios = new();
    public ComentarioListForm(IComentarioServicio servicio, IServiceProvider provider) { this.servicio = servicio; this.provider = provider; Text = "Comentarios"; Width = 1000; Height = 500; var buttons = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 42 }; foreach (var item in new[] { ("Agregar", (EventHandler)Add), ("Editar", Edit), ("Eliminar", Delete), ("Cerrar", (_, _) => Close()) }) { var b = new Button { Text = item.Item1, Width = 100 }; b.Click += item.Item2; buttons.Controls.Add(b); } Controls.Add(grid); Controls.Add(buttons); Load += async (_, _) => await LoadData(); }
    private async Task LoadData() { comentarios = (await servicio.GetAllAsync()).ToList(); grid.DataSource = comentarios; }
    private ComentarioDTO? Selected => grid.CurrentRow?.DataBoundItem as ComentarioDTO;
    private void Add(object? s, EventArgs e) { var form = provider.GetService(typeof(ComentarioEditForm)) as ComentarioEditForm; form?.Clear(); if (form?.ShowDialog() == DialogResult.OK) _ = LoadData(); }
    private void Edit(object? s, EventArgs e) { var item = Selected; if (item == null) return; var form = provider.GetService(typeof(ComentarioEditForm)) as ComentarioEditForm; form?.LoadComentario(item); if (form?.ShowDialog() == DialogResult.OK) _ = LoadData(); }
    private async void Delete(object? s, EventArgs e) { var item = Selected; if (item != null && MessageBox.Show("¿Eliminar el comentario?", "Confirmar", MessageBoxButtons.YesNo) == DialogResult.Yes) { await servicio.DeleteAsync(item.Id); await LoadData(); } }
}

public sealed class ComentarioEditForm : Form
{
    private readonly IComentarioServicio servicio; private readonly ICriticaServicio criticaServicio; private readonly IUsuarioServicio usuarioServicio; private ComentarioDTO? current;
    private readonly ComboBox critica = new() { DropDownStyle = ComboBoxStyle.DropDownList }; private readonly ComboBox usuario = new() { DropDownStyle = ComboBoxStyle.DropDownList }; private readonly TextBox texto = new(); private readonly TextBox reaccion = new(); private readonly DateTimePicker fecha = new() { Format = DateTimePickerFormat.Short };
    public ComentarioEditForm(IComentarioServicio servicio, ICriticaServicio criticaServicio, IUsuarioServicio usuarioServicio) { this.servicio = servicio; this.criticaServicio = criticaServicio; this.usuarioServicio = usuarioServicio; Text = "Comentario"; Width = 500; Height = 330; var layout = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(12), RowCount = 6, ColumnCount = 2 }; layout.Controls.Add(new Label { Text = "Crítica" }, 0, 0); layout.Controls.Add(critica, 1, 0); layout.Controls.Add(new Label { Text = "Usuario" }, 0, 1); layout.Controls.Add(usuario, 1, 1); layout.Controls.Add(new Label { Text = "Texto" }, 0, 2); layout.Controls.Add(texto, 1, 2); layout.Controls.Add(new Label { Text = "Fecha" }, 0, 3); layout.Controls.Add(fecha, 1, 3); layout.Controls.Add(new Label { Text = "Reacción" }, 0, 4); layout.Controls.Add(reaccion, 1, 4); var ok = new Button { Text = "Guardar" }; ok.Click += async (_, _) => await Save(); layout.Controls.Add(ok, 1, 5); Controls.Add(layout); Shown += async (_, _) => await LoadMasters(); }
    private async Task LoadMasters() { var cs = (await criticaServicio.GetAllAsync()).Select(c => new ComboItem { Id = c.Id, Nombre = c.Texto }).ToList(); critica.DataSource = cs; critica.DisplayMember = "Nombre"; critica.ValueMember = "Id"; var us = (await usuarioServicio.GetAllAsync()).Select(u => new ComboItem { Id = u.Id, Nombre = u.NombreUsuario }).ToList(); usuario.DataSource = us; usuario.DisplayMember = "Nombre"; usuario.ValueMember = "Id"; if (current != null) { critica.SelectedValue = current.CriticaId; usuario.SelectedValue = current.UsuarioId; } }
    public void Clear() { current = null; texto.Text = reaccion.Text = string.Empty; fecha.Value = DateTime.Today; }
    public void LoadComentario(ComentarioDTO dto) { current = dto; texto.Text = dto.Texto; reaccion.Text = dto.Reaccion; fecha.Value = dto.FechaRealizacion.ToDateTime(TimeOnly.MinValue); }
    private async Task Save() { if (critica.SelectedValue is not int criticaId || usuario.SelectedValue is not int usuarioId || string.IsNullOrWhiteSpace(texto.Text) || string.IsNullOrWhiteSpace(reaccion.Text)) return; var dto = new ComentarioDTO { Id = current?.Id ?? 0, CriticaId = criticaId, UsuarioId = usuarioId, Texto = texto.Text.Trim(), Reaccion = reaccion.Text.Trim(), FechaRealizacion = DateOnly.FromDateTime(fecha.Value) }; var ok = current == null ? await servicio.AddAsync(dto) != null : await servicio.UpdateAsync(dto); if (ok) { DialogResult = DialogResult.OK; Close(); } }
}
