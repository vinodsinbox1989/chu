using CHU.Utilties;

namespace CHUModels
{
    public class MaintenancePeriodModel
    {
        public MaintenancePeriodType PeriodType { get; set; }

        private int _blockageLimit;
        public int MaxAllowedBlockageLimit
        {
            get
            {
                return this._blockageLimit;
            }
            set
            {
                if (this.PeriodType == MaintenancePeriodType.PreDefined)
                {
                    this._blockageLimit = value;
                }
                else if (this.PeriodType == MaintenancePeriodType.OnDemand)
                {
                    this._blockageLimit = 0;
                }
            }
        }

        #region
        public string StartDateTime { get; set; }

        public string EndDateTime { get; set; }

        public RecurringType RecurringType { get; set; }

        #endregion

    }
}