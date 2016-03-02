using Moq;
using Netsparker.Core.BasicFSM;
using Netsparker.Core.BasicFSM.UnitTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netsparker.Core.RequestManager.UnitTest
{
    public static class DeterministicStateMachineMock
    {
        public static Mock<IDeterministicStateMachine> GetMock()
        {
            var mock = new Mock<IDeterministicStateMachine>();

            IState initialStateMockObject = StateMock.GetMock("Initial", StateType.Initial).Object;
            IState nextStateMockObject = StateMock.GetMock("Test", StateType.Normal).Object;
            ITransition singleTransitionMockObject = TransitionMock.GetMock(initialStateMockObject.Name, nextStateMockObject.Name, CommonTokens.SPACE).Object;

            mock.SetupGet(m => m.StateList).Returns(new List<IState> { initialStateMockObject, nextStateMockObject });
            mock.SetupGet(m => m.TransitionList).Returns(new List<ITransition> { singleTransitionMockObject });
            mock.SetupGet(m => m.CurrentState).Returns(initialStateMockObject);

            mock.Setup(m => m.AddStates(It.IsAny<IState>()));
            mock.Setup(m => m.AddStates(It.IsAny<IState[]>()));
            mock.Setup(m => m.AddTransition(It.IsAny<ITransition>()));

            mock.Setup(m => m.Run(It.IsAny<char>())).Returns(true);
            
                        
            return mock;
        }
    }
}
