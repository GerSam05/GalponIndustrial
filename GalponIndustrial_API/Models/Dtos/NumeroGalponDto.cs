using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GalponIndustrial_API.Models.Dtos
{
    public class NumeroGalponDto
    {
        [Required]
        public int NroGalpon { get; set; }

        [Required]
        public int GalponId { get; set; }

        public string Detalles { get; set; }
    }
}
