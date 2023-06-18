using System;
using System.Collections.Generic;

namespace Inventarios.Models
{
    public partial class Entrada
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int IdArticulo { get; set; }
        public double Cantidad { get; set; }
        public double Precio { get; set; }

        public virtual Articulo IdArticuloNavigation { get; set; } = null!;
    }
}
