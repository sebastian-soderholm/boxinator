//using boxinator.Models;
//using boxinator.Models.Domain;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace BoxinatorAPITests
//{
//    public class TestData
//    {
//        private List<ShipmentStatusLog> _shipmentStatusLogList;
//        private List<Shipment> _shipmentList;
//        private List<Country> _countryList;
//        private Shipment _shipment;
//        private Shipment _newShipment;
//        private User _user;
//        private User _newUser;
//        private Country _country;

//        public TestData()
//        {
//            _user = new User
//            {
//                Id = 1,
//                Country = new Country() { Id = 1 },
//                CountryId = 1,
//                Email = "test@mail.com",
//                AccountType = "test"
//            };

//            _newUser = new User
//            {
//                Id = 2,
//                Country = new Country() { Id = 2 },
//                CountryId = 2,
//                Email = "test2@mail.com",
//                AccountType = "test2"
//            };

//            _shipment = new Shipment
//            {
//                Id = 1,
//                ReceiverName = "Testi Petteri",
//                Cost = 19.99,
//                User = new User() { Id = 1 },
//                UserId = 1,
//                Country = new Country() { Id = 1 },
//                CountryId = 1,
//                Boxes = new List<Box>(),
//                ShipmentStatusLogs = new List<ShipmentStatusLog>()
//            };

//            _newShipment = new Shipment
//            {
//                Id = 2,
//                ReceiverName = "New Pekka",
//                Cost = 1.52,
//                User = new User() { Id = 2 },
//                UserId = 2,
//                Country = new Country() { Id = 2 },
//                CountryId = 2,
//                Boxes = new List<Box>(),
//                ShipmentStatusLogs = new List<ShipmentStatusLog>()
//            };

//            _shipmentStatusLogList = new List<ShipmentStatusLog>()
//            {
//                new ShipmentStatusLog
//                {
//                    Id = 1,
//                    Status = new Status() { Id = 1 },
//                    Shipment  = _shipment,
//                    ShipmentId = 1,
//                    StatusId = 1,
//                    Date = DateTime.Now
//                },
//               new ShipmentStatusLog
//                {
//                    Id = 2,
//                    Status = new Status() { Id = 2 },
//                    Shipment  = new Shipment() { Id = 2 },
//                    ShipmentId = 2,
//                    StatusId = 2,
//                    Date = DateTime.Now
//                },
//               new ShipmentStatusLog
//                {
//                    Id = 3,
//                    Status = new Status() { Id = 3 },
//                    Shipment  = new Shipment() { Id = 3 },
//                    ShipmentId = 3,
//                    StatusId = 3,
//                    Date = DateTime.Now
//                },
//               new ShipmentStatusLog
//                {
//                    Id = 4,
//                    Status = new Status() { Id = 1 },
//                    Shipment  = new Shipment() { Id = 2 },
//                    ShipmentId = 2,
//                    StatusId = 1,
//                    Date = DateTime.Now
//                }
//            };

//            _shipmentList = new List<Shipment>()
//            {
//                new Shipment
//                {
//                    Id = 1,
//                    ReceiverName = "Testi Petteri",
//                    Cost = 19.99,
//                    User = new User() { Id = 1 },
//                    UserId = 1,
//                    Country = new Country() { Id = 1 },
//                    CountryId = 1,
//                    Boxes = new List<Box>(),
//                    ShipmentStatusLogs = new List<ShipmentStatusLog>()
//                }
//            };

//            _countryList = new List<Country>()
//            {
//                new Country
//                {
//                    Id = 1,
//                    Name = "Finland",
//                    Zone = new Zone(){ Id = 1},
//                    ZoneId = 1
//                }
//            };

//            _country = new Country
//            {
//                Id = 2,
//                Name = "Sweden",
//                Zone = new Zone() { Id = 1 },
//                ZoneId = 1
//            };
//        }


//        public User User
//        {
//            get { return _user; }
//        }

//        public User NewUser
//        {
//            get { return _user; }
//        }

//        public Shipment Shipment
//        {
//            get { return _shipment; }
//        }

//        public Shipment NewShipment
//        {
//            get { return _newShipment; }
//        }

//        public List<ShipmentStatusLog> StatusLogList
//        {
//            get { return _shipmentStatusLogList; }
//        }

//        public List<Shipment> ShipmentList
//        {
//            get { return _shipmentList; }
//        }

//        public List<Country> CountryList
//        {
//            get { return _countryList; }
//        }

//        public Country Country
//        {
//            get { return _country; }
//        }
//    }
//}
