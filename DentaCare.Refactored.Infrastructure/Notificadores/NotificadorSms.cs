using DentaCare.Refactored.Domain.Entities;
using DentaCare.Refactored.Domain.Interfaces;

namespace DentaCare.Refactored.Infrastructure.Notificadores;

public class NotificadorSms : INotificador
{
    public void Notificar(Cita cita, string mensaje)
    {
        Console.WriteLine($"[SMS] Para: {cita.Paciente.Celular} - {mensaje}");
    }
}
