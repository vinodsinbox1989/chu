namespace CHUModels
{
    public class BookingModel
    {
        public int MaxUserPerSlot { get; set; }

        public int BookingDuration { get; set; }

        public int MaxDurationConsiderAfterCheckInTime { get; set; }
    }
}