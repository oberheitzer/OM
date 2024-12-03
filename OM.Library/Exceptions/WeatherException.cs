namespace OM.Library;

public class WeatherException : Exception
{
    public WeatherException() {}
    
    public WeatherException(string message) 
        : base(message) {}

    public WeatherException(string message, Exception exception) 
        : base(message, exception) {}
}
