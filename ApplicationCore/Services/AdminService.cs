using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationCore.Entity;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;

namespace ApplicationCore.Services
{
    public class AdminService: IAdminService
    {
        private readonly IBaseRepository<AdminAccount> _adminAccountRepository;
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<Class> _classRepository;
        public AdminService(IBaseRepository<AdminAccount> adminAccountRepository, IBaseRepository<User> userRepository, IBaseRepository<Class> classRepository)
        {
            _adminAccountRepository = adminAccountRepository;
            _userRepository = userRepository;
            _classRepository = classRepository;
        }
        public int CountTotalUser()
        {
            return _userRepository.Count();
        }

        public List<User> GetPagedUsersList(int numberOfUserPerPage, int pageNumber)
        {
            var usersList = _userRepository.List(
                new UserWithPagingSpec(numberOfUserPerPage, pageNumber));

            return usersList.ToList();
        }

        public int CountTotalClass()
        {
            return _classRepository.Count();
        }

        public List<Class> GetPagedClassesList(int numberOfClassPerPage, int pageNumber)
        {
            var classesList = 
                _classRepository.List(new ClassWithPagingSpec(numberOfClassPerPage, pageNumber));
            return classesList.ToList();

        }
    }
}