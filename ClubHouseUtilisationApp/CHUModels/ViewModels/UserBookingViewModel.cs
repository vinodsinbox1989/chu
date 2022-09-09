using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHUModels.ViewModels
{
    public class UserBookingViewModel
    {
        public string FacilityType { get; set; }
        public string User { get; set; }
        public string EndDateTime { get; set; }
        public string StartDateTime { get; set; }
    }
}
