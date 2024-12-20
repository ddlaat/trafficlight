using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TrafficLight.Controllers;
using TrafficLight.Domain.Events;
using TrafficLight.Infrastructure;

namespace TrafficLight.Test;

public class TrafficLightControllerTestFixture : IDisposable
{
    public ServiceProvider ServiceProvider { get; }

    public TrafficLightControllerTestFixture()
    {
        var services = new ServiceCollection();
        services.AddDbContext<TrafficLightContext>(options =>
            options.UseInMemoryDatabase("TestTrafficLightDB"));

        services.AddMediatR(typeof(TrafficLightColorChangedEventHandler).Assembly);
        services.AddScoped<TrafficLightController>();
        ServiceProvider = services.BuildServiceProvider();
    }

    public async Task<Domain.TrafficLight> SeedTrafficLightAsync()
    {
        using var scope = ServiceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TrafficLightContext>();

        var trafficLight = new Domain.TrafficLight();
        dbContext.TrafficLights.Add(trafficLight);
        await dbContext.SaveChangesAsync();

        return trafficLight;
    }

    public void Dispose()
    {
        if (ServiceProvider is IDisposable disposable)
        {
            disposable.Dispose();
        }
    }
}
