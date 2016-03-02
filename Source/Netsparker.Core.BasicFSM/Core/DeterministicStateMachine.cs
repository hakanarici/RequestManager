using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netsparker.Core.BasicFSM
{
    public delegate StringBuilder StateMachineBufferGenerator();
    public delegate List<IState> StateMachineStateGenerator();
    public delegate List<ITransition> StateMachineTransitionGenerator();

    public class DeterministicStateMachine : IDeterministicStateMachine
    {
        #region Private Members

        private IState m_CurrentState;
        private StringBuilder m_Buffer;

        #endregion

        #region Constructors

        public DeterministicStateMachine(StateMachineBufferGenerator bufferGenerator, StateMachineStateGenerator initialStateGenerator, StateMachineTransitionGenerator initialTransitionGenerator)
        {
            m_Buffer = bufferGenerator();
            StateList = initialStateGenerator();
            TransitionList = initialTransitionGenerator();
        }

        #endregion
        
        #region Public Members

        public event StateChangingHandler OnStateChanging;

        public IState CurrentState
        {
            get { return m_CurrentState; }
        }

        public string Buffer
        {
            get { return m_Buffer.ToString(); }
        }

        public List<IState> StateList { get; }

        public List<ITransition> TransitionList { get; }

        #endregion

        #region Public Methods

        public void AddStates(params IState[] stateList)
        {
            StateList.AddRange(stateList);
            m_CurrentState = StateList.SingleOrDefault(s => s.Type == StateType.Initial);
        }

        public void AddTransition(ITransition transition)
        {
            ITransition existingTransition = TransitionList.FirstOrDefault(t => t.PreviousStateName == transition.PreviousStateName && t.Token == transition.Token);

            // If transition does not exist
            if (existingTransition == null)
                TransitionList.Add(transition);
            else
                throw new InvalidOperationException("There is already such a transition with the same (initial state, token) pair.");
        }

        public bool Run(char token)
        {
            ITransition transitionToRun = getTransition(token);

            if (transitionToRun != null)
            {
                if (OnStateChanging != null)
                    OnStateChanging(this, new StateChangingEventArgs { CurrentStateName = m_CurrentState.Name, Buffer = Buffer.ToString().Trim() });

                m_CurrentState = StateList.First(s => s.Name == transitionToRun.NextStateName);
                m_Buffer.Clear();
            }

            // The token that causes to the transition belongs to the next step of state machine
            // So add it to the buffer AFTER the transition occurs
            m_Buffer.Append(token);

            return m_CurrentState.Type != StateType.Terminal;
        }

        #endregion

        #region Private Methods

        private ITransition getTransition(char token)
        {
            // Find first occurance of either (state, token) pair 
            // or (state, ANY) pair for non-whitespace token input
            return TransitionList.FirstOrDefault(t =>
            t.PreviousStateName == m_CurrentState.Name &&
            (t.Token == token ||
            (t.Token == CommonTokens.ANY && !Char.IsWhiteSpace(token))));
        } 
        
        #endregion
    }
}
