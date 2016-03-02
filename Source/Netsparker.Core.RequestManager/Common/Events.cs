
using System;

namespace Netsparker.Core.RequestManager
{
    public delegate void RequestExecutedHandler(object sender, RequestExecutedEventArgs args);

    public class RequestExecutedEventArgs : EventArgs
    {
        public int StatusCode { get; set; }
        public string Status { get; set; }
        public string Host { get; set; }
        public string URL { get; set; }
        public string Body { get; set; }
        public string Headers { get; set; }
    }
}
