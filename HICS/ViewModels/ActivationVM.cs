using CoreLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HICS.ViewModels
{
    public class ActivationVM
    {
        public int ActivationId { get; set; }
        public string CodeName { get; set; }
        public string LocationName { get; set; }
        public List<Code> Codes { get; set; } = new List<Code>();
        public int BadgeNo { get; set; }
    }
}
