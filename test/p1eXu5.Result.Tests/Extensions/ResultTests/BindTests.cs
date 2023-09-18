using p1eXu5.Result.Extensions;

namespace p1eXu5.Result.Tests.Extensions.ResultTests;

using Unit = Unit;

public class BindTests
{
    [Test]
    public void Bind_FunctionReturnsSucceeded_ReturnsSucceededWithExpectedType()
    {
        // Arrange:
        Result<int, Unit> v = 18.ToOk();
        Func<int, Result<string, Unit>> f = i => i.ToString().ToOk();

        // Action:
        var actual = v.Bind(f);

        // Assert:
        actual.Should().Be("18".ToOk());
    }

    [Test]
    public void Bind_FunctionReturnsFailure_ReturnsFailureWithExpectedType()
    {
        // Arrange:
        var v = 18.ToOk<int, string>();
        Func<int, Result<Type, string>> f = i => new Result<Type, string>.Error(i.ToString());

        // Action:
        var actual = v.Bind(f);

        // Assert:
        actual.Should().Be(new Result<Type, string>.Error("18"));
    }



    [Test]
    public void BindFlatTest()
    {
        var res = new Result<(int, string), string>.Ok((12, "asd"));

        var actual =
            res.BindFlat(() => typeof(string).ToOk<Type, string>())
                .Map(_ => "done");

        actual.IsOk().Should().BeTrue();
        actual.SuccessContext().Should().Be("done");
    }
}
