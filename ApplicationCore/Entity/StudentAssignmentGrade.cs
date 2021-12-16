namespace ApplicationCore.Entity
{
    public class StudentAssignmentGrade
    {
        public int StudentAssignmentGradeId { get; set; }
        public int StudentId;
        public Student Student { get; set; }
        public int AssignmentId { get; set; }
        public Assignment Assignment { get; set; }
        public int Point { get; set; }
        
        public bool IsFinalized { get; set; }
    }
}