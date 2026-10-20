namespace CalificarPeliculas.WindowsForms
{
    // Ítem genérico (Id + texto) para enlazar ComboBox a entidades maestras.
    public class ComboItem
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;

        public override string ToString() => Nombre;
    }
}
