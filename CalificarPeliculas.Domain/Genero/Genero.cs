namespace CalificarPeliculas.Domain.Genero
{
    public class Genero(int id, string nombre)
    {
        private int Id { get; set; } = id;
        public string? Nombre { get; set; } = nombre;
    }
}
