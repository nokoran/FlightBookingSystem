using FlightBookingSystem.Application.DTOs;
using FlightBookingSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FlightBookingSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FlightsController : ControllerBase
{
    private readonly IFlightService _service;

    public FlightsController(IFlightService service)
    {
        _service = service;
    }

    // POST: api/Flights
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateFlightDto dto)
    {
        await _service.CreateFlightAsync(dto);
        return Ok(new { message = "Рейс успішно створено!" });
    }

    // GET: api/Flights
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var flights = await _service.GetAllFlightsAsync();
        return Ok(flights);
    }
    
    // GET: api/Flights/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var flight = await _service.GetFlightByIdAsync(id);
        if (flight == null) return NotFound(new { message = "Рейс не знайдено" });
        return Ok(flight);
    }

    // PUT: api/Flights/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateFlightDto dto)
    {
        try
        {
            await _service.UpdateFlightAsync(id, dto);
            return Ok(new { message = "Рейс оновлено успішно" });
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new { message = "Рейс не знайдено" });
        }
    }

    // DELETE: api/Flights/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _service.DeleteFlightAsync(id);
            return Ok(new { message = "Рейс видалено" });
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new { message = "Рейс не знайдено" });
        }
    }
}