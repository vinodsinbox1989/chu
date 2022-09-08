using CHUModels;

namespace CHUService.ViewModels
{
    public abstract class BaseFacilityViewModel
    {
        //public abstract int MaxAllowedBlockageLimit { get; set; }

        public abstract bool IsBookingRequired { get; set; }

        public abstract LockingModel? LockingTime { get; set; }

        public abstract MaintenancePeriodModel? MaintenancePeriod { get; set; }

        //public abstract DateTime FacilityStartDate { get; set; }

        //public abstract DateTime FacilityEndDate { get; set; }

        // public abstract void Book();
    }
}
