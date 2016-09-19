namespace Cake.LoadRemote.Module.Abstraction
{
    internal interface ICommandManager
    {
        void AddCommand(ICommand command);

        void RunAll();
    }
}
