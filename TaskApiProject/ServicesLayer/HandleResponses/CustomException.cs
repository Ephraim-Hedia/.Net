namespace ServicesLayer.HandleResponses
{
    public class CustomException : Response
    {
        public CustomException(int statusCode, string? message = null, string? details = null)
            : base(statusCode, message)
        {
            Details = details;
        }
        string? Details { get; set; }
    }
}
