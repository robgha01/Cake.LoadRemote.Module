namespace Cake.LoadRemote.Module.Commands
{
    using System;
    using System.Collections.Generic;

    using Cake.LoadRemote.Module.Abstraction;

    using Core;
    using Core.Diagnostics;
    using Core.IO;

    /// <summary>
    /// The purge cache.
    /// </summary>
    public class PurgeCache : CommandBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PurgeCache"/> class.
        /// </summary>
        /// <param name="cakeContext">
        /// The cake context.
        /// </param>
        /// <param name="environment">
        /// The environment.
        /// </param>
        /// <param name="log">
        /// The log.
        /// </param>
        public PurgeCache(ICakeContext cakeContext, ICakeEnvironment environment, ICakeLog log)
            : base(cakeContext, environment, log)
        {
            Mode = CommandMode.RunOnce;
        }

        /// <summary>
        /// Gets the mode.
        /// </summary>
        public override CommandMode Mode { get; }

        /// <summary>
        /// Defines the work of a command.
        /// </summary>
        public override void DoRun()
        {
            var options = new LoadRemoteOptions(CakeContext);

            if (!options.ClearNugetCache)
            {
                return;
            }

            var nugetPath = CakeContext.Tools.Resolve("nuget.exe");
            IProcessRunner processRunner = new ProcessRunner(Environment, Log);

            var process = processRunner.Start(
                nugetPath,
                new ProcessSettings
                {
                    Arguments = "locals all -clear",
                    RedirectStandardOutput = true,
                    Silent = Log.Verbosity < Verbosity.Diagnostic
                });
            process.WaitForExit();

            var exitCode = process.GetExitCode();
            if (exitCode != 0)
            {
                Log.Warning("NuGet exited with {0}", exitCode);
                var output = string.Join(System.Environment.NewLine, process.GetStandardOutput());
                Log.Verbose(Verbosity.Diagnostic, "Output:\r\n{0}", output);
            }
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
        public override bool IsArgumentEquals(string name)
        {
            return name.Equals(Constants.PurgeCacheArgument, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
