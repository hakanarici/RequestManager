using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netsparker.Core.BasicFSM.UnitTest
{
    public static class StateMock
    {
        public static Mock<IState> GetMock(string defaultName, StateType defaultState)
        {
            var mock = new Mock<IState>();

            mock.SetupProperty(state => state.Name, defaultName);
            mock.SetupProperty(state => state.Type, defaultState);

            return mock;
        }
    }
}
