using System;

namespace grade_book_api.Responses.Class
{
    public class ClassMinimumInformationResponse
    {
        
        public int Id { get; set; }
        public string Name { get; set; }

        public ClassMinimumInformationResponse(ApplicationCore.Entity.Class source)
        {
            Id = source.Id;
            Name = source.Name;
            
        }
    }
}