using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HICS.Library.Models;

namespace HICS.ViewModels
{
    public class ActivationVM
    {
        public List<Code> Codes { get; set; }
        public Employee Employee { get; set; }
        public Code Code { get; set; }
        public Location Location { get; set; }
    }
}
