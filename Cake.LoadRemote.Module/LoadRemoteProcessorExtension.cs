namespace Cake.LoadRemote.Module
{
    using System;
    using System.Text.RegularExpressions;

    using Cake.Core;
    using Cake.Core.Diagnostics;
    using Cake.Core.Scripting;
    using Cake.Core.Scripting.Analysis;
    using Cake.Core.Scripting.Processors;

    /// <summary>
    /// The load remote processor extension.
    /// </summary>
    public sealed class LoadRemoteProcessorExtension : ProcessorExtension
    {
        /// <summary>
        /// The match directive name.
        /// </summary>
        private readonly Regex matchDirectiveName = new Regex("^(#load|#l)", RegexOptions.Compiled);

        /// <summary>
        /// The processor.
        /// </summary>
        private readonly UriDirectiveProcessor processor;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadRemoteProcessorExtension"/> class.
        /// </summary>
        /// <param name="cakeContext">The <see cref="ICakeContext"/>.</param>
        /// <param name="environment">The <see cref="ICakeEnvironment"/>.</param>
        /// <param name="cakeLog">The <see cref="ICakeLog"/>.</param>
        /// <param name="scriptProcessor">The <see cref="IScriptProcessor"/>.</param>
        public LoadRemoteProcessorExtension(
            ICakeContext cakeContext,
            ICakeEnvironment environment,
            ICakeLog cakeLog,
            IScriptProcessor scriptProcessor)
            : base(cakeContext, environment, cakeLog, scriptProcessor)
        {
            this.processor = new LoadRemoteProcessor(this, environment);
            ScriptRunnerExtension = new LoadRemoteScriptRunnerExtension(
                                        this,
                                        cakeContext,
                                        environment,
                                        cakeLog,
                                        scriptProcessor);
        }

        /// <summary>
        /// Gets the script runner extension.
        /// </summary>
        public override IScriptRunnerExtension ScriptRunnerExtension { get; }

        /// <summary>
        /// Determine if <c>this</c> <see cref="IProcessorExtension"/> can process the directive <paramref name="alias"/>.
        /// </summary>
        /// <param name="alias">directive processor alias</param>
        /// <param name="value">the alias value</param>
        /// <returns>True if <see cref="IProcessorExtension"/> can process this <paramref name="alias"/>, else False</returns>
        public override bool CanProcessDirective(string alias, string value)
        {
            if ((alias.Equals("#l", StringComparison.Ordinal) || alias.Equals("#load", StringComparison.Ordinal))
                && value.Contains("nuget:"))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Processes the specified line.
        /// </summary>
        /// <param name="analyzer">The analyzer.</param>
        /// <param name="line">The line.</param>
        /// <param name="replacement">The replacement for line, null if no replacement</param>
        /// <returns><c>true</c> if the line was processed
        /// by this processor; otherwise <c>false</c>.</returns>
        public override bool Process(IScriptAnalyzerContext analyzer, string line, out string replacement)
        {
            // How to Support both "#l" and "#load", replace to Constants.DirectiveName and process it with the LoadRemoteProcessor.
            line = line.Trim();
            line = matchDirectiveName.Replace(line, Constants.DirectiveName);

            // Set the replacement line to the modified line.
            replacement = string.Concat("// ", line);

            // We need a out value to call the processor.
            string outValue;

            // This is a nuget reference use the NugetScript processor.
            return processor.Process(analyzer, line, out outValue);
        }
    }
}