using DentaCare.Refactored.Domain.Entities;
using DentaCare.Refactored.Domain.Interfaces;

namespace DentaCare.Refactored.Infrastructure.Notificadores;

public class NotificadorEmail : INotificador
{
    public void Notificar(Cita cita, string mensaje)
    {
        Console.WriteLine($"[EMAIL] Para: {cita.Paciente.Correo} - {mensaje}");
    }
}
