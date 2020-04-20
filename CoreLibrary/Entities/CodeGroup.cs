namespace CoreLibrary.Entities
{
    public class CodeGroup
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int CodeId { get; set; }
        public string Description { get; set; }

        public Group Group { get; set; }
        public Code Code { get; set; }
    }
}