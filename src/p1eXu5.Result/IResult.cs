#nullable enable

namespace p1eXu5.Result;

using Exceptions;

public interface IResult
{
    /// <summary>
    /// Gets a value indicating whether the <see cref="IResult"/> is succeeded.
    /// </summary>
    bool Succeeded { get; }

    /// <summary>
    /// Gets a value indicating whether the <see cref="IResult"/> is failed.
    /// </summary>
    bool Failed { get; }
}

public interface IResult<TOk, TFailure> : IResult
{
    /// <summary>
    /// Gets succeeded context. If <see cref="Result{TOk,TFailure}"/> failed then throws <see cref="ResultContextAccessException"/>.
    /// </summary>
    /// <exception cref="ResultContextAccessException"></exception>
    TOk SuccessContext { get; }

    /// <summary>
    /// Gets failed context. If <see cref="Result{TOk,TFailure}"/> failed then throws <see cref="ResultContextAccessException"/>.
    /// </summary>
    /// <exception cref="ResultContextAccessException"></exception>
    TFailure FailedContext { get; }

    /// <summary>
    /// Gets the succeeded result context.
    /// </summary>
    /// <param name="succeededContext"></param>
    /// <returns></returns>
    bool TryGetSucceededContext( out TOk succeededContext );

    /// <summary>
    /// Gets the failed result context.
    /// </summary>
    /// <param name="failedContext"></param>
    /// <returns></returns>
    bool TryGetFailedContext( out TFailure failedContext );
}