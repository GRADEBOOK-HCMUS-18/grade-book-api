using ApplicationCore.Entity;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApplicationCore.Services
{
    public class InvitationService : IInvitationService
    {
        private readonly IBaseRepository<Class> _classRepository;

        public InvitationService(IBaseRepository<Class> classRepository)
        {
            _classRepository = classRepository;
        }

        public Class GetClassFromInvitation(string inviteString)
        {
            var foundClass = _classRepository.GetFirst(cl =>
                    cl.InviteStringStudent == inviteString || cl.InviteStringTeacher == inviteString,
                cl => cl.Include(c => c.MainTeacher));
            if (foundClass is null)
                return null;
            return foundClass;
        }
    }
}