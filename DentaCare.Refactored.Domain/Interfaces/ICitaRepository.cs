using DentaCare.Refactored.Domain.Entities;
using DentaCare.Refactored.Domain.ValueObjects;

namespace DentaCare.Refactored.Domain.Interfaces;

public interface ICitaRepository
{
    void Guardar(Cita cita);
    void Actualizar(Cita cita);
    Cita? ObtenerPorId(string id);
    ResumenCitas ObtenerResumen();
}
