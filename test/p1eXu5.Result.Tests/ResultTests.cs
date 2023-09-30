using p1eXu5.Result.Extensions;

namespace p1eXu5.Result.Tests;

public sealed class ResultTests
{
    [Test]
    public void EqualOperator_EqualSuccessResults_ReturnsTrue()
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
    public void Equals_EqualSuccessResults_ReturnsTrue()
    {
        // Arrange:
        var r1 = new Result<string, string>.Ok("1");
        var r2 = new Result<string, string>.Ok("1");

        // Action:
        var res = r1.Equals(r2);

        // Assert:
        res.Should().BeTrue();
    }

    [Test]
    public void EqualOperator_EqualFailedResults_ReturnsTrue()
    {
        // Arrange:
        var r1 = new Result<string, string>.Error("1");
        var r2 = "1".ToError<string>();

        // Action:
        var res = r1 == r2;

        // Assert:
        res.Should().BeTrue();
    }

    [Test]
    public void Equals_EqualFailedResults_ReturnsTrue()
    {
        // Arrange:
        var r1 = new Result<string, string>.Error("1");
        var r2 = "1".ToError<string>();

        // Action:
        var res = r1.Equals(r2);

        // Assert:
        res.Should().BeTrue();
    }
}
