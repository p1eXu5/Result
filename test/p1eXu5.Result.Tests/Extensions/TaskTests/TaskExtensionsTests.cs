﻿using p1eXu5.Result.Extensions;

namespace p1eXu5.Result.Tests.Extensions.TaskTests;

public class TaskExtensionsTests
{
    [Test]
    public async Task ToTaskResult_GenericTaskCompleted_ReturnsSucceededResult()
    {
        var task = Task.FromResult("Ok");

        Result<string, string> result = await task.ToTaskResult(CancellationToken.None).MapError(ex => ex.ToString());

        result.IsOk().Should().Be(true);
        result.SuccessContext().Should().Be("Ok");
    }

    [Test]
    public async Task ToTaskResult_GenericTaskCanceled_ReturnsFailedResult()
    {
        CancellationTokenSource source = new();
        var token = source.Token;
        source.Cancel();

        var task = Task.FromCanceled<string>(token);

        Result<string, string> result = await task.ToTaskResult(token).MapError(ex => ex.ToString());

        result.IsOk().Should().Be(false);
        result.FailedContext().Should().Contain("Task was canceled.");
    }

    [Test]
    public async Task ToTaskResult_GenericTaskFailed_ReturnsFailedResult()
    {
        var task = Task.FromException<string>(new ArgumentException("error"));

        Result<string, string> result = await task.ToTaskResult(CancellationToken.None).MapError(ex => ex.ToString());

        result.IsOk().Should().Be(false);
        result.FailedContext().Should().Contain("error");
    }

    [Test]
    public async Task ToTaskResult_TaskCompleted_ReturnsSucceededResult()
    {
        var task = Task.CompletedTask;

        Result<Unit, string> result = await task.ToTaskResult(CancellationToken.None).MapError(err => err.ToString());

        result.IsOk().Should().Be(true);
    }

    [Test]
    public async Task ToTaskResult_TaskCanceled_ReturnsFailedResult()
    {
        CancellationTokenSource source = new();
        var token = source.Token;
        source.Cancel();

        var task = Task.FromCanceled(token);

        Result<Unit, string> result = await task.ToTaskResult(token).MapError(err => err.ToString());

        result.IsOk().Should().Be(false);
        result.FailedContext().Should().Contain("Task was canceled.");
    }


    [Test]
    public async Task ToTaskResultt_TaskFailed_ReturnsFailedResult()
    {
        var task = Task.FromException(new ArgumentException("error"));

        Result<Unit, string> result = await task.ToTaskResult(CancellationToken.None).MapError(err => err.ToString());

        result.IsOk().Should().Be(false);
        result.FailedContext().Should().Contain("error");
    }
}
