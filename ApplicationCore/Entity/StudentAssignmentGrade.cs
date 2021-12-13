namespace ApplicationCore.Entity
{
    public class StudentAssignmentGrade
    {
        public int StudentAssignmentGradeId { get; set; }
        public Student Student { get; set; }
        public Assignment Assignment { get; set; }
    }
}