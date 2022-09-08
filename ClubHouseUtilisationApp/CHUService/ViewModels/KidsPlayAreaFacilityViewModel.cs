using CHU.Utilties;
using CHUModels;
using CHUService.ViewModels;

namespace CHUService.Facilities
{
    public class KidsPlayAreaFacilityViewModel : BaseFacilityViewModel
    {
        public KidsPlayAreaFacilityViewModel(string name, DateTime facilityStartDate, DateTime facilityEndDate, MaintainanceType maintainanceType)
        {
            TimingModel.OpenDate = facilityStartDate;
            TimingModel.CloseDate = facilityEndDate;
            Name = name;
            MaintainanceType = maintainanceType;
        }

        public KidsPlayAreaFacilityViewModel(string name, DateTime facilityStartDate, DateTime facilityEndDate, MaintainanceType maintainanceType, LockingModel? lockingTime = null)
        {
            TimingModel.OpenDate = facilityStartDate;
            TimingModel.CloseDate = facilityEndDate;
            Name = name;
            MaintainanceType = maintainanceType;
            LockingTime = lockingTime;
        }

        public KidsPlayAreaFacilityViewModel(string name, DateTime facilityStartDate, DateTime facilityEndDate, MaintainanceType maintainanceType, LockingModel? lockingTime = null,
            MaintenancePeriodModel? maintenancePeriod = null)
        {
            TimingModel.OpenDate = facilityStartDate;
            TimingModel.CloseDate = facilityEndDate;
            Name = name;
            MaintainanceType = maintainanceType;
            LockingTime = lockingTime;
            MaintenancePeriod = maintenancePeriod;
        }

        public override bool IsBookingRequired { get; set; } = false;
        public TimingModel TimingModel { get; set; } = new();

        public override MaintenancePeriodModel MaintenancePeriod { get; set; } = new();

        public MaintainanceType MaintainanceType { get; set; }
        public string Name { get; set; }

        public override LockingModel LockingTime { get; set; } = new();
    }
}