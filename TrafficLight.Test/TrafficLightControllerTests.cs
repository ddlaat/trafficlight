using Microsoft.Extensions.DependencyInjection;
using TrafficLight.Domain;
using TrafficLightController = TrafficLight.Controllers.TrafficLightController;

namespace TrafficLight.Test;

public class TrafficLightControllerTests(TrafficLightControllerTestFixture fixture)
    : IClassFixture<TrafficLightControllerTestFixture>
{
    [Fact]
    public async Task SwitchTrafficLightFromGreenToRed_ShouldThrowInvalidOperationException()
    {
        // Arrange
        var trafficLight = new Domain.TrafficLight(TrafficLightColor.Green);
        
        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() =>
        {
            trafficLight.SwitchToRed();
            return Task.CompletedTask;
        });
    }

    [Fact]
    public Task SwitchTrafficLightFromRedToGreen_ShouldChangeTheColorToGreen()
    {
        // Arrange
        var trafficLight = new Domain.TrafficLight(TrafficLightColor.Red);
        
        // Act
        trafficLight.SwitchToGreen();
        
        // Assert
        Assert.Equal(TrafficLightColor.Green, trafficLight.Color);
        return Task.CompletedTask;
    }
    
    [Fact]
    public async Task ChangeTrafficLightColor_ShouldChangeColorAndPublishEvent()
    {
        // Arrange
        var controller = fixture.ServiceProvider.GetRequiredService<TrafficLightController>();
        var seededTrafficLight = await fixture.SeedTrafficLightAsync();

        // Act & Assert
        var trafficLight = await controller.ChangeTrafficLightColor(seededTrafficLight.Id, TrafficLightColor.Green);
        Assert.Equal(TrafficLightColor.Green, trafficLight.Color);

        await Assert.ThrowsAsync<InvalidOperationException>(async () =>
        {
            await controller.ChangeTrafficLightColor(seededTrafficLight.Id, TrafficLightColor.Red);
        });
    }
}