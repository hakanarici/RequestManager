using Microsoft.Practices.Unity;
using Netsparker.Core.BasicFSM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netsparker.Core.RequestManager
{
    public class HttpRequestBuilderProvider : IHttpRequestBuilderProvider
    {
        private readonly IUnityContainer m_UnityContainer;

        public HttpRequestBuilderProvider()
        {
            m_UnityContainer = new UnityContainer();

            // Singleton container
            m_UnityContainer.RegisterInstance<IUnityContainer>(m_UnityContainer);

            // Register State Machine
            m_UnityContainer.RegisterInstance<StateMachineBufferGenerator>(() => new StringBuilder());
            m_UnityContainer.RegisterInstance<StateMachineStateGenerator>(() => new List<IState>());
            m_UnityContainer.RegisterInstance<StateMachineTransitionGenerator>(() => new List<ITransition>());
            m_UnityContainer.RegisterType<IDeterministicStateMachine, DeterministicStateMachine>();

            // Register Request Parser
            m_UnityContainer.RegisterInstance<RequestParserBufferGenerator>(() => new StringBuilder());
            m_UnityContainer.RegisterInstance<WebRequestProxyGenerator>(uri => new WebRequestProxy(uri));
            m_UnityContainer.RegisterType<IHttpRawRequestParser, HttpRawRequestParser>();

            // Register Request Builder
            m_UnityContainer.RegisterType<IHttpRequestBuilder, HttpRequestBuilder>();
        }

        public IHttpRequestBuilder GetBuilder()
        {
            return m_UnityContainer.Resolve<IHttpRequestBuilder>();
        }
    }
}
