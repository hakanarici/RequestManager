using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Netsparker.Core.BasicFSM;
using Moq;

namespace Netsparker.Core.RequestManager.UnitTest
{
    [TestFixture]
    public class HttpRawRequestParserTest
    {
        private IHttpRawRequestParser m_Parser;
        private Mock<IDeterministicStateMachine> m_StateMachineMock;

        [SetUp]
        public void Setup()
        {
            m_StateMachineMock = DeterministicStateMachineMock.GetMock();
            
            m_Parser = new HttpRawRequestParser(m_StateMachineMock.Object, () => new StringBuilder(), () => new StringBuilder(), uri => WebRequestProxyMock.GetMock(uri).Object);
        }

        [Test]
        public void ParseWithEmptyStringTest()
        {
            IWebRequestProxy requestProxy = m_Parser.Parse(String.Empty);

            m_StateMachineMock.Verify(m => m.Run(It.IsAny<char>()), Times.Never());
            Assert.IsNull(requestProxy);
        }

        [Test]
        public void ParseWithNonNewLineTerminatingInputTest()
        {
            string input = @"GET http://google.com/ HTTP/1.1";

            IWebRequestProxy requestProxy = m_Parser.Parse(input);

            // Will fail due to new line addition by parser
            m_StateMachineMock.Verify(m => m.Run(It.IsAny<char>()), Times.Exactly(input.Length));
        }

        [Test]
        public void ParseWithNewLineTerminatingInputTest()
        {
            string input = "GET http://google.com/ HTTP/1.1\n";

            IWebRequestProxy requestProxy = m_Parser.Parse(input);

            m_StateMachineMock.Verify(m => m.Run(It.IsAny<char>()), Times.Exactly(input.Length));
        }
    }
}
