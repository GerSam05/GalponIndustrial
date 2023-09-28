using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GalponIndustrial_API.Models
{
    public class Galpon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(30)]
        public string Nombre { get; set; }

        [MaxLength(30)]
        public string Material { get; set; }

        public int m2Totales { get; set;}

        public int m2Construidos { get; set; }

        public double AlturaMax { get; set;}

        public double AlturaMin { get; set; }
       
        public int Baños { get; set; }
       
        public int Oficinas { get; set;}
      
        [Precision(18, 2)]
        public decimal Precio { get; set; }

        public string ImagenURL { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaActualizacion { get; set; }
    }
}
