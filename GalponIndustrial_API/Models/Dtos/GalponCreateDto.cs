using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GalponIndustrial_API.Models
{
    public class GalponCreateDto
    {
        [Required(ErrorMessage = "Campo Nombre es requerido")]
        [MaxLength(30, ErrorMessage = "El Campo Nombre no debe exceder los 30 Caracteres")]
        [RegularExpression(@"^[a-zA-Z0-9 ]+$", ErrorMessage = "El Campo Nombre sólo admite letras y números")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Campo Material es requerido")]
        [MaxLength(30, ErrorMessage = "El Campo Material no debe exceder los 30 Caracteres")]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "El Campo Material sólo admite letras")]
        public string Material { get; set; }

        [Required]
        [Range(100, 1000, ErrorMessage = "Campo m2Totales requerido debe estar comprendida entre los 100 y los 1000 metros cuadrados")]
        public int m2Totales { get; set; }

        [Required]
        [Range(100, 1000, ErrorMessage = "Campo m2Totales requerido debe estar comprendida entre los 100m2 y los 1000 m2")]
        public int m2Construidos { get; set; }

        [Required]
        [Range(3.7, 12, ErrorMessage = "Campo AlturaMax requerido debe ser mayor que 3,7m y menor que 12m")]
        public double AlturaMax { get; set; }

        [Required]
        [Range(3.7, 12, ErrorMessage = "Campo AlturaMin requerido debe ser mayor que 3,7m y menor que 12m")]
        public double AlturaMin { get; set; }

        [Required]
        [Range(1, 11, ErrorMessage = "El Galpon debe tener al menos 1 baño y máximo 11 baños")]
        public int Baños { get; set; }

        [Required]
        [Range(0, 11, ErrorMessage = "El Galpon no puede tener mas de 11 oficinas")]
        public int Oficinas { get; set; }

        [Required]
        [Range(500, 5000000, ErrorMessage = "Campo Precio requerido no debe ser inferior a 500$ ni superior a 5000000$")]
        public decimal Precio { get; set; }

        public string ImagenURL { get; set; }
    }
}
