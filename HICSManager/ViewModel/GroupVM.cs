using CoreLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HICSManager.ViewModel
{
    public class GroupVM
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }

        public List<Employee> Members { get; set; }
        public List<Code> AssociatedCodes { get; set; }

    }
}
