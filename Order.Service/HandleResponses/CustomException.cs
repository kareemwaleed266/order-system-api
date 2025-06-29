namespace Order.Service.HandleResponses
{
    public class CustomException : Responses
    {
        public CustomException(int statusCode, string? message = null, string? details = null)
            : base(statusCode, message)
        {
            Details = details;
        }

        public string? Details { get; set; }
    }
}
