namespace Weatherman.Api.Models
{
    /// <summary>
    /// Base class used by API responses
    /// </summary>
    public abstract class BaseResponse : BaseMessage
    {
        protected BaseResponse(Guid correlationId)
        {
            _correlationId = correlationId;
        }

        protected BaseResponse()
        {
        }
    }
}
