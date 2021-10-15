using boxinator.Models.DTO.Status;
using System;


namespace boxinator.Models.DTO.ShipmentStatusLog
{
    public class ShipmentStatusLogReadDTO
    {
        public int ShipmentId { get; set; }
        public StatusReadDTO Status { get; set; }
        public DateTime Date { get; set; }
    }
}
