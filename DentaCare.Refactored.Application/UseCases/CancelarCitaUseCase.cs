using DentaCare.Refactored.Application.Dtos;
using DentaCare.Refactored.Domain.Entities;
using DentaCare.Refactored.Domain.Interfaces;

namespace DentaCare.Refactored.Application.UseCases;

public class CancelarCitaUseCase
{
    private readonly ICitaRepository _repository;
    private readonly IPoliticaCancelacion _politica;
    private readonly INotificador _notificador;

    public CancelarCitaUseCase(ICitaRepository repository, IPoliticaCancelacion politica, INotificador notificador)
    {
        _repository = repository;
        _politica = politica;
        _notificador = notificador;
    }

    public CancelarCitaResponse Ejecutar(CancelarCitaRequest request)
    {
        Cita cita = _repository.ObtenerPorId(request.CitaId)
            ?? throw new InvalidOperationException("Cita no encontrada.");

        decimal penalizacion = _politica.CalcularPenalizacion(cita, request.FechaHoraCancelacion);

        cita.Estado = "CANCELADA";
        cita.PenalizacionCancelacion = penalizacion;

        _repository.Actualizar(cita);
        _notificador.Notificar(cita, $"Su cita #{cita.Id} ha sido CANCELADA.");

        return new CancelarCitaResponse(cita.Id, cita.Estado, penalizacion);
    }
}
