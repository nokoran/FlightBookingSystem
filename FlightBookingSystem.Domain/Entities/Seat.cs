namespace FlightBookingSystem.Domain.Entities;

public class Seat
{
    public int Id { get; set; }
    public string SeatNumber { get; set; } = string.Empty;
    public bool IsBooked { get; set; } = false;

    public int FlightId { get; set; }
    public Flight Flight { get; set; } = null!;
}