using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HICSDashboard.ViewModels
{
    public class DashboardVM
    {
        public int Id { get; set; }
        public string CodeName { get; set; }
        public string LocationName { get; set; }
        public string EmployeeName { get; set; }
        public DateTime DateTime { get; set; }
        // activate or clear
        public bool Status { get; set; }
        // activator device name , ip and MAC
        public string DeviceName { get; set; }
        public string IPAddress { get; set; }
        public string MAC { get; set; }
        // extension is based on location, each location should be associated with one ext.
        public string Extension { get; set; }
        // group pager no.
        public int PagerNo { get; set; }
        // these two should be List<Status>
        public bool SmsStatus { get; set; }
        public bool NotificationStatus { get; set; }
        // employees who receive the notifications
    }
}
