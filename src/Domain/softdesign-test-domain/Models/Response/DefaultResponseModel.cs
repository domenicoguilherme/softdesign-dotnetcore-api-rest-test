namespace softdesign_test_domain.Models.Response
{
    public class DefaultResponseModel
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public DefaultResponseModel(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }
}
