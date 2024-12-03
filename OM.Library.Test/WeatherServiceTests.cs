using System.Net;
using Moq;
using Moq.Protected;
using OM.Domain;

namespace OM.Library.Test;

[TestClass]
public class WeatherServiceTests
{
    [TestMethod]
    public async Task GetForecastAsync_Should_Work()
    {
        // Arrange
        Mock<HttpMessageHandler> handlerMock = new();

        var setup = BuildSetup(handlerMock: handlerMock);

        setup.ReturnsAsync(new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent("{ \"latitude\": 51.7, \"longitude\": 6.1 }")
        });

        var service = new WeatherService(httpClient: new(handlerMock.Object) { BaseAddress = new Uri("https://fake-weather-test.fake") });

        // Act
        WeatherData data = await service.GetForecastAsync();

        // Assert
        Assert.AreEqual(expected: 51.7, actual: data.Latitude);
        Assert.AreEqual(expected: 6.1, actual: data.Longitude);
    }

    [TestMethod]
    public async Task GetHistoryWeatherAsync_Should_Work()
    {
        // Arrange
        Mock<HttpMessageHandler> handlerMock = new();

        var setup = BuildSetup(handlerMock: handlerMock);

        setup.ReturnsAsync(new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent("{ \"latitude\": 51.7, \"longitude\": 6.1 }")
        });

        var service = new WeatherService(httpClient: new(handlerMock.Object) { BaseAddress = new Uri("https://fake-weather-test.fake") });
        var day = new DateOnly(year: 2024, month: 11, day: 30);

        // Act
        WeatherData data = await service.GetHistoryWeatherAsync(day: day);

        // Assert
        Assert.AreEqual(expected: 51.7, actual: data.Latitude);
        Assert.AreEqual(expected: 6.1, actual: data.Longitude);
    }

    [TestMethod]
    public async Task GetHistoryWeatherAsync_Should_Throw_Exception_When_Status_Code_Is_Not_Successful()
    {
        // Arrange
        Mock<HttpMessageHandler> handlerMock = new();

        var setup = BuildSetup(handlerMock: handlerMock);

        setup.ReturnsAsync(new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.NotFound,
            Content = new StringContent("")
        });

        var service = new WeatherService(httpClient: new(handlerMock.Object) { BaseAddress = new Uri("https://fake-weather-test.fake") });
        // var service = new WeatherService();
        var day = new DateOnly(year: 2024, month: 11, day: 30);

        // Act
        var exception = await Assert.ThrowsExceptionAsync<WeatherException>(async () => await service.GetHistoryWeatherAsync(day: day));

        // Assert
        Assert.AreEqual(expected: "The request has returned with the status code of NotFound", actual: exception.Message);
    }

    [TestMethod]
    public async Task GetHistoryWeatherAsync_Should_Throw_Exception_When_Deserialization_Is_Unsuccessful()
    {
        // Arrange
        Mock<HttpMessageHandler> handlerMock = new();

        var setup = BuildSetup(handlerMock: handlerMock);

        setup.ReturnsAsync(new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent("{ \"latitude\" 51.7, \"longitude\": 6.1 }")
        });

        var service = new WeatherService(httpClient: new(handlerMock.Object) { BaseAddress = new Uri("https://fake-weather-test.fake") });
        // var service = new WeatherService();
        var day = new DateOnly(year: 2024, month: 11, day: 30);

        // Act
        var exception = await Assert.ThrowsExceptionAsync<WeatherException>(async () => await service.GetHistoryWeatherAsync(day: day));

        // Assert
        Assert.AreEqual(expected: "The deserialization was unsuccessful.", actual: exception.Message);
    }

    private Moq.Language.Flow.ISetup<HttpMessageHandler, Task<HttpResponseMessage>> BuildSetup(Mock<HttpMessageHandler> handlerMock)
    {
        return handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                methodOrPropertyName: "SendAsync", 
                ItExpr.Is<HttpRequestMessage>(request => request.RequestUri != null && !string.IsNullOrEmpty(request.RequestUri.ToString())), 
                ItExpr.IsAny<CancellationToken>()
            );
    }
}