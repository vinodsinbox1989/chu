using CHU.Utilties;
using CHUModels;
using CHUUtilties;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Xml;

namespace CHUService.Facilities
{
    public class PoolFacility : BaseFacility, IBaseFacility
    {
        private static List<PoolFacilityViewModel> pools = new();
        private static readonly string PoolXmlFile = GetCombinedPath("Data/Pools.xml");
        public override string Type { get; set; }

        public PoolFacility(string type, string username)
        {
            GetAll();
            Type = type;
            UserName = username;
        }

        public static void GetAll()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(PoolXmlFile);
            List<XmlNode> nodes = doc.SelectNodes("Pools//Pool").Cast<XmlNode>().ToList();
            pools = nodes.Select(x => new PoolFacilityViewModel(
                x.Attributes["Name"]?.Value,
                Convert.ToDateTime(x.Attributes["FacilityStartDate"]?.Value),
                Convert.ToDateTime(x.Attributes["FacilityEndDate"]?.Value),
                EnumExtensions.ParseEnum<MaintainanceType>(Convert.ToString(x.Attributes["MaintainanceType"]?.Value)),
                GetLockingPeriod(x), GetMaintenancePeriod(x))).ToList();



        }

        private static MaintenancePeriodModel GetMaintenancePeriod(XmlNode x)
        {
            return new MaintenancePeriodModel() { };
        }

        private static LockingModel GetLockingPeriod(XmlNode x)
        {
            var lockingPeriodNode = x.SelectSingleNode("LockingPeriod")?.Cast<XmlNode>()?.ToList();
            var sDate = lockingPeriodNode?.FirstOrDefault(c => c.Name == "LockStartDate")?.InnerText;
            var eDate = lockingPeriodNode?.FirstOrDefault(c => c.Name == "LockEndDate")?.InnerText;
            return new LockingModel()
            {
                LockStartTime = Convert.ToDateTime(sDate),
                LockEndTime = Convert.ToDateTime(eDate),
            };
        }

        public bool Book()
        {
            bool result = false;
            Console.WriteLine($"Enter your Pool Area from the available list: {string.Join(",", pools.Select(x => x.Name))} ");
            var type = Console.ReadLine();
            type = !String.IsNullOrEmpty(type) ? type : "FirstFloorPoolA";
            var item = pools.FirstOrDefault(x => x.Name == type);
            if (pools.Any(x => x.Name == type))
            {
                if (item.PoolMaintainanceType == MaintainanceType.Available)
                {
                    Console.WriteLine($"Please enter the booking start date and time (dd/MM/yyyy hh:mm): ");
                    var startDate = Console.ReadLine();
                    Console.WriteLine($"Please enter the booking end date and time (dd/MM/yyyy hh:mm): ");
                    var endDate = Console.ReadLine();

                    if (Convert.ToDateTime(endDate) > Convert.ToDateTime(startDate) &&
                   (Convert.ToDateTime(startDate) > Convert.ToDateTime(item.TimingModel.OpenDate) &&
                   (Convert.ToDateTime(endDate) < Convert.ToDateTime(item.TimingModel.CloseDate))))
                    {
                        if ((Convert.ToDateTime(startDate) > Convert.ToDateTime(item.LockingTime.LockStartTime)
                            && (Convert.ToDateTime(endDate) < Convert.ToDateTime(item.LockingTime.LockEndTime))))
                        {
                            Console.WriteLine($"Facility is blocked for booking in this time");
                        }
                        else
                        {
                            Task.Run(() => LogUserBooking(UserName, this.Type, startDate, endDate));
                            Console.WriteLine($"Booking is confirmed. Do you want to again book another Pool (Y/N)?");
                            var yesNo = Console.ReadLine();
                            switch (yesNo)
                            {
                                case "Y":
                                    this.Book();
                                    break;
                                case "N":
                                    result = true;
                                    Console.WriteLine("Thank you booking has been confirmed.");
                                    break;
                                default:
                                    Console.WriteLine("Invalid input.");
                                    break;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Bookings are not avialable in this time slot.");
                    }
                }
                else
                {
                    Console.WriteLine("Bookings are not avialable due to maintainance.");
                }
            }
            return result;
        }
    }
}