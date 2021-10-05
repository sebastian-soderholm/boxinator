using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace boxinator.Models.Domain
{
    [Table("Shipment")]
    public class Shipment
    {
        // Primary key
        [Key]
        public int Id { get; set; }

        // Fields
        [Required]
        [MaxLength(100)]
        public string RecieverName { get; set; }

        [Required]
        [MaxLength(100)]
        public string DestinationCountry { get; set; }

        // Relationships
        //public ICollection<Movie> Movies { get; set; }
    }
}
