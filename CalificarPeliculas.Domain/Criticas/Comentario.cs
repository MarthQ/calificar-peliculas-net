namespace CalificarPeliculas.Domain;

public class Comentario
{
    public int Id { get; private set; }
    public string Texto { get; private set; } = null!;
    public DateOnly FechaRealizacion { get; private set; }
    public string Reaccion { get; private set; } = null!;

    private int _criticaId;
    private Critica? _critica;
    public int CriticaId => _critica?.Id ?? _criticaId;
    public Critica? Critica => _critica;

    private int _usuarioId;
    private Users.Usuario? _usuario;
    public int UsuarioId => _usuario?.Id ?? _usuarioId;
    public Users.Usuario? Usuario => _usuario;

    protected Comentario() { }

    public Comentario(int id, int criticaId, int usuarioId, string texto, DateOnly fechaRealizacion, string reaccion)
    {
        SetId(id);
        SetCriticaId(criticaId);
        SetUsuarioId(usuarioId);
        SetTexto(texto);
        SetFechaRealizacion(fechaRealizacion);
        SetReaccion(reaccion);
    }

    public void SetId(int id)
    {
        if (id < 0) throw new ArgumentException("La ID debe ser mayor o igual a 0.", nameof(id));
        Id = id;
    }

    public void SetTexto(string texto)
    {
        if (string.IsNullOrWhiteSpace(texto)) throw new ArgumentException("El texto no puede estar vacío.", nameof(texto));
        Texto = texto.Trim();
    }

    public void SetFechaRealizacion(DateOnly fecha)
    {
        if (fecha == default) throw new ArgumentException("La fecha no puede ser inválida.", nameof(fecha));
        FechaRealizacion = fecha;
    }

    public void SetReaccion(string reaccion)
    {
        if (string.IsNullOrWhiteSpace(reaccion)) throw new ArgumentException("La reacción no puede estar vacía.", nameof(reaccion));
        Reaccion = reaccion.Trim();
    }

    public void SetCriticaId(int id)
    {
        if (id <= 0) throw new ArgumentException("La crítica debe ser válida.", nameof(id));
        _criticaId = id;
        if (_critica?.Id != id) _critica = null;
    }

    public void SetUsuarioId(int id)
    {
        if (id <= 0) throw new ArgumentException("El usuario debe ser válido.", nameof(id));
        _usuarioId = id;
        if (_usuario?.Id != id) _usuario = null;
    }
}
