using System.Windows.Forms;
using CalificarPeliculas.Application.DTOs;
using CalificarPeliculas.Application.Interfaces.Grupo;
using CalificarPeliculas.Application.Interfaces.Usuario;

namespace CalificarPeliculas.WindowsForms;

public sealed class UsuarioListForm : Form
{
    private readonly IUsuarioServicio servicio; private readonly IServiceProvider provider;
    private readonly DataGridView grid = new() { Dock = DockStyle.Fill, ReadOnly = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect, AutoGenerateColumns = true };
    private List<UsuarioDTO> usuarios = new();
    public UsuarioListForm(IUsuarioServicio servicio, IServiceProvider provider) { this.servicio = servicio; this.provider = provider; Text = "Usuarios"; Width = 900; Height = 500; var buttons = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 42 }; foreach (var item in new[] { ("Agregar", (EventHandler)Add), ("Editar", Edit), ("Eliminar", Delete), ("Cerrar", (_, _) => Close()) }) { var b = new Button { Text = item.Item1, Width = 100 }; b.Click += item.Item2; buttons.Controls.Add(b); } Controls.Add(grid); Controls.Add(buttons); Load += async (_, _) => await LoadData(); }
    private async Task LoadData() { usuarios = (await servicio.GetAllAsync()).ToList(); grid.DataSource = usuarios.Select(u => new { u.Id, u.NombreUsuario, u.Mail, u.RolId, u.GrupoId }).ToList(); }
    private UsuarioDTO? Selected => grid.CurrentRow == null ? null : usuarios.FirstOrDefault(u => u.Id == Convert.ToInt32(grid.CurrentRow.Cells["Id"].Value));
    private void Add(object? s, EventArgs e) { var form = provider.GetService(typeof(UsuarioEditForm)) as UsuarioEditForm; form?.Clear(); if (form?.ShowDialog() == DialogResult.OK) _ = LoadData(); }
    private void Edit(object? s, EventArgs e) { var item = Selected; if (item == null) return; var form = provider.GetService(typeof(UsuarioEditForm)) as UsuarioEditForm; form?.LoadUsuario(item); if (form?.ShowDialog() == DialogResult.OK) _ = LoadData(); }
    private async void Delete(object? s, EventArgs e) { var item = Selected; if (item != null && MessageBox.Show("¿Eliminar el usuario?", "Confirmar", MessageBoxButtons.YesNo) == DialogResult.Yes) { await servicio.DeleteAsync(item.Id); await LoadData(); } }
}

public sealed class UsuarioEditForm : Form
{
    private readonly IUsuarioServicio servicio; private readonly IGrupoServicio grupoServicio; private UsuarioDTO? current;
    private readonly TextBox nombre = new(); private readonly TextBox mail = new(); private readonly TextBox password = new() { UseSystemPasswordChar = true }; private readonly NumericUpDown rol = new() { Minimum = 1, Maximum = 3, Value = 3 }; private readonly ComboBox grupo = new() { DropDownStyle = ComboBoxStyle.DropDownList };
    public UsuarioEditForm(IUsuarioServicio servicio, IGrupoServicio grupoServicio) { this.servicio = servicio; this.grupoServicio = grupoServicio; Text = "Usuario"; Width = 450; Height = 330; var layout = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(12), RowCount = 6, ColumnCount = 2 }; layout.Controls.Add(new Label { Text = "Nombre" }, 0, 0); layout.Controls.Add(nombre, 1, 0); layout.Controls.Add(new Label { Text = "Mail" }, 0, 1); layout.Controls.Add(mail, 1, 1); layout.Controls.Add(new Label { Text = "Contraseña (solo alta)" }, 0, 2); layout.Controls.Add(password, 1, 2); layout.Controls.Add(new Label { Text = "Rol" }, 0, 3); layout.Controls.Add(rol, 1, 3); layout.Controls.Add(new Label { Text = "Grupo" }, 0, 4); layout.Controls.Add(grupo, 1, 4); var ok = new Button { Text = "Guardar" }; ok.Click += async (_, _) => await Save(); layout.Controls.Add(ok, 1, 5); Controls.Add(layout); Shown += async (_, _) => await LoadGroups(); }
    private async Task LoadGroups() { var items = (await grupoServicio.GetAllAsync()).Select(g => new ComboItem { Id = g.Id, Nombre = g.Nombre }).ToList(); items.Insert(0, new ComboItem { Id = 0, Nombre = "Sin grupo" }); grupo.DataSource = items; grupo.DisplayMember = "Nombre"; grupo.ValueMember = "Id"; if (current != null) grupo.SelectedValue = current.GrupoId ?? 0; }
    public void Clear() { current = null; nombre.Text = mail.Text = password.Text = string.Empty; rol.Value = 3; password.Enabled = true; }
    public void LoadUsuario(UsuarioDTO dto) { current = dto; nombre.Text = dto.NombreUsuario; mail.Text = dto.Mail; rol.Value = dto.RolId; password.Text = string.Empty; password.Enabled = false; }
    private async Task Save() { if (string.IsNullOrWhiteSpace(nombre.Text) || string.IsNullOrWhiteSpace(mail.Text) || (current == null && string.IsNullOrWhiteSpace(password.Text))) return; var grupoId = grupo.SelectedValue is int id && id > 0 ? id : (int?)null; bool ok; if (current == null) ok = await servicio.RegistrarAsync(new RegistrarUsuarioDTO { NombreUsuario = nombre.Text.Trim(), Mail = mail.Text.Trim(), Password = password.Text, RolId = (int)rol.Value, GrupoId = grupoId }) != null; else ok = await servicio.UpdateAsync(new ActualizarUsuarioDTO { Id = current.Id, NombreUsuario = nombre.Text.Trim(), Mail = mail.Text.Trim(), RolId = (int)rol.Value, GrupoId = grupoId }); if (ok) { DialogResult = DialogResult.OK; Close(); } }
}
