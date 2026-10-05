using CalificarPeliculas.Domain.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CalificarPeliculas.Domain
{
    public class Critica
    {
        public int Id { get; private set; }
        public DateOnly FechaPublicacion { get; private set; }
        public string Texto { get; private set; }
        public float Calificacion { get; private set; }
        public int CantReacciones { get; private set; }

        private Usuario? _usuario;
        private int _usuarioId;
        public int UsuarioId
        {
            get => _usuario?.Id ?? _usuarioId;
            private set => _usuarioId = value;
        }

        public Usuario? Usuario
        {
            get => _usuario;
            private set
            {
                _usuario = value;
                if (value != null && _usuarioId != value.Id)
                {
                    _usuarioId = value.Id;
                }
            }
        }
        private Contenido? _contenido;
        private int _contenidoId;
        public int ContenidoId
        {
            get => _contenido?.Id ?? _contenidoId;
            private set => _contenidoId = value;
        }

        public Contenido? Contenido
        {
            get => _contenido;
            private set
            {
                _contenido = value;
                if (value != null && _contenidoId != value.Id)
                {
                    _contenidoId = value.Id;
                }
            }
        }

        protected Critica() { }

        public Critica(int id, int usuarioId, int contenidoId, DateOnly fecha, string texto, float calificacion, int cantReacciones)
        {
            SetId(id);
            SetUsuarioId(usuarioId);
            SetContenidoId(contenidoId);
            SetFecha(fecha);
            SetTexto(texto);
            SetCalificacion(calificacion);
            SetCantReacciones(cantReacciones);
        }

        public void SetId(int id)
        {
            if (id < 0)
                throw new ArgumentException("La ID debe ser mayor o igual a 0.", nameof(id));
            Id = id;
        }
        public void SetFecha(DateOnly fecha)
        {
            if (fecha == default)
                throw new ArgumentException("La fecha de publicacion no puede ser la fecha por defecto.", nameof(fecha));
            FechaPublicacion = fecha;
        }
        public void SetTexto(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                throw new ArgumentException("El texto no puede ser vacío o nulo.", nameof(texto));
            Texto = texto;
        }
        public void SetCalificacion(float calificacion)
        {
            if (calificacion < 0 || calificacion > 10)
                throw new ArgumentException("La calificación debe estar entre 0 y 10.", nameof(calificacion));
            Calificacion = calificacion;
        }
        public void SetCantReacciones(int cantReacciones)
        {
            if (cantReacciones < 0)
                throw new ArgumentException("La cantidad de reacciones debe ser mayor o igual a 0.", nameof(cantReacciones));
            CantReacciones = cantReacciones;
        }

        public void SetUsuarioId(int id)
        {
            if (id < 0)
                throw new ArgumentException("El id del usuario debe ser mayor o igual a 0.", nameof(id));
            _usuarioId = id;

            if (_usuario != null && _usuario.Id != id)
            {
                _usuario = null;
            }
        }
        public void SetContenidoId(int id)
        {
            if (id < 0)
                throw new ArgumentException("El id del contenido debe ser mayor o igual a 0.", nameof(id));
            _contenidoId = id;

            if (_contenido != null && _contenido.Id != id)
            {
                _contenido = null;
            }
        }
    }
}
