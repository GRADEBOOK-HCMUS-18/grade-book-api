namespace ApplicationCore.Entity
{
    public class ClassStudentsAccount : BaseEntity
    {
        public int ClassId { get; set; }
        public Class Class { get; set; }
        public int StudentAccountId { get; set; }
        public User Student { get; set; }
    }
}