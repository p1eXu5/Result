#nullable enable

namespace p1eXu5.Result.Extensions;

using Unit = ValueTuple;


/// <summary>
/// <see cref="Result{TSuccess, TFailure}"/> extensions.
/// </summary>
public static partial class ResultExtensions
{
    public static Result< Unit, int > ToSuccessResult( this int _ )
        => Result< Unit, int >.Success( new Unit() );

    public static Result< Unit, int > ToFiledResult( this int failedContext )
        => Result< Unit, int >.Failure( failedContext );

    public static Result< Unit, int > ToResult( this int failedContext, Predicate< int > isSuccessful )
        => 
            isSuccessful( failedContext )
                ? failedContext.ToSuccessResult()
                : failedContext.ToFiledResult();

    public static Result< TSuccess > ToSuccessResult< TSuccess >( this TSuccess successContext )
        => Result< TSuccess >.Success( successContext );

    public static Result<TSuccess> ToFailedResult<TSuccess>(this TSuccess _)
        => Result<TSuccess>.Failure();

    public static Result<TSuccess> ToFailedResult<TSuccess>(this TSuccess _, string error)
        => Result<TSuccess>.Failure(error);

    public static Result<TSuccess> ToFailedResult<TSuccess>(this TSuccess _, Exception exception)
        => Result<TSuccess>.Failure(exception);
}
