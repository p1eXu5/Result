using p1eXu5.Result.Extensions;

namespace p1eXu5.Result.Tests.Extensions.TaskResultTests;

public class BindTests
{
    [Test]
    public void Bind_ExceptionIsThrows_ThrowsRaisedException()
    {
        var result = "failure".ToOkWithStringError();
        var actual = Assert.CatchAsync<InvalidOperationException>(
            async () =>
                await result
                    .Retn()
                    .Bind(sc => Task.Run(() =>
                    {
                        throw new InvalidOperationException(sc);
#pragma warning disable CS0162 // Unreachable code detected
                        return sc.ToOkWithStringError();
#pragma warning restore CS0162 // Unreachable code detected
                    }))
        );

        actual?.Message.Should().Be("failure");
    }

    [Test]
    public async Task Bind_SuccessResultWithString_ToTaskResult_ReturnsSuccesResult()
    {
        Task<Result<string, string>> task1()
            => Task.FromResult<Result<string, string>>(new Result<string, string>.Ok("sdf"));

        Task<Result<Unit, string>> task2(string arg)
            => Task.FromResult<Result<Unit, string>>(new Result<Unit, string>.Ok(new Unit()));

        var resTsk =
            task1().Bind(task2);

        var res = await resTsk;

        res.IsOk().Should().Be(true);
    }
}
