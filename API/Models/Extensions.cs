using boxinator.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace boxinator.Models
{
    public static class Extensions
    {
        public static List<Shipment> ToFilteredShipmentList(this List<ShipmentStatusLog> shipmentStatusLogList)
        {
            List<Shipment> shipmentListFiltered = new List<Shipment>();

            var filteredShipmentStatusLogList = shipmentStatusLogList
                .GroupBy(x => x.ShipmentId)
                .Select(x => x.OrderByDescending(y => y.Date).FirstOrDefault()).ToList();

            foreach (var shipmentStatusLog in filteredShipmentStatusLogList)
            {
                if (filteredShipmentStatusLogList.Select(x => x.ShipmentId).Contains(shipmentStatusLog.ShipmentId))
                {
                    Shipment shipment = new Shipment()
                    {
                        Id = shipmentStatusLog.Shipment.Id,
                        FirstName = shipmentStatusLog.Shipment.FirstName,
                        LastName = shipmentStatusLog.Shipment.LastName,
                        Address = shipmentStatusLog.Shipment.Address,
                        ZipCode = shipmentStatusLog.Shipment.ZipCode,
                        Cost = shipmentStatusLog.Shipment.Cost,
                        User = shipmentStatusLog.Shipment.User,
                        UserId = shipmentStatusLog.Shipment.UserId,
                        Country = shipmentStatusLog.Shipment.Country,
                        CountryId = shipmentStatusLog.Shipment.Country.Id,
                        Boxes = shipmentStatusLog.Shipment.Boxes,
                        ShipmentStatusLogs = shipmentStatusLog.Shipment.ShipmentStatusLogs
                    };

                    shipmentListFiltered.Add(shipment);
                }
            }

            return shipmentListFiltered;
        }


        /// <summary>
        /// Parse date
        /// </summary>
        /// <param name="date"></param>
        /// <returns>Date or null</returns>
        public static DateTime? parseDate(this string date)
        {
            return date != "" && date != null ? DateTime.Parse(date) : null;
        }

    }
}
