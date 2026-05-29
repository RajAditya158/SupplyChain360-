using Supplychain.DTOs.Admin;

public interface IAuthService
{

    bool Register(RegisterDto registerDto);

    LoginResponse Login(LoginDto loginDto);
}