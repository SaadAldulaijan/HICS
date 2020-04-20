using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLibrary.Entities
{
    public class Activation
    {
        public int ActivationId { get; set; }
        public int CodeId { get; set; }
        public int LocationId { get; set; }
        public DateTime ActivationTime { get; set; }
        public bool Status { get; set; }

        public Code Code { get; set; }
        public Location Location { get; set; }

    }
}
