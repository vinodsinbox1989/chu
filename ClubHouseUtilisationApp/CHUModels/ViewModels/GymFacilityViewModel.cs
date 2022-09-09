using CHU.Utilties;
using CHUModels;
using CHUModels.ViewModels;

namespace CHUService.Facilities
{
    public class GymFacilityViewModel : BaseFacilityViewModel
    {
        public GymFacilityViewModel(string name, int maxAllowedBlockageLimit, DateTime facilityStartDate, DateTime facilityEndDate, MaintainanceType gymMaintainanceType)
        {
            BookingModel.MaxUserPerSlot = maxAllowedBlockageLimit;
            this.TimingModel.OpenDate = facilityStartDate;
            this.TimingModel.CloseDate = facilityEndDate;
            Name = name;
            GymMaintainanceType = gymMaintainanceType;
        }
        public GymFacilityViewModel(string name, int maxAllowedBlockageLimit, DateTime facilityStartDate, DateTime facilityEndDate, MaintainanceType gymMaintainanceType, LockingModel? lockingTime = null)
        {
            BookingModel.MaxUserPerSlot = maxAllowedBlockageLimit;
            this.TimingModel.OpenDate = facilityStartDate;
            this.TimingModel.CloseDate = facilityEndDate;
            Name = name;
            GymMaintainanceType = gymMaintainanceType;
            LockingTime = lockingTime;
        }

        public GymFacilityViewModel(string name, int maxAllowedBlockageLimit, DateTime facilityStartDate, DateTime facilityEndDate, MaintainanceType gymMaintainanceType, LockingModel? lockingTime = null,
            MaintenancePeriodModel? maintenancePeriod = null)
        {
            BookingModel.MaxUserPerSlot = maxAllowedBlockageLimit;
            this.TimingModel.OpenDate = facilityStartDate;
            this.TimingModel.CloseDate = facilityEndDate;
            Name = name;
            GymMaintainanceType = gymMaintainanceType;
            LockingTime = lockingTime;
            MaintenancePeriod = maintenancePeriod;
        }
        public override bool IsBookingRequired { get; set; } = true;
        public TimingModel TimingModel { get; set; } = new();
        public override MaintenancePeriodModel MaintenancePeriod { get; set; } = new();
        public BookingModel BookingModel { get; set; } = new();
        public MaintainanceType GymMaintainanceType { get; set; }
        public string Name { get; set; }

        public override LockingModel LockingTime { get; set; } = new();
    }
}