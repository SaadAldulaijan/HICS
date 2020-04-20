using System.Collections.Generic;

namespace CoreLibrary.Entities
{
    public class Location
    {
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public int AreaCode { get; set; }

        public virtual ICollection<Activation> Activations { get; set; }
    }
}