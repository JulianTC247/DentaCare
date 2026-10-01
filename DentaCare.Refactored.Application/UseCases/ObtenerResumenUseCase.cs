using DentaCare.Refactored.Domain.Interfaces;
using DentaCare.Refactored.Domain.ValueObjects;

namespace DentaCare.Refactored.Application.UseCases;

public class ObtenerResumenUseCase
{
    private readonly ICitaRepository _repository;

    public ObtenerResumenUseCase(ICitaRepository repository)
    {
        _repository = repository;
    }

    public ResumenCitas Ejecutar()
    {
        return _repository.ObtenerResumen();
    }
}
