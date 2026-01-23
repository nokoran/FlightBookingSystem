namespace FlightBookingSystem.Application.DTOs;

public class BookingDto
{
    public int BookingId { get; set; }
    public string FlightNumber { get; set; }
    public string DepartureCity { get; set; }
    public string ArrivalCity { get; set; }
    public string Seat { get; set; }
    public DateTime Date { get; set; }
    public bool IsCancelled { get; set; }
    
}