using System.Net;

namespace RPScygni.Models
{
    public class Error
    {
        public HttpStatusCode ErrorCode { get; set; }
        public string ErrorDesc { get; set; }
    }
}
