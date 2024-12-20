using MediatR;

namespace TrafficLight.Domain.Events;

public class TrafficLightColorChangedEventHandler : INotificationHandler<TrafficLightColorChangedEvent>
{
    public Task Handle(TrafficLightColorChangedEvent notification, CancellationToken cancellationToken)
    {
        // 1. Logging
        
        // 2. Geschiedenis bijhouden
        
        // 3. SMS versturen als het licht op groen springt
        
        // 4. Externe API aanroepen
        
        // 5. etc.
        
        System.Diagnostics.Debug.WriteLine($"Traffic light {notification.TrafficLightId} changed to {notification.NewColor}.");
        return Task.CompletedTask;
    }
}