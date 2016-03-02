using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netsparker.Core.RequestManager
{
    public interface IHttpRequestBuilderProvider
    {
        IHttpRequestBuilder GetBuilder();
    }
}
