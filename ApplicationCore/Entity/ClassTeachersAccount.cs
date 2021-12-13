namespace ApplicationCore.Entity
{
    public class ClassTeachersAccount : BaseEntity
    {
        public int ClassId { get; set; }
        public Class Class { get; set; }
        public int TeacherId { get; set; }
        public User Teacher { get; set; }
    }
}