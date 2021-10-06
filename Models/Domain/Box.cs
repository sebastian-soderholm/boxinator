using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace boxinator.Models.Domain
{
    [Table("Box")]
    public class Box
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Color { get; set; }
        [Required]
        public int BoxTypeId { get; set; }
        public BoxType BoxType { get; set; }
        public int? ShipmentId { get; set; }
        public Shipment Shipment { get; set; }


    }
}
