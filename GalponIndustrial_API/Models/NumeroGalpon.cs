using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GalponIndustrial_API.Models
{
    public class NumeroGalpon
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NroGalpon { get; set; }

        [Required]
        public int GalponId { get; set; }

        [ForeignKey(nameof(GalponId))]
        public Galpon Galpon { get; set; }

        public string Detalles { get; set; }

        public DateTime FechaCreacion { get; set;}

        public DateTime FechaActualizacion { get; set;}
    }
    
}
