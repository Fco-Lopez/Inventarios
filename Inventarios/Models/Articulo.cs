using System;
using System.Collections.Generic;

namespace Inventarios.Models
{
    public partial class Articulo
    {
        public Articulo()
        {
            Entrada = new HashSet<Entrada>();
            Salida = new HashSet<Salida>();
        }

        public int IdArticulo { get; set; }
        public string Codigo { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Unidad { get; set; } = null!;
        public double Existencia { get; set; }
        public double Precio { get; set; }
        public int IdGrupo { get; set; }

        public virtual Grupo IdGrupoNavigation { get; set; } = null!;
        public virtual ICollection<Entrada> Entrada { get; set; }
        public virtual ICollection<Salida> Salida { get; set; }
    }
}
