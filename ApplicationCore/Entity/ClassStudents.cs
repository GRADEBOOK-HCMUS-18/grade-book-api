namespace ApplicationCore.Entity
{
    public class ClassStudents : BaseEntity
    {
        public int ClassId { get; set; }
        public Class Class { get; set; }
        public int StudentId { get; set; }
        public User Student { get; set; }
    }
}