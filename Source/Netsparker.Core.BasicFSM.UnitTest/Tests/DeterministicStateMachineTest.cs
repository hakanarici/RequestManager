
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Netsparker.Core.BasicFSM.UnitTest
{
    [TestFixture]
    public class DeterministicStateMachineTest
    {
        private IDeterministicStateMachine m_StateMachine;

        [SetUp]
        public void Setup()
        {
            m_StateMachine = new DeterministicStateMachine(() => new StringBuilder(), () => new List<IState>(), () => new List<ITransition>());
        }

        [Test]
        public void RunBeforeInitializationTest()
        {
            // Throws NullReferenceException because there is no initial state
            Assert.Throws(typeof(NullReferenceException), () => m_StateMachine.Run(CommonTokens.ANY));
        }

        [Test]
        public void RunWithInitialStateTest()
        {
            m_StateMachine.AddStates(StateMock.GetMock("Test", StateType.Initial).Object);

            m_StateMachine.Run(CommonTokens.NEWLINE);
            
            StringAssert.AreEqualIgnoringCase(m_StateMachine.Buffer, CommonTokens.NEWLINE.ToString());
        }

        [Test]
        public void RunWithInitialStateAndLoopbackTransition()
        {
            IState stateMockObject = StateMock.GetMock("Test", StateType.Initial).Object;

            m_StateMachine.AddStates(stateMockObject);
            m_StateMachine.AddTransition(TransitionMock.GetLoopbackMock(stateMockObject.Name).Object);

            IState previousState = m_StateMachine.CurrentState;

            m_StateMachine.Run(CommonTokens.LOOPBACK);

            Assert.AreSame(previousState, m_StateMachine.CurrentState);
        }

        [Test]
        public void RunWithActualTransition()
        {
            IState initialStateMockObject = StateMock.GetMock("Initial", StateType.Initial).Object;
            IState nextStateMockObject = StateMock.GetMock("Next", StateType.Normal).Object;
            ITransition transitionMockObject = TransitionMock.GetMock(initialStateMockObject.Name, nextStateMockObject.Name, CommonTokens.SPACE).Object;
            m_StateMachine.AddStates(initialStateMockObject, nextStateMockObject);
            m_StateMachine.AddTransition(transitionMockObject);

            IState previousState = m_StateMachine.CurrentState;

            m_StateMachine.Run(CommonTokens.SPACE);

            Assert.AreNotSame(previousState, m_StateMachine.CurrentState);
            StringAssert.AreEqualIgnoringCase(m_StateMachine.CurrentState.Name, transitionMockObject.NextStateName);
        }

        [TearDown]
        public void TearDown()
        {
            m_StateMachine = null;
        }
    }
}
