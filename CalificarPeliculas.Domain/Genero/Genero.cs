namespace CalificarPeliculas.Domain.Genero
{
    public class Genero(int id, string nombre)
    {
        public int Id { get; private set; } = id;
        public string? Nombre { get; private set; } = nombre;

        public void SetId(int id)
        {
            if (id < 0)
                throw new ArgumentException("El id debe ser mayor que 0.", nameof(id));
            Id = id;
        }
        public void SetNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre no puede estar vacío o ser nulo.", nameof(nombre));
            Nombre = nombre;
        }
    }
}
