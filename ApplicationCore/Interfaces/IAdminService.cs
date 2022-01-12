using System.Collections.Generic;
using ApplicationCore.Entity;

namespace ApplicationCore.Interfaces
{
    public interface IAdminService
    {

        int CountTotalUser();
        List<User> GetPagedUsersList(int numberOfUserPerPage, int pageNumber);
    }
}