using CHU.Utilties;
using CHUModels;
using CHUModels.ViewModels;
using CHUUtilties;
using System.Xml;

namespace CHUService.Facilities
{
    public class LibraryFacility : BaseFacility, IBaseFacility
    {
        private static List<LibraryFacilityViewModel> libraryFacilities = new();
        private static readonly string LibrariesXmlFile = GetCombinedPath("Data/Libraries.xml");
        public LibraryFacility()
        {
            GetAll();
        }

        public static void GetAll()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(LibrariesXmlFile);
            List<XmlNode> nodes = doc.SelectNodes("Libraries//Library").Cast<XmlNode>().ToList();
            libraryFacilities = nodes.Select(x => new LibraryFacilityViewModel(
                x.Attributes["Name"]?.Value,
                Convert.ToInt32(x.Attributes["MaxAllowedBlockageLimit"]?.Value) <= 0 ? 1000 : Convert.ToInt32(x.Attributes["MaxAllowedBlockageLimit"]?.Value),
                Convert.ToDateTime(x.Attributes["FacilityStartDate"]?.Value),
                Convert.ToDateTime(x.Attributes["FacilityEndDate"]?.Value),
                GetLockingPeriod(x), GetMaintenancePeriod(x)
               )).ToList();
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

        public void Book()
        {
            Console.WriteLine($"Enter your Library Name from the available list: {string.Join(",", libraryFacilities.Select(x => x.Name))} ");
            var type = Console.ReadLine();
            type = !String.IsNullOrEmpty(type) ? type : "FirstFloorLibraryA";
            var item = libraryFacilities.FirstOrDefault(x => x.Name == type);
            if (libraryFacilities.Any(x => x.Name == type) && item?.BookingModel.MaxUserPerSlot > 0)
            {
                Console.WriteLine($"Please enter the booking start date and time (dd/MM/yyyy hh:mm): ");
                var startDate = Console.ReadLine();
                Console.WriteLine($"Please enter the booking end date and time (dd/MM/yyyy hh:mm): ");
                var endDate = Console.ReadLine();

                if ((Convert.ToDateTime(startDate) > Convert.ToDateTime(item.LockingTime.LockStartTime)
                            && (Convert.ToDateTime(endDate) < Convert.ToDateTime(item.LockingTime.LockEndTime))))
                {
                    Console.WriteLine($"Facility is blocked for booking in this time");
                }
                else
                {
                    --item.BookingModel.MaxUserPerSlot;
                    Console.WriteLine($"Booking is confirmed. Only {item.BookingModel.MaxUserPerSlot} seats are available, do you want to again book another library (Y/N)?");
                    var yesNo = Console.ReadLine();
                    switch (yesNo)
                    {
                        case "Y":
                            this.Book();
                            break;
                        case "N":
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
                Console.WriteLine("No more space available for use.");
            }
        }

    }
}