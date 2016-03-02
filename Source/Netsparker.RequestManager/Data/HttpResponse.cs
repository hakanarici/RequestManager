using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netsparker.RequestManager
{
    public class HttpResponse
    {
        public int Result { get; set; }
        public string Status { get; set; }
        public string Host { get; set; }
        public string URL { get; set; }
        public string Headers { get; set; }
        public string Body { get; set; }
        public int BodyLength { get; set; }
    }
}
