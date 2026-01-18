namespace FlightBookingSystem.Domain.Entities;

public class Booking
{
    public int Id { get; set; }
    public DateTime BookingDate { get; set; } = DateTime.UtcNow;
    
    public bool IsCancelled { get; set; } = false;
        
    //who booked?
    public string UserId { get; set; } // ID from Identity
    public User User { get; set; }

    // which flight?
    public int FlightId { get; set; }
    public Flight Flight { get; set; }

    // which seat?
    public int SeatId { get; set; }
    public Seat Seat { get; set; }
}