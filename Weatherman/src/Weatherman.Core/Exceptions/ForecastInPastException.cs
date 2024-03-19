namespace Weatherman.Core.Exceptions
{
    public class ForecastInPastException : BaseException
    {
        public ForecastInPastException(string message) : base(message)
        {
        }

        public ForecastInPastException(DateOnly date) : base($"Forecast Input cannot be in the past.{date} < today!")
        {
        }
    }
}
