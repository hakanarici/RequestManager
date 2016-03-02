
namespace Netsparker.Core.RequestManager
{
    public interface IHttpRawRequestParser
    {
        IWebRequestProxy Parse(string rawRequest);
    }
}
