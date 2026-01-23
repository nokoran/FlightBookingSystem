using System.ComponentModel.DataAnnotations;

namespace FlightBookingSystem.Application.DTOs;

public class CreateBookingDto
{
    [Required]
    public int FlightId { get; set; }
    [Required]
    public int RowId { get; set; }
    [Required]
    public int LetterId { get; set; }
}