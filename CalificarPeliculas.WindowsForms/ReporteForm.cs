using System.Drawing;
using CalificarPeliculas.Application.DTOs;

namespace CalificarPeliculas.WindowsForms;

public sealed class ReporteForm : Form
{
    private readonly ReporteApiServicio reportes;
    private readonly TabControl tabs = new() { Dock = DockStyle.Fill };
    private readonly Panel chart = new() { Dock = DockStyle.Fill, BackColor = Color.White };
    private readonly DataGridView grid = new() { Dock = DockStyle.Fill, ReadOnly = true, AutoGenerateColumns = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect };

    public ReporteForm(ReporteApiServicio reportes)
    {
        this.reportes = reportes;
        Text = "Reportes";
        Width = 1100;
        Height = 650;
        var grafico = new TabPage("Resumen de calificaciones");
        var detalle = new TabPage("Detalle de críticas y comentarios");
        grafico.Controls.Add(chart);
        detalle.Controls.Add(grid);
        tabs.TabPages.Add(grafico);
        tabs.TabPages.Add(detalle);
        Controls.Add(tabs);
        chart.Paint += PintarGrafico;
        Load += async (_, _) => await LoadReportsAsync();
    }

    private List<ReporteContenidoDTO> resumen = new();

    private void PintarGrafico(object? sender, PaintEventArgs e)
    {
        if (resumen.Count == 0) return;
        var datos = resumen.Take(10).Reverse().ToList();
        var area = chart.ClientRectangle;
        using var brush = new SolidBrush(Color.FromArgb(13, 110, 253));
        using var textBrush = new SolidBrush(Color.Black);
        using var font = new Font(FontFamily.GenericSansSerif, 9);
        var ancho = Math.Max(20, (area.Width - 70) / datos.Count - 12);
        var altoMaximo = Math.Max(1, area.Height - 80);
        for (var i = 0; i < datos.Count; i++)
        {
            var alto = (float)(datos[i].PromedioCalificacion / 10 * altoMaximo);
            var x = 50 + i * (ancho + 12);
            var y = area.Height - 45 - alto;
            e.Graphics.FillRectangle(brush, x, y, ancho, alto);
            e.Graphics.DrawString(datos[i].PromedioCalificacion.ToString("0.0"), font, textBrush, x, y - 18);
            e.Graphics.TranslateTransform(x + ancho / 2, area.Height - 40);
            e.Graphics.RotateTransform(-45);
            e.Graphics.DrawString(datos[i].Contenido, font, textBrush, 0, 0);
            e.Graphics.ResetTransform();
        }
        e.Graphics.DrawLine(Pens.Black, 45, area.Height - 45, area.Width - 10, area.Height - 45);
    }

    private async Task LoadReportsAsync()
    {
        resumen = await reportes.GetResumenAsync();
        var detalle = await reportes.GetDetalleAsync();
        grid.DataSource = detalle;
        chart.Invalidate();
    }
}
