
namespace Netsparker.Core.BasicFSM
{
    public class State : IState
    {
        public string Name { get; set; }

        public StateType Type { get; set; }
    }
}
