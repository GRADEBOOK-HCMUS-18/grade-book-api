namespace ApplicationCore.Entity
{
    public class Assignment: BaseEntity
    {
        public string Name { get; set; }
        public int Point { get; set; }
        public int Priority { get; set; }
        public Class Class { get; set; }
    }
}