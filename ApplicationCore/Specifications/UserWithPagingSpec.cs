using ApplicationCore.Entity;
using Ardalis.Specification;

namespace ApplicationCore.Specifications
{
    public class UserWithPagingSpec: Specification<User>
    {
        public UserWithPagingSpec(int numberOfUserPerPage, int pageNumber)
        {
            Query.OrderByDescending(user => user.Id)
                .Skip((pageNumber - 1) * numberOfUserPerPage)
                .Take(numberOfUserPerPage);
        }
    }
}