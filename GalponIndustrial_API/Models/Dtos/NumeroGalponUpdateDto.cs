using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GalponIndustrial_API.Models.Dtos
{
    public class NumeroGalponUpdateDto
    {
        [Required]
        [Range(1,int.MaxValue,ErrorMessage ="Campo NroGalpon requerido debe ser mayor que cero (0)")]
        public int NroGalpon { get; set; }

        [Required(ErrorMessage ="Campo GalponId requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "Campo GalonId requerido debe ser mayor que cero (0)")]
        public int GalponId { get; set; }

        public string Detalles { get; set; }
    }
}
