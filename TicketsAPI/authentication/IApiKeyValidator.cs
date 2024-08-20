namespace TicketsAPI.authentication
{
    public interface IApiKeyValidator
    {
        bool IsValid(string apiKey);
    }
}
