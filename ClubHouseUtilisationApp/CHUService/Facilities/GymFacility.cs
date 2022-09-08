using CHU.Utilties;

namespace CHUService.Facilities
{
    public class GymFacility : IBaseFacility
    {
        private static readonly List<GymFacilityViewModel> gyms = new();
        public GymFacility()
        {
            gyms.Add(new GymFacilityViewModel("FirstFloorBlockA", 3, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 09, 00, 00), new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 00, 00), MaintainanceType.Available) { });
            gyms.Add(new GymFacilityViewModel("SecondFloorBlockA", 50, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 09, 00, 00), new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 13, 00, 00), MaintainanceType.PartiallyAvailable) { });
            gyms.Add(new GymFacilityViewModel("FirstFloorBlockB", 100, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 09, 00, 00), new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 00, 00), MaintainanceType.NotAvailable) { });
            gyms.Add(new GymFacilityViewModel("SecondFloorBlockB", 100, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 09, 00, 00), new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 13, 00, 00), MaintainanceType.PartiallyAvailable) { });
        }

        public void Book()
        {
            Console.WriteLine($"Enter your Gym Area from the available list: {string.Join(",", gyms.Select(x => x.Name))} ");
            var type = Console.ReadLine();
            type = !String.IsNullOrEmpty(type) ? type : "FirstFloorBlockA";
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
                            Console.WriteLine($"Booking is confirmed. Only {item.BookingModel.MaxUserPerSlot} seats are available, do you want to again book another gym (Y/N)?");
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
        }
    }
}