using MediatR;

namespace TrafficLight.Domain.Events;

public class TrafficLightColorChangedEvent(int trafficLightId, TrafficLightColor newColor) : INotification
{
    public int TrafficLightId { get; } = trafficLightId;
    public TrafficLightColor NewColor { get; } = newColor;
}