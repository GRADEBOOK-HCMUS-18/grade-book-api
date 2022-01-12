using System.Collections.Generic;
using System.Linq;
using grade_book_api.Responses.User;

namespace grade_book_api.Responses.Admin
{
    public class PagedUserListResponse
    {
        public int NumberPerPage { get; set; }
        public int PageNumber { get; set; }
        public int TotalUser { get; set; }
        public List<UserDetailedInformationResponse> Users { get; set; } = new();

        public PagedUserListResponse(int numberPerPage, int pageNumber, int totalUser,
            List<ApplicationCore.Entity.User> users)
        {
            NumberPerPage = numberPerPage;
            PageNumber = pageNumber;
            TotalUser = totalUser;

            Users = users.Select(user => new UserDetailedInformationResponse(user)).ToList();
        }
    }
}