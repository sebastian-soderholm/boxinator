using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace boxinator.Models.Domain
{
    [Table("Status")]
    public class Status
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        //Relationships
        public List<ShipmentStatusLog> ShipmentStatusLogs { get; set; }


    }
}
