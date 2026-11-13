namespace DotNetBlazorDashboard.Tests;

using NUnit.Framework;

[TestFixture]
public class SampleNUnitTests
{
    [Test]
    public void NUnit_BasicMathAssertion_Passes()
    {
        var result = 1 + 1;
        Assert.That(result, Is.EqualTo(2));
    }
}

