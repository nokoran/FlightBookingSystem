using FlightBookingSystem.Application.DTOs;
using FlightBookingSystem.Application.Interfaces;
using FlightBookingSystem.Domain.Entities;
using FlightBookingSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FlightBookingSystem.Application.Interfaces;

public interface IBookingService
{
    public Task BookSeatAsync(BookingDto dto, string userId);
    Task CancelBookingAsync(int bookingId, string userId);
    public Task<IEnumerable<object>> GetMyBookingsAsync(string userId);
    
}