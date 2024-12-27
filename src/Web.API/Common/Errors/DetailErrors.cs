using System.Net;

namespace Web.API.Common.Errors
{
        public class ErrorDetails
        {
            public string Title { get; set; }
            public string Type { get; set; }
            public string Detail { get; set; }
            public HttpStatusCode StatusCode { get; set; }
            public string? ExtensionName { get; set; }
            public ErrorDetails(HttpStatusCode statusCode ,string title, string type, string detail, string extensionName = null)
            {
                StatusCode = statusCode;
                Title = title;
                Type = type;
                Detail = detail;
                ExtensionName = extensionName;
            }

        public static readonly Dictionary<string, ErrorDetails> list = new()
        {
            {  "HeaderError", new ErrorDetails(HttpStatusCode.BadRequest,"BadRequest","HeaderValidation","Error Headers Invalid", "Errors")  },
            {  "InternalError", new ErrorDetails(HttpStatusCode.InternalServerError,"Server Error","Server Error","An Internal error")  }
        };

    }

}
