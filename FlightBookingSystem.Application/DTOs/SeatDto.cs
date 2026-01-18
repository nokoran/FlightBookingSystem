namespace FlightBookingSystem.Application.DTOs;

public class SeatDto
{
    public int Id { get; set; }
    public string SeatNumber { get; set; }
    public bool IsBooked { get; set; }
}