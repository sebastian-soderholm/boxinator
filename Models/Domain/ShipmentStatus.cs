using boxinator.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace boxinator.Models
{
    [Table("ShipmentStatus")]
    public class ShipmentStatus
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ShipmentId { get; set; }
        public Shipment Shipment { get; set; }
        [Required]
        [MaxLength(20)]
        public string Status { get; set; }
        public DateTime Date { get; set; }
    }
}
