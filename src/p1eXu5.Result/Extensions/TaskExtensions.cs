#nullable enable

namespace p1eXu5.Result.Extensions;

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
    public static Task<Result<TContext>> ToTaskResult<TContext>(this Task<TContext> task, CancellationToken cancellationToken)
    {
        if (task.IsFaulted)
        {
            return Task.FromResult(Result<TContext>.Failure(task.Exception!.Flatten()));
        }

        if (task.IsCanceled)
        {
            return Task.FromResult(Result<TContext>.Failure("Task was canceled."));
        }

        if (task.IsCompleted)
        {
            return Task.FromResult(Result<TContext>.Success(task.Result));
        }

        return task.ContinueWith(Continuation<TContext>(), cancellationToken);
    }

    /// <summary>
    /// Returns <see cref="Task"/>&lt;<see cref="Result"/>&gt; from <see cref="Task"/>.
    /// </summary>
    /// <param name="task"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<Result> ToTaskResult(this Task task, CancellationToken cancellationToken)
    {
        if (task.IsFaulted)
        {
            return Task.FromResult(Result.Failure(task.Exception));
        }

        if (task.IsCanceled)
        {
            return Task.FromResult(Result.Failure("Task was canceled."));
        }

        if (task.IsCompleted)
        {
            return Task.FromResult(Result.Success());
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
    public static ValueTask<Result<TContext>> ToValueTaskResult<TContext>(this Task<TContext> task, CancellationToken cancellationToken)
    {
        if (task.IsFaulted)
        {
            return new ValueTask<Result<TContext>>(Result<TContext>.Failure(task.Exception));
        }

        if (task.IsCanceled)
        {
            return new ValueTask<Result<TContext>>(Result<TContext>.Failure("Task was canceled."));
        }

        if (task.IsCompleted)
        {
            return new ValueTask<Result<TContext>>(Result<TContext>.Success(task.Result));
        }

        return new ValueTask<Result<TContext>>(task.ContinueWith(
            Continuation<TContext>(), cancellationToken));
    }

    /// <summary>
    /// Returns <see cref="ValueTask"/>&lt;<see cref="Result"/>&gt; from <see cref="Task"/>.
    /// </summary>
    /// <param name="task"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static ValueTask<Result> ToValueTaskResult(this Task task, CancellationToken cancellationToken)
    {
        if (task.IsFaulted)
        {
            return new ValueTask<Result>(Result.Failure(task.Exception));
        }

        if (task.IsCanceled)
        {
            return new ValueTask<Result>(Result.Failure("Task was canceled."));
        }

        if (task.IsCompleted)
        {
            return new ValueTask<Result>(Result.Success());
        }

        return new ValueTask<Result>(task.ContinueWith(
            Continuation(), cancellationToken));
    }

    /// <summary>
    /// Returns <see cref="ValueTask"/>&lt;<see cref="Result"/>&lt;<typeparamref name="TContext"/>&gt;&gt; from <see cref="ValueTask"/>&lt;<typeparamref name="TContext"/>&gt;.
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    /// <param name="task"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static ValueTask<Result<TContext>> ToValueTaskResult<TContext>(this ValueTask<TContext> task, CancellationToken cancellationToken)
    {
        if (task.IsFaulted)
        {
            return new ValueTask<Result<TContext>>(Result<TContext>.Failure(task.AsTask().Exception));
        }

        if (task.IsCanceled)
        {
            return new ValueTask<Result<TContext>>(Result<TContext>.Failure("Task was canceled."));
        }

        if (task.IsCompleted)
        {
            return new ValueTask<Result<TContext>>(Result<TContext>.Success(task.Result));
        }

        return new ValueTask<Result<TContext>>(task.AsTask().ContinueWith(
            Continuation<TContext>(), cancellationToken));
    }

    /// <summary>
    /// Returns <see cref="ValueTask"/>&lt;<see cref="Result"/>&gt; from <see cref="ValueTask"/>.
    /// </summary>
    /// <param name="task"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static ValueTask<Result> ToValueTaskResult(this ValueTask task, CancellationToken cancellationToken)
    {
        if (task.IsFaulted)
        {
            return new ValueTask<Result>(Result.Failure(task.AsTask().Exception));
        }

        if (task.IsCompleted)
        {
            return new ValueTask<Result>(Result.Success());
        }

        if (task.IsCanceled)
        {
            return new ValueTask<Result>(Result.Failure("Task was canceled."));
        }

        return new ValueTask<Result>(task.AsTask().ContinueWith(
            Continuation(), cancellationToken));
    }


    // ----------------
    // privates methods
    // ----------------

    private static Func<Task<TContext>, Result<TContext>> Continuation<TContext>()
    {
        return t =>
        {
            if (t.IsFaulted)
            {
                return Result<TContext>.Failure(t.Exception);
            }

            if (t.IsCanceled)
            {
                return Result<TContext>.Failure("Task was canceled.");
            }

            if (t.IsCompleted)
            {
                return Result<TContext>.Success(t.Result);
            }

            return Result<TContext>.Failure("Task was not failed, not canceled and not completed.");
        };
    }


    private static Func<Task, Result> Continuation()
    {
        return t =>
        {
            if (t.IsFaulted)
            {
                return Result.Failure(t.Exception);
            }

            if (t.IsCanceled)
            {
                return Result.Failure("Task was canceled.");
            }

            if (t.IsCompleted)
            {
                return Result.Success();
            }

            return Result.Failure("Task was not failed, not canceled and not completed.");
        };
    }
}
