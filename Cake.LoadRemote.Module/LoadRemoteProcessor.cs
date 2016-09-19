namespace Cake.LoadRemote.Module
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    using Cake.Core;
    using Cake.Core.Factories;
    using Cake.Core.Scripting.Analysis;
    using Cake.Core.Scripting.Processors;

    /// <summary>
    /// The load remote processor.
    /// </summary>
    public sealed class LoadRemoteProcessor : UriDirectiveProcessor
    {
        /// <summary>
        /// The component factory.
        /// </summary>
        private readonly ICakeComponentFactory componentFactory;

        /// <summary>
        /// The processor extension.
        /// </summary>
        private readonly IProcessorExtension processorExtension;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadRemoteProcessor"/> class.
        /// </summary>
        /// <param name="processorExtension">
        /// The processor extension.
        /// </param>
        /// <param name="environment">
        /// The environment.
        /// </param>
        public LoadRemoteProcessor(IProcessorExtension processorExtension, ICakeEnvironment environment)
            : base(environment)
        {
            this.processorExtension = processorExtension;
            componentFactory = new CakeComponentFactory();
        }

        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
        protected override void AddToContext(IScriptAnalyzerContext context, Uri uri)
        {
            var package = componentFactory.CreatePackageReference(uri);
            context.Script.ProcessorValues.Add(processorExtension, package);
        }

        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
        protected override string GetDirectiveName()
        {
            return Constants.DirectiveName;
        }
    }
}