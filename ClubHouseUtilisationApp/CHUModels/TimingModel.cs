namespace CHUModels
{
    public class TimingModel
    {
        public DateTime OpenDate { get; set; }

        public DateTime CloseDate { get; set; }

        public List<(DateTime StartDate, DateTime EndTime)> BreakTimes { get; set; } = new();
    }
}