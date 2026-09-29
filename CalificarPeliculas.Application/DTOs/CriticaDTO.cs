using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalificarPeliculas.Application.DTOs
{
    public class CriticaDTO
    {
        public int Id { get; set; }
        public DateOnly FechaPublicacion { get; set; }
        public string Texto { get; set; }
        public float Calificacion { get; set; }
        public int CantReacciones { get; set; }
        public int UsuarioId { get; set; }
        public int ContenidoId { get; set; }
    }
}
