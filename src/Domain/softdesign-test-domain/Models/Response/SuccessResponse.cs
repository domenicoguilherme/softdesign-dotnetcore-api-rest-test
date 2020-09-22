namespace softdesign_test_domain.Models.Response
{
    public class SuccessResponse<T> : DefaultResponseModel
    {
        public T Data { get; set; }

        public SuccessResponse() : base(true, string.Empty) { }

        public SuccessResponse(T data) : base(true, string.Empty)
        {
            Data = data;
        }

        public SuccessResponse(T data, string message) : base(true, message)
        {
            Data = data;
        }
    }
}
