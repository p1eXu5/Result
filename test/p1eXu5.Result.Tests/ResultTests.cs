namespace p1eXu5.Result.Tests;

public sealed class ResultTests
{
    [Test]
    public void EqualOperator_EqualResults_ReturnsTrue()
    {
        // Arrange:
        var r1 = new Result<string, string>.Ok("1");
        var r2 = new Result<string, string>.Ok("1");

        // Action:
        var res = r1 == r2;

        // Assert:
        res.Should().BeTrue();
    }

    [Test]
    public void Equals_EqualResults_ReturnsTrue()
    {
        // Arrange:
        var r1 = new Result<string, string>.Ok("1");
        var r2 = new Result<string, string>.Ok("1");

        // Action:
        var res = r1.Equals(r2);

        // Assert:
        res.Should().BeTrue();
    }
}
