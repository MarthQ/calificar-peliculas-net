using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalificarPeliculas.Domain.Users
{
    public class Rol
    {
        public int Id { get; private set; }
        public string Description { get; private set; } = null!;
        public TipoRol Denominacion { get; private set; }

        protected Rol() { }

        public Rol(int id, string description, TipoRol denominacion) {
            SetId(id);
            SetDescription(description);
            SetDenominacion(denominacion);
        }

        public void SetId(int id) {
            if (id < 0) {
                throw new ArgumentException("El id debe ser mayor a 0");
            }
            Id = id;
        }

        public void SetDescription(string description) {
            if (string.IsNullOrEmpty(description)) {
                throw new ArgumentException("La descripción no puede ser vacía");
            }
            Description = description;
        }

        public void SetDenominacion(TipoRol denominacion) {
            if (!Enum.IsDefined(denominacion))
            {
                throw new ArgumentException("La denominación del rol no es válida.", nameof(denominacion));
            }
            Denominacion = denominacion;
        }
    }
}
