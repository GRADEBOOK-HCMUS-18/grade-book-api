using ApplicationCore.Entity;
using Ardalis.Specification;

namespace ApplicationCore.Specifications
{
    public class AdminWithPagingSpec: Specification<AdminAccount>
    {
        public AdminWithPagingSpec(int numberPerPage, int pageNumber)
        {
            Query.Include(admin => admin.User)
                .OrderByDescending(admin => admin.User.Id)
                .Skip((pageNumber - 1) * numberPerPage)
                .Take(numberPerPage);
        }
    }
}