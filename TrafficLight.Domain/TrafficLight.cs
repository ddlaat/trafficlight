using TrafficLight.Domain.Events;
namespace TrafficLight.Domain;
public class TrafficLight
{
    public int Id { get; private set; }
    public TrafficLightColor Color { get; private set; } = TrafficLightColor.Red;

    public TrafficLight()
    {
        
    }

    public TrafficLight(TrafficLightColor initialColor)
    {
        Color = initialColor;
    }
    
    public TrafficLightColorChangedEvent[] SwitchToRed()
    {
        if (Color == TrafficLightColor.Green)
        {
            throw new InvalidOperationException("Cannot switch from green to orange.");
        }
        
        Color = TrafficLightColor.Red;

        return
        [
            new TrafficLightColorChangedEvent(Id, TrafficLightColor.Red)
        ];
    }
    
    public TrafficLightColorChangedEvent[] SwitchToGreen()
    {
        if (Color == TrafficLightColor.Orange)
        {
            throw new InvalidOperationException("Cannot switch from orange to green.");
        }
        
        Color = TrafficLightColor.Green;

        return
        [
            new TrafficLightColorChangedEvent(Id, TrafficLightColor.Green)
        ];
    }
}

public enum TrafficLightColor
{
    Red,
    Orange,
    Green
}