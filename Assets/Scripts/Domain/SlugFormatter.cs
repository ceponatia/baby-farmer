using System;

namespace BabyFarmer.Domain
{
    /// <summary>
    /// Placeholder domain-layer smoke-test subject for the Unity bootstrap task.
    /// It is not a gameplay system. Its only purpose is to prove that this
    /// assembly compiles with zero dependency on Unity presentation APIs
    /// (enforced by "noEngineReferences" in BabyFarmer.Domain.asmdef) and that
    /// its logic can be exercised by the Unity Test Runner.
    /// </summary>
    public static class SlugFormatter
    {
        /// <summary>
        /// Converts arbitrary display text into a lowercase, hyphenated slug.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="input"/> is null, empty, or whitespace.
        /// </exception>
        public static string ToSlug(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentException("Input must not be null, empty, or whitespace.", nameof(input));
            }

            return input.Trim().ToLowerInvariant().Replace(' ', '-');
        }
    }
}
