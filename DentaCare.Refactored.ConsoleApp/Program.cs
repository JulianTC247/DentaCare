using DentaCare.Refactored.Application.Dtos;
using DentaCare.Refactored.Application.UseCases;
using DentaCare.Refactored.Domain.Entities;
using DentaCare.Refactored.Domain.Enums;
using DentaCare.Refactored.Domain.Interfaces;
using DentaCare.Refactored.Domain.ValueObjects;
using DentaCare.Refactored.Infrastructure.Notificadores;
using DentaCare.Refactored.Infrastructure.Repositories;
using DentaCare.Refactored.Infrastructure.Servicios;

Console.WriteLine("=================================================");
Console.WriteLine(" DENTACARE SYSTEM - MÓDULO REFACTORIZADO");
Console.WriteLine("=================================================\n");

try
{
    ICitaRepository repository = new EnMemoriaCitaRepository();
    ICalculadoraCopago calculadora = new CalculadoraCopagoEstandar();
    IPoliticaCancelacion politica = new PoliticaCancelacionEstandar();
    INotificador notificador = new NotificadorCompuesto(
        new NotificadorEmail(),
        new NotificadorSms());

    AgendarCitaUseCase agendarUseCase = new AgendarCitaUseCase(repository, calculadora, notificador);
    CancelarCitaUseCase cancelarUseCase = new CancelarCitaUseCase(repository, politica, notificador);
    ObtenerResumenUseCase resumenUseCase = new ObtenerResumenUseCase(repository);

    Paciente paciente = new Paciente
    {
        Id = "PAC-101",
        NombreCompleto = "Ana María Gómez",
        Correo = "ana.gomez@email.com",
        Celular = "3001234567",
        TipoConvenio = TipoConvenio.EPS,
        EsPrimeraVez = true
    };

    Odontologo odontologo = new Odontologo
    {
        Id = "ODO-202",
        Nombre = "Dr. Roberto Martínez",
        Especialidad = Especialidad.Cirugia,
        EstaDisponible = true
    };

    Console.WriteLine("---> [FLUJO 1]: AGENDAMIENTO DE CITA ODONTOLÓGICA");
    DateTime fechaCita = DateTime.Now.AddHours(12);

    AgendarCitaRequest requestAgendar = new AgendarCitaRequest(paciente, odontologo, fechaCita, RequiereRadiografia: true);
    AgendarCitaResponse respuestaAgendar = agendarUseCase.Ejecutar(requestAgendar);

    Console.WriteLine($"\n Cita generada exitosamente con ID: {respuestaAgendar.CitaId}");
    Console.WriteLine($" Copago Final Calculado: ${respuestaAgendar.Copago:N2}");
    Console.WriteLine($" Estado de la Cita: {respuestaAgendar.Estado}\n");

    Console.WriteLine("-------------------------------------------------");
    Console.WriteLine("---> [FLUJO 2]: CANCELACIÓN DE CITA (MENOS DE 24 HORAS)");

    CancelarCitaRequest requestCancelar = new CancelarCitaRequest(respuestaAgendar.CitaId, DateTime.Now);
    CancelarCitaResponse respuestaCancelar = cancelarUseCase.Ejecutar(requestCancelar);

    Console.WriteLine($"\n Cita ID #{respuestaCancelar.CitaId} ha sido actualizada.");
    Console.WriteLine($" Nuevo Estado: {respuestaCancelar.Estado}");
    Console.WriteLine($" Penalización Aplicada: ${respuestaCancelar.Penalizacion:N2}\n");

    Console.WriteLine("-------------------------------------------------");
    Console.WriteLine("---> [FLUJO 3]: REPORTE DE TOTALES ACUMULADOS EN MEMORIA");
    ResumenCitas resumen = resumenUseCase.Ejecutar();
    Console.WriteLine($" Total Recaudado en Copagos: ${resumen.TotalRecaudado:N2}");
    Console.WriteLine($" Total Citas Canceladas: {resumen.TotalCanceladas}");
}
catch (Exception ex)
{
    Console.WriteLine($"\n ERROR CRÍTICO EN EL SISTEMA: {ex.Message}");
}

Console.WriteLine("\n=================================================");
Console.WriteLine("Presione Enter para salir...");
Console.ReadLine();
