using ApplicationCore.Entity;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;

namespace ApplicationCore.Specifications
{
    public class ClassWithPagingSpec: Specification<Class>
    {
        public ClassWithPagingSpec(int numberPerPage, int pageNumber)
        {
            Query.Include(cl => cl.MainTeacher)
                .OrderByDescending(cl => cl.Id)
                .Skip((pageNumber - 1) * numberPerPage)
                .Take(numberPerPage);
        }
    }
}