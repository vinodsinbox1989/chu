namespace CHU.Utilties
{
    public enum MaintenancePeriodType
    {
        OnDemand = 1,
        PreDefined
    }

    public enum RecurringType
    {
        DefaultEmpty = 0,
        Weekly,
        Monthly,
        Yearly
    }

    public enum MaintainanceType
    {
        Available,
        PartiallyAvailable,
        NotAvailable
    }
}