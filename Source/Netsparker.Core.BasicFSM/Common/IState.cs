
namespace Netsparker.Core.BasicFSM
{
    public interface IState
    {
        StateType Type { get; set; }
        string Name { get; set; }
    }
}
