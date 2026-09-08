namespace CalificarPeliculas.Domain.Users
{
    // Los valores explícitos mantienen estable la denominación del rol al persistirla o intercambiarla.
    public enum TipoRol
    {
        SuperUsuario = 1,
        Moderador = 2,
        Usuario = 3
    }
}
