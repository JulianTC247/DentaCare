using DentaCare.Refactored.Domain.Entities;

namespace DentaCare.Refactored.Application.Dtos;

public record AgendarCitaRequest(
    Paciente Paciente,
    Odontologo Odontologo,
    DateTime FechaHora,
    bool RequiereRadiografia);
