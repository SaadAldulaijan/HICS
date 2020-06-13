using CoreLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HICSManager.ViewModel
{
    public class GroupEmpVM
    {
        public List<Employee> Members { get; set; } = new List<Employee>();
        public List<Employee> NotMembers { get; set; } = new List<Employee>();
        public Group Group { get; set; }
    }
}
