using System;

namespace Netsparker.Core.BasicFSM
{
    public class Transition : ITransition
    {
        public Transition(string previousStateName, string nextStateName, char token)
        {
            PreviousStateName = previousStateName;
            NextStateName = nextStateName;
            Token = token;
        }

        public string NextStateName { get; set; }

        public string PreviousStateName { get; set; }

        public char Token { get; set; }
    }
}
