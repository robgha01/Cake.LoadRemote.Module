namespace Cake.LoadRemote.Module
{
    using System;
    using Abstraction;
    using Core;

    /// <summary>
    /// The load remote options.
    /// </summary>
    public class LoadRemoteOptions : ILoadRemoteOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoadRemoteOptions"/> class. 
        /// </summary>
        /// <param name="cakeContext">
        /// The <see cref="ICakeContext"/>.
        /// </param>
        public LoadRemoteOptions(ICakeContext cakeContext)
        {
            ClearNugetCache = false;

            if (cakeContext.Arguments.HasArgument(Constants.PurgeCacheArgument))
            {
                var value = cakeContext.Arguments.GetArgument(Constants.PurgeCacheArgument);
                ClearNugetCache = ParseBooleanValue(value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to clear the nuget cache before installing packages.
        /// </summary>
        /// <value>
        ///   <c>true</c> to clear the nuget cache; otherwise, <c>false</c>.
        /// </value>
        public bool ClearNugetCache { get; set; }

        /// <summary>
        /// The parse boolean value.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// if value is not a valid bool.
        /// </exception>
        private static bool ParseBooleanValue(string value)
        {
            value = (value ?? string.Empty).UnQuote();
            if (string.IsNullOrWhiteSpace(value))
            {
                return true;
            }

            if (value.Equals("true", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (value.Equals("false", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            throw new InvalidOperationException("Argument value is not a valid boolean value.");
        }
    }
}
