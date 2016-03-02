using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netsparker.Core.BasicFSM.UnitTest
{
    public static class TransitionMock
    {
        public static Mock<ITransition> GetLoopbackMock(string stateName)
        {
            return TransitionMock.GetMock(stateName, stateName, CommonTokens.LOOPBACK);
        }

        public static Mock<ITransition> GetMock(string previousStateName, string nextStateName, char token)
        {
            var mock = new Mock<ITransition>();

            mock.SetupProperty(t => t.PreviousStateName, previousStateName);
            mock.SetupProperty(t => t.NextStateName, nextStateName);
            mock.SetupProperty(t => t.Token, token);

            return mock;
        }
    }
}
