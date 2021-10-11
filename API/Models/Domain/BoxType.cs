using boxinator.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace boxinator.Models
{
    [Table("BoxType")]
    public class BoxType
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(30)]
        public string Name { get; set; }
        public double Weight { get; set; }
        public List<Box> Boxes { get; set; }

    }
}
