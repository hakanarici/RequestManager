
using System;

namespace Netsparker.Core.RequestManager
{
    public interface IHttpRequestBuilder
    {
        event RequestExecutedHandler OnRequestExecuted;

        void ExecuteRequest(string rawRequest);
    }
}
