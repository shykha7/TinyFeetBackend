using TinyFeetBackend.Entities;

namespace TinyFeetBackend.Helpers.Interfaces
{
    public interface IJwtHelper
    {
        string GetJwtToken(User user);
    }
}
