using CHU.Utilties;
using CHUModels.ViewModels;
using CHUUtilties;
using System.Linq;
using System.Xml;

namespace CHUService.Facilities
{
    public class GymFacility : BaseFacility, IBaseFacility
    {
        private static List<GymFacilityViewModel> gyms = new();
        private static readonly string GymXmlFile = GetCombinedPath("Data/Gyms.xml");
        List<UserBookingViewModel> UserBookings = new();


        public override string Type { get; set; }

        public GymFacility(string type, string username)
        {
            GetAll();
            Type = type;
            UserName = username;

        }

        public static void GetAll()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(GymXmlFile);
            List<XmlNode> nodes = doc.SelectNodes("Gyms//Gym").Cast<XmlNode>().ToList();
            gyms = nodes.Select(x => new GymFacilityViewModel(
                x.Attributes["Name"]?.Value,
                Convert.ToInt32(x.Attributes["MaxAllowedBlockageLimit"]?.Value) <= 0 ? 1000 : Convert.ToInt32(x.Attributes["MaxAllowedBlockageLimit"]?.Value),
                Convert.ToDateTime(x.Attributes["FacilityStartDate"]?.Value),
                Convert.ToDateTime(x.Attributes["FacilityEndDate"]?.Value),
                EnumExtensions.ParseEnum<MaintainanceType>(Convert.ToString(x.Attributes["MaintainanceType"]?.Value))
               )).ToList();
        }



        public bool Book()
        {
            bool result = false;
            UserBookings = BaseFacility.ReadBookingInfoByUser(UserName);
            Console.WriteLine($"Enter your Gym Area from the available list: {string.Join(",", gyms.Select(x => x.Name))} ");
            var type = Console.ReadLine();
            type = !String.IsNullOrEmpty(type) ? type : "FirstFloorGymA";
            var item = gyms.FirstOrDefault(x => x.Name == type);
            if (gyms.Any(x => x.Name == type) && item?.BookingModel.MaxUserPerSlot > 0)
            {
                if (item.GymMaintainanceType != MaintainanceType.NotAvailable)
                {
                    Console.WriteLine($"Please enter the booking start date and time (dd/MM/yyyy hh:mm): ");
                    var startDate = Console.ReadLine();
                    Console.WriteLine($"Please enter the booking end date and time (dd/MM/yyyy hh:mm): ");
                    var endDate = Console.ReadLine();

                    if (Convert.ToDateTime(endDate) > Convert.ToDateTime(startDate) && (Convert.ToDateTime(startDate) > Convert.ToDateTime(item.TimingModel.OpenDate) &&
                   (Convert.ToDateTime(endDate) < Convert.ToDateTime(item.TimingModel.CloseDate))))
                    {
                        if ((Convert.ToDateTime(startDate) > Convert.ToDateTime(item.LockingTime.LockStartTime)
                                    && (Convert.ToDateTime(endDate) < Convert.ToDateTime(item.LockingTime.LockEndTime))))
                        {
                            Console.WriteLine($"Facility is blocked for booking in this time");
                        }
                        else
                        {
                            --item.BookingModel.MaxUserPerSlot;
                            Task.Run(() => LogUserBooking(UserName, this.Type, startDate, endDate));
                            Console.WriteLine($"Booking is confirmed. Only {item.BookingModel.MaxUserPerSlot} seats are available, do you want to again book another gym (Y/N)?");
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
            else
            {
                Console.WriteLine("No more space available for use.");
            }
            return result;
        }
    }
}