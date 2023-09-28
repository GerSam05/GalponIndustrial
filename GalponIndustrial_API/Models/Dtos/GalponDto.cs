using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GalponIndustrial_API.Models
{
    public class GalponDto
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Material { get; set; }

        public int m2Totales { get; set;}

        public int m2Construidos { get; set; }

        public double AlturaMax { get; set;}

        public double AlturaMin { get; set;}

        public int Baños { get; set; }

        public int Oficinas { get; set;}

        public decimal Precio { get; set; }

        public string ImagenURL { get; set; }
    }
}
