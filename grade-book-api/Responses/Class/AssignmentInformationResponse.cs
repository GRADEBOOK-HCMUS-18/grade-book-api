using ApplicationCore.Entity;

namespace grade_book_api.Responses.Class
{
    public class AssignmentInformationResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Point { get; set; }

        public AssignmentInformationResponse(Assignment source)
        {
            Id = source.Id;
            Name = source.Name;
            Point = source.Point;
        }
    }
}