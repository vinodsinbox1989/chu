namespace CHUModels.ViewModels
{
    public class LibraryFacilityViewModel : BaseFacilityViewModel
    {
        public LibraryFacilityViewModel(string name, int maxAllowedBlockageLimit, DateTime facilityStartDate, DateTime facilityEndDate)
        {
            BookingModel.MaxUserPerSlot = maxAllowedBlockageLimit;
            TimingModel.OpenDate = facilityStartDate;
            TimingModel.CloseDate = facilityEndDate;
            Name = name;
        }
        public LibraryFacilityViewModel(string name, int maxAllowedBlockageLimit, DateTime facilityStartDate, DateTime facilityEndDate, LockingModel? lockingTime = null)
        {
            BookingModel.MaxUserPerSlot = maxAllowedBlockageLimit;
            TimingModel.OpenDate = facilityStartDate;
            TimingModel.CloseDate = facilityEndDate;
            Name = name;
            LockingTime = lockingTime;
        }

        public LibraryFacilityViewModel(string name, int maxAllowedBlockageLimit, DateTime facilityStartDate, DateTime facilityEndDate, LockingModel? lockingTime = null,
            MaintenancePeriodModel? maintenancePeriod = null)
        {
            BookingModel.MaxUserPerSlot = maxAllowedBlockageLimit;
            TimingModel.OpenDate = facilityStartDate;
            TimingModel.CloseDate = facilityEndDate;
            Name = name;
            LockingTime = lockingTime;
            MaintenancePeriod = maintenancePeriod;
        }
        public BookingModel BookingModel { get; set; } = new();
        public override bool IsBookingRequired { get; set; } = false;
        public TimingModel TimingModel { get; set; } = new();
        public string Name { get; set; }
        public override MaintenancePeriodModel MaintenancePeriod { get; set; } = new();
        public override LockingModel LockingTime { get; set; } = new();
    }
}
