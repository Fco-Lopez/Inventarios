using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Inventarios.Models
{
    public partial class Entrada
    {
        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Fecha { get; set; }
        public int IdArticulo { get; set; }
        public double Cantidad { get; set; }
        public double Precio { get; set; }

        public virtual Articulo IdArticuloNavigation { get; set; } = null!;
    }
}
