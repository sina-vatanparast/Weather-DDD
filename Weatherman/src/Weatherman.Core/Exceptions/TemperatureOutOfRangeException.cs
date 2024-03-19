namespace Weatherman.Core.Exceptions
{
    public class TemperatureOutOfRangeException : BaseException
    {
        public TemperatureOutOfRangeException(string message) : base(message)
        {
        }

        public TemperatureOutOfRangeException(int temperature) : base($"Temperature {temperature} is out of range(-60,60)")
        {
        }
    }
}
