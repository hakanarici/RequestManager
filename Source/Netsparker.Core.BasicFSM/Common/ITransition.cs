
namespace Netsparker.Core.BasicFSM
{
    public interface ITransition
    {
        string PreviousStateName { get; set; }
        string NextStateName { get; set; }
        char Token { get; set; }
    }
}
