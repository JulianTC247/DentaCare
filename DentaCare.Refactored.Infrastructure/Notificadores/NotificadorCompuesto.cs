using DentaCare.Refactored.Domain.Entities;
using DentaCare.Refactored.Domain.Interfaces;

namespace DentaCare.Refactored.Infrastructure.Notificadores;

public class NotificadorCompuesto : INotificador
{
    private readonly IEnumerable<INotificador> _notificadores;

    public NotificadorCompuesto(params INotificador[] notificadores)
    {
        _notificadores = notificadores ?? throw new ArgumentNullException(nameof(notificadores));
    }

    public void Notificar(Cita cita, string mensaje)
    {
        foreach (INotificador notificador in _notificadores)
        {
            notificador.Notificar(cita, mensaje);
        }
    }
}
