using Common.Contracts;
using Common.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Common;

public static class Setup
{
    public static void AddServices(this IServiceCollection services)
    {
        // Transient ist das Standardszenario: Beim Aufloesen der Abhaengigkeit wird IMMER eine neue Instanz erzeugt
        services.AddTransient<ITransportService, TransportService>();

        // Bei Scoped wird jedes mal eine neue Instanz innerhalb eines Scopes erzeugt (bei ASP.Net == Request)
        services.AddScoped<IVehicleService, VehicleService>();

        // Eine Instanz wird nur einmal erzeugt fuer den gesamten Lebenszyklus der Anwendung
        // Kann sehr problematisch werden, z. B. DB-Zugriff oder Dateizugriff etc. (Concurrency-Probleme, vgl. M006)
        services.AddSingleton<AppSettings>();
    }
}
