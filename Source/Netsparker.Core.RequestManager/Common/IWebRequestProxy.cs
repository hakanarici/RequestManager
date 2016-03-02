
namespace Netsparker.Core.RequestManager
{
    public interface IWebRequestProxy
    {
        void SetMethod(string method);
        void SetVersion(string version);
        void AddHeader(string key, string value);
        void SetBody(string body);
    }
}
