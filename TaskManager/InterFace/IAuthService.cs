using TaskManager.Dtos;

namespace TaskManager.InterFace
{
    public interface IAuthService
    {
        Task Register(RegisterDto dto);
        Task<String> Login(LoginDto dto);
    }
}
