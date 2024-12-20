using MediatR;
using TrafficLight.Domain;
using TrafficLight.Domain.Events;
using TrafficLight.Infrastructure;

namespace TrafficLight.Controllers;

public class TrafficLightController(TrafficLightContext context, IMediator mediator)
{
    public async Task<TrafficLight.Domain.TrafficLight> ChangeTrafficLightColor(int id, TrafficLightColor newColor)
    {
        var trafficLight = await context.TrafficLights.FindAsync(id);

        if (trafficLight == null)
            throw new Exception("Traffic light not found");

        var events = newColor switch
        {
            TrafficLightColor.Red => trafficLight.SwitchToRed(),
            TrafficLightColor.Green => trafficLight.SwitchToGreen(),
            _ => []
        };

        await context.SaveChangesAsync();
        
        // Publish all domain events (this could be done automatically with a middleware in a real-world scenario)
        foreach (var domainEvent in events)
        {
            await mediator.Publish(domainEvent);
        }

        return trafficLight;
    }
}
