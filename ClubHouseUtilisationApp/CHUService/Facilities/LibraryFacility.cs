﻿using CHUService.ViewModels;

namespace CHUService.Facilities
{
    public class LibraryFacility : IBaseFacility
    {
        private static readonly List<LibraryFacilityViewModel> libraryFacilities = new();
        public LibraryFacility()
        {
            libraryFacilities.Add(new LibraryFacilityViewModel("FirstFloorBlockA", 3, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 09, 00, 00), new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 00, 00)) { });
            libraryFacilities.Add(new LibraryFacilityViewModel("SecondFloorBlockA", 50, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 09, 00, 00), new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 13, 00, 00)) { });
            libraryFacilities.Add(new LibraryFacilityViewModel("FirstFloorBlockB", 100, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 09, 00, 00), new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 00, 00)) { });
            libraryFacilities.Add(new LibraryFacilityViewModel("SecondFloorBlockB", 100, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 09, 00, 00), new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 13, 00, 00)) { });
        }

        public void Book()
        {
            Console.WriteLine($"Enter your Library Name from the available list: {string.Join(",", libraryFacilities.Select(x => x.Name))} ");
            var type = Console.ReadLine();
            type = !String.IsNullOrEmpty(type) ? type : "FirstFloorBlockA";
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