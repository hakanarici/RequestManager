using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netsparker.Core.RequestManager.UnitTest
{
    public static class WebRequestProxyMock
    {
        public static Mock<IWebRequestProxy> GetMock(string uri)
        {
            var mock = new Mock<IWebRequestProxy>();

            mock.SetupAllProperties();
            mock.Setup(w => w.AddHeader(It.IsAny<string>(), It.IsAny<string>()));
            mock.Setup(w => w.SetBody(It.IsAny<string>()));
            mock.Setup(w => w.SetMethod(It.IsAny<string>()));
            mock.Setup(w => w.SetVersion(It.IsAny<string>()));

            return mock;
        }
    }
}
