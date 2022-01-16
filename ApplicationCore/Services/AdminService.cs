using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationCore.Entity;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using SharedKernel;

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

        public AdminAccount GetAdminByUsername(string userName)
        {
            var foundAccount = _adminAccountRepository.GetFirst(admin => admin.Username == userName);
            if (foundAccount is null)
                return null;
            return foundAccount;
        }

        public AdminAccount CreateNewAdminAccount(string userName, string password, bool isSuperAdmin)
        {
            var foundAccount = GetAdminByUsername(userName);
            if (foundAccount is not null)
                throw new InvalidOperationException($"Admin with username: {userName} already existed");
            var newAccount = new AdminAccount(userName, password, isSuperAdmin);

            _adminAccountRepository.Insert(newAccount);

            return newAccount;
        }

        public string TryGetAdminToken(string userName, string password)
        {

            var foundAdmin = GetAdminByUsername(userName);

            if (foundAdmin is null)
                return null;
            var success = PasswordHelper
                .CheckPasswordHash(password, foundAdmin.PasswordHash, foundAdmin.PasswordSalt);

            if (!success)
                return null;
            return PasswordHelper.GenerateJwtToken(foundAdmin.Id, foundAdmin.Username);
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

        public int CountTotalAdmin()
        {
            return _adminAccountRepository.Count();
        }

        public List<AdminAccount> GetPagedAdminsList(int numberPerPage, int pageNumber)
        {
            var adminList =
                _adminAccountRepository.List(new AdminWithPagingSpec(numberPerPage, pageNumber));
            return adminList.ToList();
        }

        public User SetLockStateOfUser(int userId, bool newState)
        {
            var foundUser = _userRepository.GetFirst(user => user.Id == userId);
            if (foundUser is null)
                return null; 
            foundUser.SetLockAccount(newState);

            _userRepository.Update(foundUser);
            return foundUser;
        }

        public User SetUserStudentIdentification(int userId, string newStudentId)
        {
            var foundUser = _userRepository.GetFirst(user => user.Id == userId);
            if (foundUser is null)
                return null;
            var foundExistedStudentId = _userRepository.GetFirst(user => user.StudentIdentification == newStudentId);
            if (foundExistedStudentId is not null)
                throw new InvalidOperationException($"Student with StudentId: {newStudentId} already existed");

            foundUser.StudentIdentification = newStudentId;
            _userRepository.Update(foundUser);

            return foundUser;
        }
    }
}