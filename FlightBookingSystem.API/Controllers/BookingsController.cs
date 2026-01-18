using FlightBookingSystem.Application.DTOs;
using FlightBookingSystem.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FlightBookingSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class BookingsController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingsController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    // POST: api/Bookings
    [HttpPost]
    public async Task<IActionResult> Book([FromBody] BookingDto dto)
    {
        try
        {
            // get user id from claims
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Не вдалося визначити користувача.");

            await _bookingService.BookSeatAsync(dto, userId);

            return Ok(new { message = "Місце успішно заброньовано!" });
        }
        catch (Exception ex)
        {
            // return 400 for any booking errors with corresponding message
            return BadRequest(new { message = ex.Message });
        }
    }

    // GET: api/Bookings/my-bookings
    [HttpGet("my-bookings")]
    public async Task<IActionResult> GetMyBookings()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var bookings = await _bookingService.GetMyBookingsAsync(userId);

        return Ok(bookings);
    }

    // DELETE: api/Bookings/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> CancelBooking(int id)
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _bookingService.CancelBookingAsync(id, userId);

            return Ok(new { message = "Бронювання скасовано, місце звільнено." });
        }
        catch (KeyNotFoundException)
        {
            // 404 if booking not found
            return NotFound(new { message = "Бронювання не знайдено." });
        }
        catch (UnauthorizedAccessException)
        {
            // 403 if user tries to cancel someone else's booking
            return StatusCode(403, new { message = "Ви не маєте права скасовувати це бронювання." });
        }
        catch (Exception ex)
        {
            // 400 for other errors
            return BadRequest(new { message = ex.Message });
        }
    }
}