using System.ComponentModel.DataAnnotations;

namespace Inventarios.Models.ViewModels
{
    public class GrupoViewModel
    {
        [Required]
        public string Nombre { get; set; }
    }
}
