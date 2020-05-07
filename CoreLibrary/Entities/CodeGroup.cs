namespace CoreLibrary.Entities
{
    public class CodeGroup
    {
        public int GroupId { get; set; }
        public int CodeId { get; set; }

        public Group Group { get; set; }
        public Code Code { get; set; }
    }
}