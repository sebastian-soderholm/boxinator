using boxinator.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
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
        public static DateTime? ParseDate(this string date)
        {
            return date != "" && date != null ? DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture) : null;
        }

        /// <summary>
        /// Extract user email from authorization header
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Email</returns>
        public static string ExtractEmailFromToken(this HttpRequest request)
        {
            // Fetches token from header
            string accessTokenWithBearerPrefix = request.Headers[HeaderNames.Authorization];

            if(accessTokenWithBearerPrefix != null)
            {
                string accessTokenWithoutBearerPrefix = accessTokenWithBearerPrefix.Substring("Bearer ".Length);
                var token = new JwtSecurityToken(jwtEncodedString: accessTokenWithoutBearerPrefix);
                // Fetch user's email with the token
                return token.Claims.First(c => c.Type == "email").Value;
            }

            return "";
        }

        /// <summary>
        /// Role check
        /// </summary>
        /// <param name="user"></param>
        /// <returns>True if admin</returns>
        public static bool IsAdmin(this User user)
        {
            if (user?.AccountType == AccountTypes.ADMINISTRATOR.ToString())
                return true;

            return false;
        }

    }
}
