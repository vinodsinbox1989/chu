using CHU.Utilties;

namespace CHUService.Facilities
{
    public class PoolFacility : IBaseFacility
    {
        private static readonly List<PoolFacilityViewModel> pools = new();
        public PoolFacility()
        {
            pools.Add(new PoolFacilityViewModel("FirstFloorBlockA", new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 09, 00, 00), new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 00, 00), MaintainanceType.NotAvailable, new CHUModels.LockingModel() { LockStartTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 03, 00, 00), LockEndTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 05, 00, 00) }) { });
            pools.Add(new PoolFacilityViewModel("SecondFloorBlockA", new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 09, 00, 00), new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 13, 00, 00), MaintainanceType.NotAvailable) { });
            pools.Add(new PoolFacilityViewModel("FirstFloorBlockB", new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 09, 00, 00), new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 00, 00), MaintainanceType.Available) { });
            pools.Add(new PoolFacilityViewModel("SecondFloorBlockB", new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 09, 00, 00), new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 13, 00, 00), MaintainanceType.Available) { });
        }

        public void Book()
        {
            Console.WriteLine($"Enter your Pool Area from the available list: {string.Join(",", pools.Select(x => x.Name))} ");
            var type = Console.ReadLine();
            type = !String.IsNullOrEmpty(type) ? type : "FirstFloorBlockA";
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
                            Console.WriteLine($"Booking is confirmed. Do you want to again book another Pool (Y/N)?");
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
        }
    }
}