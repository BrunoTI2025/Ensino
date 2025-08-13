using gestao_estagios.Models;

namespace gestao_estagios.Services
{
    public interface IAuthService
    {
        string GenerateToken(Usuario user);
    }
}
