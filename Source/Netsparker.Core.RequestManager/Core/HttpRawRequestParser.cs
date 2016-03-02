using Netsparker.Core.BasicFSM;
using System;
using System.Collections.Generic;
using System.Text;

namespace Netsparker.Core.RequestManager
{
    public delegate StringBuilder RequestParserBufferGenerator();
    public delegate IWebRequestProxy WebRequestProxyGenerator(string uri);

    public class HttpRawRequestParser : IHttpRawRequestParser
    {
        #region Private Members

        private WebRequestProxyGenerator m_RequestGenerator;
        private IWebRequestProxy m_Request;
        private IDeterministicStateMachine m_StateMachine;

        private string m_MethodBuffer;
        private StringBuilder m_HeaderKeyBuffer;
        private StringBuilder m_HeaderValueBuffer;

        #endregion

        #region Constructors

        public HttpRawRequestParser(IDeterministicStateMachine stateMachine, RequestParserBufferGenerator keyBufferGenerator, RequestParserBufferGenerator valueBufferGenerator, WebRequestProxyGenerator webRequestProxyGenerator)
        {
            m_HeaderKeyBuffer = keyBufferGenerator();
            m_HeaderValueBuffer = valueBufferGenerator();
            m_StateMachine = stateMachine;
            m_RequestGenerator = webRequestProxyGenerator;

            initializeStateMachine();
        }

        #endregion

        #region Private Methods

        private void initializeStateMachine()
        {
            m_StateMachine.OnStateChanging += stateMachine_OnStateChanging;

            m_StateMachine.AddStates(
                new State { Name = HttpRawRequestStates.Method, Type = StateType.Initial },
                new State { Name = HttpRawRequestStates.URL },
                new State { Name = HttpRawRequestStates.Version },
                new State { Name = HttpRawRequestStates.HeaderKey },
                new State { Name = HttpRawRequestStates.HeaderValue },
                new State { Name = HttpRawRequestStates.HeaderEnd },
                new State { Name = HttpRawRequestStates.RequestBody },
                new State { Name = HttpRawRequestStates.Termination }
                );

            foreach (IState state in m_StateMachine.StateList)
            {
                // If not a dummy state (HeaderEnd is a inter state so it's dummy)
                if (state.Name != HttpRawRequestStates.HeaderEnd)
                    m_StateMachine.AddTransition(new Transition(state.Name, state.Name, CommonTokens.LOOPBACK)); // Add loopback transitions
            }

            // Method --- SPACE ---> URL
            m_StateMachine.AddTransition(new Transition(HttpRawRequestStates.Method, HttpRawRequestStates.URL, CommonTokens.SPACE));

            // URL --- SPACE ---> Version
            m_StateMachine.AddTransition(new Transition(HttpRawRequestStates.URL, HttpRawRequestStates.Version, CommonTokens.SPACE));

            // Version ---> NEWLINE ---> HeaderKey
            m_StateMachine.AddTransition(new Transition(HttpRawRequestStates.Version, HttpRawRequestStates.HeaderKey, CommonTokens.NEWLINE));

            // HeaderKey ---> SPACE ---> HeaderValue
            m_StateMachine.AddTransition(new Transition(HttpRawRequestStates.HeaderKey, HttpRawRequestStates.HeaderValue, CommonTokens.SPACE));

            // HeaderValue ---> NEWLINE ---> HeaderEnd
            m_StateMachine.AddTransition(new Transition(HttpRawRequestStates.HeaderValue, HttpRawRequestStates.HeaderEnd, CommonTokens.NEWLINE));

            // HeaderEnd ---> NEWLINE ---> RequestBody
            m_StateMachine.AddTransition(new Transition(HttpRawRequestStates.HeaderEnd, HttpRawRequestStates.RequestBody, CommonTokens.NEWLINE));

            // HeaderEnd ---> ANY ---> HeaderKey
            m_StateMachine.AddTransition(new Transition(HttpRawRequestStates.HeaderEnd, HttpRawRequestStates.HeaderKey, CommonTokens.ANY));

            // RequestBody ---> NEWLINE ---> Termination
            m_StateMachine.AddTransition(new Transition(HttpRawRequestStates.RequestBody, HttpRawRequestStates.Termination, CommonTokens.NEWLINE));

        }

        #region Event Handlers

        private void stateMachine_OnStateChanging(object sender, StateChangingEventArgs args)
        {
            switch (args.CurrentStateName)
            {
                case HttpRawRequestStates.Method:
                    m_MethodBuffer = args.Buffer; // WebRequestProxy object won't be created till Uri is read, so save method info into a buffer
                    break;
                case HttpRawRequestStates.URL:
                    m_Request = m_RequestGenerator(args.Buffer);
                    m_Request.SetMethod(m_MethodBuffer);
                    break;
                case HttpRawRequestStates.Version:
                    m_Request.SetVersion(args.Buffer);
                    break;
                case HttpRawRequestStates.HeaderKey:
                    m_HeaderKeyBuffer.Append(args.Buffer.Replace(":", string.Empty));
                    break;
                case HttpRawRequestStates.HeaderValue:
                    m_HeaderValueBuffer.Append(args.Buffer);
                    break;
                case HttpRawRequestStates.HeaderEnd:
                    m_Request.AddHeader(m_HeaderKeyBuffer.ToString(), m_HeaderValueBuffer.ToString());
                    m_HeaderKeyBuffer.Clear();
                    m_HeaderValueBuffer.Clear();
                    break;
                case HttpRawRequestStates.RequestBody:
                    m_Request.SetBody(args.Buffer);
                    break;
            }
        }

        #endregion

        #endregion

        #region Public Methods

        public IWebRequestProxy Parse(string rawRequest)
        {
            if (!String.IsNullOrEmpty(rawRequest) && rawRequest[rawRequest.Length - 1] != CommonTokens.NEWLINE)
                rawRequest += CommonTokens.NEWLINE;

            foreach (char token in rawRequest)
            {
                // If FSM is terminated
                if (!m_StateMachine.Run(token))
                    break;
            }

            return m_Request;
        } 
        
        #endregion
    }
}
