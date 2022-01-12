using System.Collections.Generic;
using System.Linq;
using grade_book_api.Responses.Class;

namespace grade_book_api.Responses.Admin
{
    public class PagedClassListResponse
    {
        
        public int NumberPerPage { get; set; }
        public int PageNumber { get; set; }
        public int TotalClass { get; set; }
        public List<ClassShortInformationResponse> Classes { get; set; } = new();

        public PagedClassListResponse(int numberPerPage, int pageNumber, int totalClass, List<ApplicationCore.Entity.Class> classes)
        {
            NumberPerPage = numberPerPage;
            PageNumber = pageNumber;
            TotalClass = totalClass;
            Classes = classes.Select(cl => new ClassShortInformationResponse(cl, "teacher", cl.MainTeacher))
                .ToList();

        }
    }
}