
using System.Collections.Generic;

namespace Netsparker.Core.BasicFSM
{
    public interface IDeterministicStateMachine
    {
        event StateChangingHandler OnStateChanging;

        IState CurrentState { get; }
        string Buffer { get; }

        List<IState> StateList { get; }
        List<ITransition> TransitionList { get; }

        void AddStates(params IState[] stateList);
        void AddTransition(ITransition transition);
        bool Run(char token);
    }
}
