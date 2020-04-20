using System.Collections.Generic;

namespace CoreLibrary.Entities
{
    public class Group
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Membership> Memberships { get; set; }
        public virtual ICollection<CodeGroup> CodeGroups { get; set; }
    }
}