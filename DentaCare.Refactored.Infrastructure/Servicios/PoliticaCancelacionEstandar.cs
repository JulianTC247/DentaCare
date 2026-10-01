using DentaCare.Refactored.Domain.Entities;
using DentaCare.Refactored.Domain.Enums;
using DentaCare.Refactored.Domain.Interfaces;

namespace DentaCare.Refactored.Infrastructure.Servicios;

public class PoliticaCancelacionEstandar : IPoliticaCancelacion
{
    private const decimal PenalizacionBase = 50.0m;
    private const decimal CargoCirugia = 40.0m;
    private const int HorasMinimasSinPenalizacion = 24;

    public decimal CalcularPenalizacion(Cita cita, DateTime fechaCancelacion)
    {
        double horasRestantes = (cita.FechaHora - fechaCancelacion).TotalHours;

        if (horasRestantes >= HorasMinimasSinPenalizacion)
        {
            return 0;
        }

        decimal penalizacion = PenalizacionBase;

        if (cita.Odontologo.Especialidad == Especialidad.Cirugia)
        {
            penalizacion += CargoCirugia;
        }

        return penalizacion;
    }
}
