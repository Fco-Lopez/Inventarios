using System.ComponentModel.DataAnnotations;

namespace Inventarios.Models.ViewModels
{
    public class SalidaViewModel
    {
        [Required]
        [Display(Name = "Folio")]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        [Required]
        public double Cantidad { get; set; }

        [Required]
        public double Precio { get; set; }

        [Required]
        [Display(Name = "Articulo")]
        public int IdArticulo { get; set; }

        public string Codigo { get; set; }
    }
}
