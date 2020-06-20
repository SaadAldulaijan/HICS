using HICS.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HICSManager.ViewModel
{
    public class CodeGroupVM
    {
        public List<Group> AssignedGroups { get; set; }
        public List<Group> UnassignedGroups { get; set; }
        public Code Code { get; set; }

    }
}
