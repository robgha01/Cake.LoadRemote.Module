namespace Cake.LoadRemote.Module.Abstraction
{
    using Commands;

    /// <summary>
    /// The Command interface.
    /// </summary>
    internal interface ICommand
    {
        /// <summary>
        /// Gets the mode.
        /// </summary>
        CommandMode Mode { get; }
        
        /// <summary>
        /// Defines the work of a command.
        /// </summary>
        void Run();

        /// <summary>
        /// Determind if argument <paramref name="name"/> belongs to this command.
        /// </summary>
        /// <param name="name">
        /// The argument name.
        /// </param>
        /// <returns>
        /// Returns <c>True</c> if this argument belongs to this <see cref="ICommand"/> otherwise <c>False</c>
        /// </returns>
        bool IsArgumentEquals(string name);
    }
}
