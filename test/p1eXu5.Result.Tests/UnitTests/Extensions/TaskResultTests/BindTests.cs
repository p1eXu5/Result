using p1eXu5.Result.Extensions;

namespace p1eXu5.Result.Tests.UnitTests.Extensions.TaskResultTests;

public class BindTests
{
    [Test]
    public void Bind_ExceptionIsThrows_ThrowsRaisedException()
    {
        var result = "failure".ToSuccessResult();
        var actual = Assert.CatchAsync<InvalidOperationException>(
            async () =>
                await result
                    .Retn()
                    .Bind(sc => Task.Run<Result<string>>(() => {
                        throw new InvalidOperationException(sc);
#pragma warning disable CS0162 // Unreachable code detected
                        return sc.ToSuccessResult();
#pragma warning restore CS0162 // Unreachable code detected
                    }))
        );

        actual?.Message.Should().Be("failure");
    }

    [Test]
    public async Task Bind_SuccesResultWithString_ToTaskResult_ReturnsSuccesResult()
    {
        Task<Result<string>> task1()
            => Task.FromResult(Result<string>.Success("sdf"));

        Task<Result> task2(string arg)
            => Task.FromResult(Result.Success());

        var resTsk =
            task1().Bind(task2);

        var res = await resTsk;

        res.Succeeded.Should().Be(true);
    }
}
