namespace Cake.LoadRemote.Module.Commands
{
    using System.Collections.Generic;

    using Abstraction;
    using Core;
    using Core.Diagnostics;

    /// <summary>
    /// The command base.
    /// </summary>
    public abstract class CommandBase : ICommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandBase"/> class. 
        /// </summary>
        /// <param name="cakeContext">The <see cref="ICakeContext"/>.</param>
        /// <param name="environment">The <see cref="ICakeEnvironment"/>.</param>
        /// <param name="log">The <see cref="ICakeLog"/>.</param>
        protected CommandBase(ICakeContext cakeContext, ICakeEnvironment environment, ICakeLog log)
        {
            CakeContext = cakeContext;
            Environment = environment;
            Log = log;
        }

        /// <summary>
        /// Gets the mode.
        /// </summary>
        public abstract CommandMode Mode { get; }

        /// <summary>
        /// Gets the <see cref="ICakeContext"/>.
        /// </summary>
        protected ICakeContext CakeContext { get; private set; }

        /// <summary>
        /// Gets the <see cref="ICakeEnvironment"/>.
        /// </summary>
        protected ICakeEnvironment Environment { get; private set; }

        /// <summary>
        /// Gets the <see cref="ICakeLog"/>
        /// </summary>
        protected ICakeLog Log { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether this command has executed.
        /// </summary>
        private bool Executed { get; set; }
        
        /// <summary>
        /// Runs this command.
        /// </summary>
        public void Run()
        {
            if (Mode == CommandMode.RunOnce && Executed)
            {
                return;
            }

            Executed = true;
            DoRun();
        }

        /// <summary>
        /// Determind if argument <paramref name="name"/> belongs to this command.
        /// </summary>
        /// <param name="name">
        /// The argument name.
        /// </param>
        /// <returns>
        /// Returns <c>True</c> if this argument belongs to this <see cref="ICommand"/> otherwise <c>False</c>
        /// </returns>
        public abstract bool IsArgumentEquals(string name);

        /// <summary>
        /// Defines the work of a command.
        /// </summary>
        protected abstract void DoRun();
    }
}
