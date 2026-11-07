namespace CalificarPeliculas.Domain.Users;

public class Grupo
{
    public int Id { get; private set; }
    public string Nombre { get; private set; } = null!;
    public string Descripcion { get; private set; } = null!;
    public DateOnly FechaCreacion { get; private set; }
    public ICollection<Usuario> Usuarios { get; } = new List<Usuario>();

    protected Grupo() { }

    public Grupo(int id, string nombre, string descripcion, DateOnly fechaCreacion)
    {
        SetId(id);
        SetNombre(nombre);
        SetDescripcion(descripcion);
        SetFechaCreacion(fechaCreacion);
    }

    public void SetId(int id)
    {
        if (id < 0) throw new ArgumentException("La ID debe ser mayor o igual a 0.", nameof(id));
        Id = id;
    }

    public void SetNombre(string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre)) throw new ArgumentException("El nombre no puede estar vacío.", nameof(nombre));
        Nombre = nombre.Trim();
    }

    public void SetDescripcion(string descripcion)
    {
        if (string.IsNullOrWhiteSpace(descripcion)) throw new ArgumentException("La descripción no puede estar vacía.", nameof(descripcion));
        Descripcion = descripcion.Trim();
    }

    public void SetFechaCreacion(DateOnly fecha)
    {
        if (fecha == default) throw new ArgumentException("La fecha no puede ser inválida.", nameof(fecha));
        FechaCreacion = fecha;
    }
}
