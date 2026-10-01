using DentaCare.Refactored.Domain.Entities;
using DentaCare.Refactored.Domain.Enums;

namespace DentaCare.Refactored.Domain.Interfaces;

public interface ICalculadoraCopago
{
    decimal Calcular(Especialidad especialidad, TipoConvenio convenio, bool primeraVez, bool requiereRadiografia);
}
