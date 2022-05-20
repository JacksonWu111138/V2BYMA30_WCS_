namespace Mirle.STKC.R46YP320.Simulator.CraneCommand
{
    internal interface ICommand
    {
        CommandInfo Info { get; }

        void Execute();
    }
}