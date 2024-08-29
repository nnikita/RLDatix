
namespace RLDatix.Weather.Application.Configuration
{
    public interface IHttpConfiguration
    {
        string ApiKeyHeaderName { get; }
        string ApiKeyValue { get; }
        string ApiBaseUrl { get; }
        string Path { get; }
    }
}
