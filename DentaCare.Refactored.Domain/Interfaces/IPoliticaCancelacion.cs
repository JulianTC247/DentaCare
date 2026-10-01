using DentaCare.Refactored.Domain.Entities;

namespace DentaCare.Refactored.Domain.Interfaces;

public interface IPoliticaCancelacion
{
    decimal CalcularPenalizacion(Cita cita, DateTime fechaCancelacion);
}
