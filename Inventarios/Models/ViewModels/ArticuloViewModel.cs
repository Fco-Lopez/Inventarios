using System.ComponentModel.DataAnnotations;

namespace Inventarios.Models.ViewModels
{
    public class ArticuloViewModel
    {
        [Required]
        public int IdArticulo { get; set; }
        
        [Required]
        public string Codigo { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Unidad { get; set; }

        [Required]
        public double Existencia { get; set; }

        [Required]
        public double Precio { get; set; }

        [Required]
        [Display(Name = "Grupo")]
        public int IdGrupo { get; set; }
    }
}
