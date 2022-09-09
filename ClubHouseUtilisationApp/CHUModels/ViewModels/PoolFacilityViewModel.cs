using CHU.Utilties;
using CHUModels;
using CHUModels.ViewModels;

namespace CHUService.Facilities
{
    public class PoolFacilityViewModel : BaseFacilityViewModel
    {
        public PoolFacilityViewModel(string name, DateTime facilityStartDate, DateTime facilityEndDate, MaintainanceType MaintainanceType)
        {
            TimingModel.OpenDate = facilityStartDate;
            TimingModel.CloseDate = facilityEndDate;
            PoolMaintainanceType = MaintainanceType;
            Name = name;

        }
        public PoolFacilityViewModel(string name, DateTime facilityStartDate, DateTime facilityEndDate, MaintainanceType MaintainanceType, LockingModel? lockingTime = null)
        {
            TimingModel.OpenDate = facilityStartDate;
            TimingModel.CloseDate = facilityEndDate;
            PoolMaintainanceType = MaintainanceType;
            Name = name;
            LockingTime = lockingTime;
        }

        public PoolFacilityViewModel(string name, DateTime facilityStartDate, DateTime facilityEndDate, MaintainanceType MaintainanceType,
            LockingModel? lockingTime = null, MaintenancePeriodModel? maintenancePeriod = null)
        {
            TimingModel.OpenDate = facilityStartDate;
            TimingModel.CloseDate = facilityEndDate;
            PoolMaintainanceType = MaintainanceType;
            Name = name;
            LockingTime = lockingTime;
            MaintenancePeriod = maintenancePeriod;
        }
        public override bool IsBookingRequired { get; set; } = true;
        public TimingModel TimingModel { get; set; } = new();

        public override MaintenancePeriodModel MaintenancePeriod { get; set; } = new();
        public MaintainanceType PoolMaintainanceType { get; set; }
        public string Name { get; set; }
        public override LockingModel LockingTime { get; set; } = new();
    }
}