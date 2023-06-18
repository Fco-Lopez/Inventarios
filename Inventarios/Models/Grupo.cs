using System;
using System.Collections.Generic;

namespace Inventarios.Models
{
    public partial class Grupo
    {
        public Grupo()
        {
            Articulos = new HashSet<Articulo>();
        }

        public int IdGrupo { get; set; }
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Articulo> Articulos { get; set; }
    }
}
