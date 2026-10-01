namespace DentaCare.Refactored.Domain.Entities;

public class Odontologo
{
    public string Id { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public Enums.Especialidad Especialidad { get; set; }
    public bool EstaDisponible { get; set; }
}
