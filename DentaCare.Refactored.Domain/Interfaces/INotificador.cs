using DentaCare.Refactored.Domain.Entities;

namespace DentaCare.Refactored.Domain.Interfaces;

public interface INotificador
{
    void Notificar(Cita cita, string mensaje);
}
