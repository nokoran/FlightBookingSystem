using FlightBookingSystem.Application.DTOs;

namespace FlightBookingSystem.Application.Interfaces;

public interface IAuthService
{
    Task<bool> RegisterAsync(RegisterDto dto);
    Task<string> LoginAsync(LoginDto dto); // returns JWT token
}