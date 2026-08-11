namespace CalificarPeliculas.Domain.Genero
{
    public class Genero
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; }

        public Genero(int id, string nombre)
        {
            SetId(id);
            SetNombre(nombre);
        }

        public void SetId(int id)
        {
            if (id < 0)
                throw new ArgumentException("El id debe ser mayor que 0.", nameof(id));
            Id = id;
        }
        public void SetNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre de un genero no puede estar vacío o ser nulo.", nameof(nombre));
            Nombre = nombre;
        }
    }
}
