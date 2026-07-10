using ServiceDesk.Domain.Entities;

namespace ServiceDesk.Business.Interfaces
{
    public interface IAuthService
    {
        Usuario Autenticar(string email, string senha);
    }
}