using System.Collections.Generic;
using ApplicationCore.Entity;

namespace ApplicationCore.Interfaces
{
    public interface IAdminService
    {

        int CountTotalUser();
        List<User> GetPagedUsersList(int numberOfUserPerPage, int pageNumber);

        int CountTotalClass();

        List<Class> GetPagedClassesList(int numberOfClassPerPage, int pageNumber);

        int CountTotalAdmin();
        List<AdminAccount> GetPagedAdminsList(int numberPerPage, int pageNumber); 

        User SetLockStateOfUser(int userId, bool newState);
    }
}