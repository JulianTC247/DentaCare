namespace DentaCare.Refactored.Domain.Entities;

public class Cita
{
    public string Id { get; set; } = string.Empty;
    public Paciente Paciente { get; set; } = new();
    public Odontologo Odontologo { get; set; } = new();
    public DateTime FechaHora { get; set; }
    public decimal CopagoCalculado { get; set; }
    public string Estado { get; set; } = "PROGRAMADA";
    public decimal PenalizacionCancelacion { get; set; }

    public string GenerarTextoConfirmacion()
    {
        return $"CITA #{Id} - Paciente: {Paciente?.NombreCompleto}, Fecha: {FechaHora:yyyy-MM-dd HH:mm}, Copago: ${CopagoCalculado}";
    }
}
