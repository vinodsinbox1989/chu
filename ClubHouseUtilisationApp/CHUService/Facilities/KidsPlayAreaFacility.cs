using CHU.Utilties;

namespace CHUService.Facilities
{
    public class KidsPlayAreaFacility : IBaseFacility
    {
        private static readonly List<KidsPlayAreaFacilityViewModel> kidsAreaFacilties = new();
        public KidsPlayAreaFacility()
        {
            kidsAreaFacilties.Add(new KidsPlayAreaFacilityViewModel("FirstFloorBlockA", new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 09, 00, 00), new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 00, 00), MaintainanceType.Available) { });
            kidsAreaFacilties.Add(new KidsPlayAreaFacilityViewModel("SecondFloorBlockA", new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 09, 00, 00), new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 13, 00, 00), MaintainanceType.NotAvailable) { });
            kidsAreaFacilties.Add(new KidsPlayAreaFacilityViewModel("FirstFloorBlockB", new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 09, 00, 00), new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 00, 00), MaintainanceType.NotAvailable) { });
            kidsAreaFacilties.Add(new KidsPlayAreaFacilityViewModel("SecondFloorBlockB", new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 09, 00, 00), new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 13, 00, 00), MaintainanceType.NotAvailable) { });
        }

        public void Book()
        {
            Console.WriteLine($"Enter your Kids area Name from the available list: {string.Join(",", kidsAreaFacilties.Select(x => x.Name))} ");
            var type = Console.ReadLine();
            type = !String.IsNullOrEmpty(type) ? type : "FirstFloorBlockA";
            var item = kidsAreaFacilties.FirstOrDefault(x => x.Name == type);
            if (kidsAreaFacilties.Any(x => x.Name == type))
            {
                if (item.MaintainanceType == MaintainanceType.Available)
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
                        Console.WriteLine($"Booking is confirmed.Do you want to again book another Kids area (Y/N)?");
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
            }
            else
            {
                Console.WriteLine("Bookings are not avialable due to maintainance.");
            }
        }
    }
}