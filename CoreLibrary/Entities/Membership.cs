using System;

namespace CoreLibrary.Entities
{
    public class Membership
    {
        public int EmployeeId { get; set; }
        public int GroupId { get; set; }
        public DateTime AssignDate { get; set; }

        public Employee Employee { get; set; }
        public Group Group { get; set; }
    }
}