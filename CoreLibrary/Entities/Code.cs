using System.Collections.Generic;

namespace CoreLibrary.Entities
{
    public class Code
    {
        public int CodeId { get; set; }
        public string CodeName { get; set; }
        public string CodeColor { get; set; }
        public int PagerNo { get; set; }

        public virtual ICollection<CodeGroup> CodeGroups { get; set; }
        public virtual ICollection<Activation> Activations { get; set; }
    }
}