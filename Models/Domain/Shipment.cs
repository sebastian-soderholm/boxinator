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
        public string ReceiverName { get; set; }

        [Required]
        public int CountryId { get; set; }
        public Country Country { get; set; }

        public double Cost { get; set; }

        public int? UserId { get; set; }
        public User User { get; set; }
        // Relationships
        //public ICollection<Movie> Movies { get; set; }
    }
}
