using DentaCare.Refactored.Application.Dtos;
using DentaCare.Refactored.Domain.Entities;
using DentaCare.Refactored.Domain.Interfaces;

namespace DentaCare.Refactored.Application.UseCases;

public class AgendarCitaUseCase
{
    private readonly ICitaRepository _repository;
    private readonly ICalculadoraCopago _calculadora;
    private readonly INotificador _notificador;

    public AgendarCitaUseCase(ICitaRepository repository, ICalculadoraCopago calculadora, INotificador notificador)
    {
        _repository = repository;
        _calculadora = calculadora;
        _notificador = notificador;
    }

    public AgendarCitaResponse Ejecutar(AgendarCitaRequest request)
    {
        if (!request.Odontologo.EstaDisponible)
        {
            throw new InvalidOperationException("El odontólogo no tiene disponibilidad en el horario seleccionado.");
        }

        decimal copago = _calculadora.Calcular(
            request.Odontologo.Especialidad,
            request.Paciente.TipoConvenio,
            request.Paciente.EsPrimeraVez,
            request.RequiereRadiografia);

        Cita cita = new Cita
        {
            Id = Guid.NewGuid().ToString()[..8],
            Paciente = request.Paciente,
            Odontologo = request.Odontologo,
            FechaHora = request.FechaHora,
            CopagoCalculado = copago,
            Estado = "PROGRAMADA"
        };

        _repository.Guardar(cita);
        _notificador.Notificar(cita, cita.GenerarTextoConfirmacion());

        return new AgendarCitaResponse(cita.Id, copago, cita.Estado);
    }
}
