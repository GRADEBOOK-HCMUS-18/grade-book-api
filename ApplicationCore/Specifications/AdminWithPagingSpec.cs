using ApplicationCore.Entity;
using Ardalis.Specification;

namespace ApplicationCore.Specifications
{
    public class AdminWithPagingSpec: Specification<AdminAccount>
    {
        public AdminWithPagingSpec(int numberPerPage, int pageNumber)
        {
            Query
                .OrderByDescending(admin => admin.Id)
                .Skip((pageNumber - 1) * numberPerPage)
                .Take(numberPerPage);
        }
    }
}