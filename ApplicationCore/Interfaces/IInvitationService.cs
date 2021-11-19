using ApplicationCore.Entity;

namespace ApplicationCore.Interfaces
{
    public interface IInvitationService
    {
        Class GetClassFromInvitation(string inviteString);
    }
}