namespace p1eXu5.Result
{
    using Generic;
    using Exceptions;

    public interface IResult< TSuccess, TFailure >
    {
        /// <summary>
        /// Gets a value indicating whether the <see cref="Result{TSuccess,TFailure}"/> is succeeded.
        /// </summary>
        bool Succeeded { get; }

        /// <summary>
        /// Gets a value indicating whether the <see cref="Result{TSuccess,TFailure}"/> is failed.
        /// </summary>
        bool Failed { get; }

        /// <summary>
        /// Gets succeeded context. If <see cref="Result{TSuccess,TFailure}"/> failed then throws <see cref="ResultContextAccessException"/>.
        /// </summary>
        /// <exception cref="ResultContextAccessException"></exception>
        TSuccess SuccessContext { get; }

        /// <summary>
        /// Gets failed context. If <see cref="Result{TSuccess,TFailure}"/> failed then throws <see cref="ResultContextAccessException"/>.
        /// </summary>
        /// <exception cref="ResultContextAccessException"></exception>
        TFailure FailedContext { get; }

        /// <summary>
        /// Gets the succeeded result context.
        /// </summary>
        /// <param name="succeededContext"></param>
        /// <returns></returns>
        bool TryGetSucceededContext( out TSuccess succeededContext );

        /// <summary>
        /// Gets the failed result context.
        /// </summary>
        /// <param name="failedContext"></param>
        /// <returns></returns>
        bool TryGetFailedContext( out TFailure failedContext );
    }
}