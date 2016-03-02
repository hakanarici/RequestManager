
using System;

namespace Netsparker.Core.BasicFSM
{
    public delegate void StateChangingHandler(object sender, StateChangingEventArgs args);

    public class StateChangingEventArgs : EventArgs
    {
        public string CurrentStateName { get; set; }
        public string Buffer { get; set; }
    }
}
