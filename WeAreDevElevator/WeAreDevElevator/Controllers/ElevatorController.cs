using Microsoft.AspNetCore.Mvc;
using System;
using WeAreDevElevator.Models;

[ApiController]
[Route("[controller]")]
public class ElevatorController : ControllerBase
{
    private static Elevator _elevator = new Elevator();

    [HttpGet("status")]
    public IActionResult GetStatus()
    {
        return Ok(_elevator);
    }

    [HttpPost("call")]
    public IActionResult CallElevator([FromBody] int floor)
    {
        if (floor < 1) return BadRequest(new
        {
            message = $"No contamos con sotanos :({Environment.NewLine}Intenta ingresar otro valor."
        });
        if (floor > 12) return BadRequest(new
        {
            message = $"!Planeamos seguir creciendo¡{Environment.NewLine}Actualmente nuestro límite son 12 pisos :)"
        });

        _elevator.Requests.Add(floor);
        Console.WriteLine($"Solicitudes pendientes {String.Join(",", _elevator.Requests)}");
        if (!_elevator.IsMoving)
        {
            _elevator.IsMoving = true;
            // Simulate elevator movement
            MoveElevator();
        }
        return Ok();
    }

    private async void MoveElevator()
    {
        AddMessage("Elevador en movimiento...");
        Random random = new Random();
        while (_elevator.IsMoving && _elevator.Requests.Any())
        {
            var nextFloor = _elevator.Requests.First();
            bool move = true;
            while (move)
            {
                if (random.Next(1, 6) == 1) //Probabilidad del 20%
                {
                    AddMessage("Elevador solicitado por otro usuario");
                    CallElevator(random.Next(1, 13));
                }
                if (_elevator.Requests.Any())
                {
                    _elevator.DistinctRequests = _elevator.Requests.Distinct().ToList();
                    nextFloor = _elevator.Requests.First();
                    await Task.Delay(1000);
                    if (_elevator.Requests.Contains(_elevator.CurrentFloor))
                    {
                        int countPassengers = _elevator.Requests.Count(n => n == _elevator.CurrentFloor);
                        AddMessage($"Abrir puertas...{Environment.NewLine} {((countPassengers == 1) ? "Baja 1 pasajero" : "Bajan " + countPassengers + " pasajeros")} {Environment.NewLine}Cerrar puertas.");
                        _elevator.Requests.RemoveAll(n => n == _elevator.CurrentFloor);
                    }
                    if (_elevator.CurrentFloor < nextFloor)
                    {
                        _elevator.CurrentFloor++;
                    }
                    else
                    {
                        _elevator.CurrentFloor--;
                    }
                }
                else
                {
                    move = false;
                }
            }
        }
        _elevator.IsMoving = false;
    }

    private async void AddMessage(string message)
    {
        if(_elevator.Messages.Count() > 10)
        {
            _elevator.Messages.RemoveAt(0);
        }
        _elevator.Messages.Add(message);
    }
}