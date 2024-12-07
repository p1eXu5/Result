
namespace p1eXu5.Result.Extensions;

using Unit = ValueTuple;

/// <summary>
/// Extension methods for converting <see cref="Task"/>'s and <see cref="ValueTask"/>'s to <see cref="Task"/>'s 
/// and <see cref="ValueTask"/>'s with <see cref="Result"/> objects.
/// </summary>
public static partial class TaskExtensions
{
    /// <summary>
    /// Returns <see cref="Task"/>&lt;<see cref="Result"/>&lt;<typeparamref name="TContext"/>&gt;&gt; from <see cref="Task"/>&lt;<typeparamref name="TContext"/>&gt;.
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    /// <param name="task"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<Result<TContext, Exception>> ToTaskResult<TContext>(this Task<TContext> task, CancellationToken cancellationToken)
    {
        if (task.IsFaulted)
        {
            return Task.FromResult<Result<TContext, Exception>>(new Result<TContext, Exception>.Error(task.Exception!.Flatten()));
        }

        if (task.IsCanceled)
        {
            return Task.FromResult<Result<TContext, Exception>>(new Result<TContext, Exception>.Error(new TaskCanceledException("Task was canceled.")));
        }

        if (task.IsCompleted)
        {
            return Task.FromResult<Result<TContext, Exception>>(new Result<TContext, Exception>.Ok(task.Result));
        }

        return task.ContinueWith(Continuation<TContext>(), cancellationToken);
    }

    /// <summary>
    /// Returns <see cref="Task"/>&lt;<see cref="Result"/>&gt; from <see cref="Task"/>.
    /// </summary>
    /// <param name="task"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<Result<Unit, Exception>> ToTaskResult(this Task task, CancellationToken cancellationToken)
    {
        if (task.IsFaulted)
        {
            return Task.FromResult<Result<Unit, Exception>>(new Result<Unit, Exception>.Error(task.Exception));
        }

        if (task.IsCanceled)
        {
            return Task.FromResult<Result<Unit, Exception>>(new Result<Unit, Exception>.Error(new TaskCanceledException("Task was canceled.")));
        }

        if (task.IsCompleted)
        {
            return Task.FromResult(Result.UnitOkWith<Exception>());
        }

        return task.ContinueWith(
            Continuation(), cancellationToken);
    }

    /// <summary>
    /// Returns <see cref="ValueTask"/>&lt;<see cref="Result"/>&lt;<typeparamref name="TContext"/>&gt;&gt; from <see cref="Task"/>&lt;<typeparamref name="TContext"/>&gt;.
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    /// <param name="task"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static ValueTask<Result<TContext, Exception>> ToValueTaskResult<TContext>(this Task<TContext> task, CancellationToken cancellationToken)
    {
        if (task.IsFaulted)
        {
            return new ValueTask<Result<TContext, Exception>>(new Result<TContext, Exception>.Error(task.Exception));
        }

        if (task.IsCanceled)
        {
            return new ValueTask<Result<TContext, Exception>>(new Result<TContext, Exception>.Error(new TaskCanceledException("Task was canceled.")));
        }

        if (task.IsCompleted)
        {
            return new ValueTask<Result<TContext, Exception>>(new Result<TContext, Exception>.Ok(task.Result));
        }

        return new ValueTask<Result<TContext, Exception>>(task.ContinueWith(
            Continuation<TContext>(), cancellationToken));
    }

    /// <summary>
    /// Returns <see cref="ValueTask"/>&lt;<see cref="Result"/>&gt; from <see cref="Task"/>.
    /// </summary>
    /// <param name="task"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static ValueTask<Result<Unit, Exception>> ToValueTaskResult(this Task task, CancellationToken cancellationToken)
    {
        if (task.IsFaulted)
        {
            return new ValueTask<Result<Unit, Exception>>(new Result<Unit, Exception>.Error(task.Exception));
        }

        if (task.IsCanceled)
        {
            return new ValueTask<Result<Unit, Exception>>(new Result<Unit, Exception>.Error(new TaskCanceledException("Task was canceled.")));
        }

        if (task.IsCompleted)
        {
            return new ValueTask<Result<Unit, Exception>>(Result.UnitOkWith<Exception>());
        }

        return new ValueTask<Result<Unit, Exception>>(task.ContinueWith(
            Continuation(), cancellationToken));
    }

    /// <summary>
    /// Returns <see cref="ValueTask"/>&lt;<see cref="Result"/>&lt;<typeparamref name="TContext"/>&gt;&gt; from <see cref="ValueTask"/>&lt;<typeparamref name="TContext"/>&gt;.
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    /// <param name="task"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static ValueTask<Result<TContext, Exception>> ToValueTaskResult<TContext>(this ValueTask<TContext> task, CancellationToken cancellationToken)
    {
        if (task.IsFaulted)
        {
            return new ValueTask<Result<TContext, Exception>>(new Result<TContext, Exception>.Error(task.AsTask().Exception!));
        }

        if (task.IsCanceled)
        {
            return new ValueTask<Result<TContext, Exception>>(new Result<TContext, Exception>.Error(new TaskCanceledException("Task was canceled.")));
        }

        if (task.IsCompleted)
        {
            return new ValueTask<Result<TContext, Exception>>(new Result<TContext, Exception>.Ok(task.Result));
        }

        return new ValueTask<Result<TContext, Exception>>(task.AsTask().ContinueWith(
            Continuation<TContext>(), cancellationToken));
    }

    /// <summary>
    /// Returns <see cref="ValueTask"/>&lt;<see cref="Result"/>&gt; from <see cref="ValueTask"/>.
    /// </summary>
    /// <param name="task"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static ValueTask<Result<Unit, Exception>> ToValueTaskResult(this ValueTask task, CancellationToken cancellationToken)
    {
        if (task.IsFaulted)
        {
            return new ValueTask<Result<Unit, Exception>>(new Result<Unit, Exception>.Error(task.AsTask().Exception!));
        }

        if (task.IsCompleted)
        {
            return new ValueTask<Result<Unit, Exception>>(Result.UnitOkWith<Exception>());
        }

        if (task.IsCanceled)
        {
            return new ValueTask<Result<Unit, Exception>>(new Result<Unit, Exception>.Error(new TaskCanceledException("Task was canceled.")));
        }

        return new ValueTask<Result<Unit, Exception>>(task.AsTask().ContinueWith(
            Continuation(), cancellationToken));
    }


    // ----------------
    // privates methods
    // ----------------

    private static Func<Task<TContext>, Result<TContext, Exception>> Continuation<TContext>()
    {
        return t =>
        {
            if (t.IsFaulted)
            {
                return new Result<TContext, Exception>.Error(t.Exception!);
            }

            if (t.IsCanceled)
            {
                return new Result<TContext, Exception>.Error(new TaskCanceledException("Task was canceled."));
            }

            if (t.IsCompleted)
            {
                return new Result<TContext, Exception>.Ok(t.Result);
            }

            return new Result<TContext, Exception>.Error(new InvalidOperationException("Task was not failed, not canceled and not completed."));
        };
    }


    private static Func<Task, Result<Unit, Exception>> Continuation()
    {
        return t =>
        {
            if (t.IsFaulted)
            {
                return new Result<Unit, Exception>.Error(t.Exception);
            }

            if (t.IsCanceled)
            {
                return new Result<Unit, Exception>.Error(new TaskCanceledException("Task was canceled."));
            }

            if (t.IsCompleted)
            {
                return Result.UnitOkWith<Exception>();
            }

            return new Result<Unit, Exception>.Error(new InvalidOperationException("Task was not failed, not canceled and not completed."));
        };
    }
}
