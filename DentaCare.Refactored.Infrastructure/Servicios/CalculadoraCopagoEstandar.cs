using DentaCare.Refactored.Domain.Entities;
using DentaCare.Refactored.Domain.Enums;
using DentaCare.Refactored.Domain.Interfaces;

namespace DentaCare.Refactored.Infrastructure.Servicios;

public class CalculadoraCopagoEstandar : ICalculadoraCopago
{
    private const decimal CostoBase = 100.0m;
    private const decimal CargoPrimeraVez = 20.0m;
    private const decimal CargoRadiografia = 35.0m;

    public decimal Calcular(Especialidad especialidad, TipoConvenio convenio, bool primeraVez, bool requiereRadiografia)
    {
        decimal copago = CostoBase * ObtenerMultiplicadorEspecialidad(especialidad);
        copago *= ObtenerFactorConvenio(convenio);

        if (primeraVez)
        {
            copago += CargoPrimeraVez;
        }

        if (requiereRadiografia)
        {
            copago += CargoRadiografia;
        }

        return copago;
    }

    private static decimal ObtenerMultiplicadorEspecialidad(Especialidad especialidad) =>
        especialidad switch
        {
            Especialidad.Ortodoncia => 1.2m,
            Especialidad.Endodoncia => 1.8m,
            Especialidad.Cirugia => 2.5m,
            Especialidad.Odontopediatria => 1.1m,
            _ => throw new ArgumentOutOfRangeException(nameof(especialidad), especialidad, "Especialidad no soportada.")
        };

    private static decimal ObtenerFactorConvenio(TipoConvenio convenio) =>
        convenio switch
        {
            TipoConvenio.Particular => 1.0m,
            TipoConvenio.EPS => 0.30m,
            TipoConvenio.Prepagada => 0.10m,
            _ => throw new ArgumentOutOfRangeException(nameof(convenio), convenio, "Convenio no soportado.")
        };
}
