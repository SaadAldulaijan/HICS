using System;
using System.Collections.Generic;
using System.Text;

namespace HICS.Library.Models
{
    public class Membership
    {
        public int EmployeeId { get; set; }
        public int GroupId { get; set; }
        public DateTime AssignDate { get; set; }
    }
}
