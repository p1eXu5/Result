using p1eXu5.Result.Extensions;

namespace p1eXu5.Result.Tests.UnitTests.Extensions.ResultTests;

public class BindTests
{
    [Test]
    public void Bind_FunctionReturnsSucceeded_ReturnsSucceededWithExpectedType()
    {
        // Arrange:
        Result<int> v = Result.Success(18);
        Func<int, Result<string>> f = i => Result.Success(i.ToString());

        // Action:
        var actual = v.Bind(f);

        // Assert:
        actual.Should().Be(Result.Success("18"));
    }

    [Test]
    public void Bind_FunctionReturnsFailure_ReturnsFailureWithExpectedType()
    {
        // Arrange:
        Result<int> v = Result.Success(18);
        Func<int, Result<Type>> f = i => Result.Failure<Type>(i.ToString());

        // Action:
        var actual = v.Bind(f);

        // Assert:
        actual.Should().Be(Result.Failure<Type>("18"));
    }



    [Test]
    public void BindFlatTest()
    {
        var res = (12, "asd").ToSuccessResult();

        var actual =
            res.BindFlat(() => typeof(string).ToSuccessResult())
                .Map((i, s, t) => "done");

        actual.Succeeded.Should().BeTrue();
        actual.SuccessContext.Should().Be("done");
    }
}
