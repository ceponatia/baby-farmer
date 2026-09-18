using System;
using BabyFarmer.Domain;
using NUnit.Framework;

namespace BabyFarmer.Domain.Tests
{
    /// <summary>
    /// Trivial domain-level smoke test for the Unity bootstrap task. It proves:
    /// the domain and test assemblies compile, the Unity Test Runner can
    /// execute EditMode domain tests, and the domain layer runs correctly
    /// with zero Unity presentation dependency.
    /// </summary>
    public sealed class SlugFormatterTests
    {
        [Test]
        public void ToSlug_LowercasesAndReplacesSpaces()
        {
            var result = SlugFormatter.ToSlug("Baby Farmer");

            Assert.AreEqual("baby-farmer", result);
        }

        [Test]
        public void ToSlug_ThrowsOnEmptyInput()
        {
            Assert.Throws<ArgumentException>(() => SlugFormatter.ToSlug(""));
        }
    }
}
