using CHUModels.ViewModels;
using Newtonsoft.Json;

namespace CHUService.Facilities
{
    public abstract class BaseFacility
    {
        public static List<UserBookingViewModel> UserBookingViews = new();
        public BaseFacility()
        {
            ReadBookingInfoByUser(UserName);
        }

        public static string BookingDataFile = GetCombinedPath("BookingData.txt");
        public string? UserName { get; set; }

        public abstract string Type { get; set; }
        public static string GetCombinedPath(string fileName)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
        }

        public static async Task LogUserBooking(string user, string facilityType, string startDateTime, string endDateTime)
        {
            var data = new UserBookingViewModel()
            {
                User = user,
                FacilityType = facilityType,
                EndDateTime = endDateTime,
                StartDateTime = startDateTime
            };
            var jsonData = JsonConvert.SerializeObject(data);

            using (StreamWriter file = new(BookingDataFile, append: true))
            {
                await file.WriteLineAsync(jsonData);
            }
        }

        public static List<UserBookingViewModel> ReadBookingInfoByUser(string UserName)
        {
            using (StreamReader sr = new StreamReader(BookingDataFile))
            {
                while (!sr.EndOfStream)
                {
                    string json = sr.ReadLine();
                    var item = JsonConvert.DeserializeObject<UserBookingViewModel>(json);
                    if (UserName == item.User)
                    {
                        UserBookingViews.Add(item);
                    }
                }
            }
            return UserBookingViews;
        }
    }
}
