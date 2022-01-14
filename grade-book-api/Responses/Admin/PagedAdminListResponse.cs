using System.Collections.Generic;
using System.Linq;
using ApplicationCore.Entity;
using CloudinaryDotNet.Actions;

namespace grade_book_api.Responses.Admin
{
    public class PagedAdminListResponse
    {
        public int NumberPerPage { get; set; }
        public int PageNumber { get; set; }
        public int TotalAdmin { get; set; }

        public List<AdminAccountResponse> Admins { get; set; }

        public PagedAdminListResponse(int numberPerPage, int pageNumber, int totalAdmin, List<AdminAccount> listAdmin)
        {
            NumberPerPage = numberPerPage;
            PageNumber = pageNumber;
            TotalAdmin = totalAdmin;
            Admins = listAdmin.Select(ad => new AdminAccountResponse(ad)).ToList();
        }
    }
}