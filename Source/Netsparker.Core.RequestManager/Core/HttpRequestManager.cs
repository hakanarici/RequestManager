using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netsparker.Core.RequestManager
{
    // Composition root
    public class HttpRequestManager : IHttpRequestManager
    {
        private IHttpRequestBuilderProvider m_Provider;

        public HttpRequestManager()
        {
            m_Provider = new HttpRequestBuilderProvider();
        }

        public IHttpRequestBuilder GetBuilder()
        {
            return m_Provider.GetBuilder();
        }
    }
}
