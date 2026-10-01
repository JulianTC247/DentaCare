namespace DentaCare.Refactored.Domain.Entities;

public class Paciente
{
    public string Id { get; set; } = string.Empty;
    public string NombreCompleto { get; set; } = string.Empty;
    public string Correo { get; set; } = string.Empty;
    public string Celular { get; set; } = string.Empty;
    public Enums.TipoConvenio TipoConvenio { get; set; }
    public bool EsPrimeraVez { get; set; }
}
