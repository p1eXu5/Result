using p1eXu5.Result.Extensions;

namespace p1eXu5.Result.Tests.Extensions.ResultTests;

public class TraverseTests
{
    [Test]
    public void TraverseM_AllSucceeded_ReturnsSucceededResult()
    {
        // Arrange:
        var list = new int[] { 1, 2, 3, 4 };
        Func<int, Result<int, string>> f = (i) => i.ToOkWithStringError();

        // Action:
        var actual = list.TraverseM(f);

        // Assert:
        actual.IsOk().Should().BeTrue();
        actual.SuccessContext().Should().BeEquivalentTo(list);
    }

    [Test]
    public void TraverseM_SomeFailed_ReturnsFailedResult()
    {
        // Arrange:
        var list = new int[] { 1, 2, 3, 4 };
        bool invokedWithLastElem = false;

        Func<int, Result<int, string>> f = (i) =>
        {
            if (i == 4) invokedWithLastElem = true;
            return i == 3 ? new Result<int, string>.Error("error") : i.ToOkWithStringError();
        };

        // Action:
        var actual = list.TraverseM(f);

        // Assert:
        actual.IsOk().Should().BeFalse();
        actual.FailedContext().Should().Be("error");
        invokedWithLastElem.Should().BeFalse();
    }

    [Test]
    public void TraverseM_AllSucceeded_DifferentTypes_ReturnsSucceededResult()
    {
        // Arrange:
        var list = new int[] { 1, 2, 3, 4 };
        Func<int, Result<string, string>> f = (i) => i.ToString().ToOkWithStringError();

        // Action:
        var actual = list.TraverseM(f);

        // Assert:
        actual.IsOk().Should().BeTrue();
        actual.SuccessContext().Should().BeEquivalentTo(list.Select(i => i.ToString()));
    }

    [Test]
    public void TraverseM_SomeFailed_DifferentTypes_ReturnsFailedResult()
    {
        // Arrange:
        var list = new int[] { 1, 2, 3, 4 };
        bool invokedWithLastElem = false;
        Func<int, Result<string, string>> f = (i) =>
        {
            if (i == 4) invokedWithLastElem = true;
            return i == 3 ? "error".ToError<string>() : i.ToString().ToOkWithStringError();
        };

        // Action:
        var actual = list.TraverseM(f);

        // Assert:
        actual.IsOk().Should().BeFalse();
        actual.FailedContext().Should().Be("error");
        invokedWithLastElem.Should().BeFalse();
    }

    [Test]
    public async Task TraverseM_TraverseTask()
    {
        // Arrange:
        var result = new Result<Task<int>, string>.Ok(Task.FromResult(12));

        // Action:
        var taskResult = result.SequenceTask();

        // Assert:
        var actual = await taskResult;
        actual.IsOk().Should().BeTrue();
        actual.SuccessContext().Should().Be(12);
    }


    [Test]
    public void TraverseA_AllSucceeded_ReturnsSucceededResult()
    {
        // Arrange:
        var list = new int[] { 1, 2, 3, 4 };
        Func<int, Result<int, string>> f = (i) => i.ToOkWithStringError();

        // Action:
        var actual = list.TraverseA(f);

        // Assert:
        actual.result.IsOk().Should().BeTrue();
        actual.result.SuccessContext().Should().BeEquivalentTo(list);
    }

    [Test]
    public void TraverseA_SomeFailed_ReturnsSucceededResult()
    {
        // Arrange:
        var list = new int[] { 1, 2, 3, 4 };
        bool invokedWithLastElem = false;

        Func<int, Result<int, string>> f = (i) =>
        {
            if (i == 4) invokedWithLastElem = true;
            return i == 3 ? new Result<int, string>.Error("error") : i.ToOkWithStringError();
        };

        // Action:
        var actual = list.TraverseA(f);

        // Assert:
        actual.result.IsOk().Should().BeTrue();
        actual.errors.Should().HaveCount(1).And.BeEquivalentTo(new[] { "error" });
        invokedWithLastElem.Should().BeTrue();
    }


    [Test]
    public void TraverseA_AllFailed_ReturnsFailedResult()
    {
        // Arrange:
        var list = new int[] { 1, 2, 3, 4 };
        bool invokedWithLastElem = false;

        Func<int, Result<int, string>> f = (i) =>
        {
            if (i == 4) invokedWithLastElem = true;
            return new Result<int, string>.Error("error");
        };

        // Action:
        var actual = list.TraverseA(f);

        // Assert:
        actual.result.IsOk().Should().BeFalse();
        actual.errors.Should().HaveCount(4);
        invokedWithLastElem.Should().BeTrue();
    }
}
