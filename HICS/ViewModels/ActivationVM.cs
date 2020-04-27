using CoreLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HICS.ViewModels
{
    public class ActivationVM
    {
        //public int ActivationId { get; set; }
        //public int CodeId { get; set; }
        //public int LocationId { get; set; }
        //public int BadgeNo { get; set; }
        //public string CodeName { get; set; }
        //public string LocationName { get; set; }
        public List<Code> Codes { get; set; }
        public Employee Employee { get; set; }
        public Code Code { get; set; }
        public Location Location { get; set; }
    }
}
