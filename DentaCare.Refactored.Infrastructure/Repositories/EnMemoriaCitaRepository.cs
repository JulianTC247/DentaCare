using DentaCare.Refactored.Domain.Entities;
using DentaCare.Refactored.Domain.Interfaces;
using DentaCare.Refactored.Domain.ValueObjects;

namespace DentaCare.Refactored.Infrastructure.Repositories;

public class EnMemoriaCitaRepository : ICitaRepository
{
    private readonly List<Cita> _citas = new();
    private decimal _totalRecaudado = 0;

    public void Guardar(Cita cita)
    {
        _citas.Add(cita);
        _totalRecaudado += cita.CopagoCalculado;
    }

    public void Actualizar(Cita cita)
    {
        Cita? existente = _citas.FirstOrDefault(c => c.Id == cita.Id);
        if (existente is null)
        {
            return;
        }

        int index = _citas.IndexOf(existente);
        _citas[index] = cita;
    }

    public Cita? ObtenerPorId(string id)
    {
        return _citas.FirstOrDefault(c => c.Id == id);
    }

    public ResumenCitas ObtenerResumen()
    {
        return new ResumenCitas
        {
            TotalRecaudado = _totalRecaudado,
            TotalCanceladas = _citas.Count(c => c.Estado == "CANCELADA")
        };
    }
}
