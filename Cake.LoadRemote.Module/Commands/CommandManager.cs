namespace Cake.LoadRemote.Module.Commands
{
    using System.Collections.Generic;
    using Abstraction;

    /// <summary>
    /// The command manager.
    /// </summary>
    internal class CommandManager : ICommandManager
    {
        /// <summary>
        /// The instance.
        /// </summary>
        private static ICommandManager instance;

        /// <summary>
        /// The commands.
        /// </summary>
        private readonly List<ICommand> commands = new List<ICommand>();
        
        /// <summary>
        /// Gets the instance.
        /// </summary>
        public static ICommandManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CommandManager();
                }

                return instance;
            }
        }

        /// <summary>
        /// Adds a command.
        /// </summary>
        /// <param name="command">
        /// The command.
        /// </param>
        public void AddCommand(ICommand command)
        {
            if (b)
            {
                
            }

            commands.Add(command);
        }

        /// <summary>
        /// Run all commands.
        /// </summary>
        public void RunAll()
        {
            foreach (var command in commands)
            {
                command.Run();
            }
        }
    }
}
