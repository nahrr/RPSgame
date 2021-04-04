using System.Net;

namespace RPScygni.Models
{
    public class Errors
    {
        public HttpStatusCode ErrorCode { get; set; }
        public string ErrorDesc { get; set; }
    }
}
