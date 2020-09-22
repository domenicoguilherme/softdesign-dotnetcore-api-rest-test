using System;

namespace softdesign_test_domain.Models.Response
{
    public class ErrorResponseModel : DefaultResponseModel
    {
        public ErrorResponseModel(Exception ex) : this(ExceptionMessage(ex)) { }

        public ErrorResponseModel(string message) : base(false, message) { }

        private static string ExceptionMessage(Exception ex)
        {
            return ex?.InnerException?.Message ?? ex.Message ?? "An exception was thrown by application";
        }
    }
}