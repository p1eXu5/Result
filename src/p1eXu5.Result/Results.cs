﻿namespace p1eXu5.Result;

using System.Text.Json.Serialization;
using p1eXu5.Result.Exceptions;
using Unit = ValueTuple;


public abstract record Result<TOk, TError>
{
    public sealed record Ok(TOk SuccessContext) : Result<TOk, TError>;

    public sealed record Error(TError FailedContext) : Result<TOk, TError>;
}

public static class Result
{
    public static Result<Unit, TError> UnitOkWith<TError>()
        => new Result<Unit, TError>.Ok(new Unit());

    public static Result<TOk, Unit> UnitErrorWith<TOk>()
        => new Result<TOk, Unit>.Error(new Unit());
}

/*
/// <summary>
/// Result class with generic succeeded and failed contexts.
/// </summary>
/// <typeparam name="TOk"></typeparam>
/// <typeparam name="TFailure"></typeparam>
public class Result_<TOk, TFailure> : IResult<TOk, TFailure>
{
    #region fields

    private readonly TOk _successContext = default!;
    private readonly TFailure _failureContext = default!;

    #endregion ----------------------------------------------------- fields


    #region ctor

    /// <summary>
    /// Initializes a new instance of the <see cref="Result{TOk,TFailure}"/> class
    /// with succeeded context.
    /// </summary>
    /// <param name="successContext"></param>
    protected Result(TOk successContext)
    {
        _successContext = successContext;
        Succeeded = true;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Result{TOk,TFailure}"/> class
    /// with failed context. 
    /// </summary>
    /// <param name="failureContext"></param>
    protected Result(TFailure failureContext)
    {
        _failureContext = failureContext;
        Succeeded = false;
    }

    #endregion ----------------------------------------------------- ctor


    #region properties

    /// <summary>
    /// Gets a value indicating whether the <see cref="Result{TOk,TFailure}"/> is succeeded.
    /// </summary>
    public bool Succeeded { get; }

    /// <summary>
    /// Gets a value indicating whether the <see cref="Result{TOk,TFailure}"/> is failed.
    /// </summary>
    public bool Failed => !Succeeded;

    /// <summary>
    /// Gets succeeded context. If <see cref="Result{TOk,TFailure}"/> failed then throws <see cref="ResultContextAccessException"/>.
    /// </summary>
    /// <exception cref="ResultContextAccessException"></exception>
    public TOk SuccessContext
    {
        get {
            if (TryGetSucceededContext(out var success)) {
                return success;
            }

            throw new ResultContextAccessException("Result is failed.");
        }
    }

    /// <summary>
    /// Gets failed context. If <see cref="Result{TOk,TFailure}"/> failed then throws <see cref="ResultContextAccessException"/>.
    /// </summary>
    /// <exception cref="ResultContextAccessException"></exception>
    public TFailure FailedContext
    {
        get {
            if (TryGetFailedContext(out var failure)) {
                return failure;
            }

            throw new ResultContextAccessException("Result is succeeded.");
        }
    }

    #endregion ----------------------------------------------------- properties


    #region static factory methods 

    /// <summary>
    /// Creates succeeded <see cref="Result{TOk,TFailure}"/>
    /// </summary>
    /// <param name="successContext"></param>
    /// <returns></returns>
    public static Result<TOk, TFailure> Success(TOk successContext) => new(successContext);

    /// <summary>
    /// Creates failed <see cref="Result{TOk,TFailure}"/>
    /// </summary>
    /// <param name="failureContext"></param>
    /// <returns></returns>
    public static Result<TOk, TFailure> Failure(TFailure failureContext) => new(failureContext);

    #endregion ----------------------------------------------------- static factory methods


    #region methods

    /// <summary>
    /// Gets the succeeded result context.
    /// </summary>
    /// <param name="succeededContext"></param>
    /// <returns></returns>
    public bool TryGetSucceededContext(out TOk succeededContext)
    {
        succeededContext = default!;
        if (Succeeded) {
            succeededContext = _successContext;
            return true;
        }

        return false;
    }

    /// <summary>
    /// Gets the failed result context.
    /// </summary>
    /// <param name="failedContext"></param>
    /// <returns></returns>
    public bool TryGetFailedContext(out TFailure failedContext)
    {
        failedContext = default!;
        if (Failed) {
            failedContext = _failureContext;
            return true;
        }

        return false;
    }

    #endregion ----------------------------------------------------- methods


    #region overrides

    /// <summary>
    /// Implicitly converts <see cref="Result{TOk,TFailure}"/> to <see cref="bool"/>.
    /// </summary>
    /// <param name="result"></param>
    public static implicit operator bool(Result<TOk, TFailure> result) => result?.Succeeded ?? false;

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != GetType()) {
            return false;
        }

        var other = (Result<TOk, TFailure>)obj;

        if (TryGetSucceededContext(out TOk sa) && TryGetSucceededContext(out var sb)) {
            return
                typeof(TOk).IsClass
                    ? sa?.Equals(sb) ?? (sb == null)
                    : sa!.Equals(sb);
        }

        if (TryGetFailedContext(out var fa) && TryGetFailedContext(out var fb)) {
            return
                typeof(TFailure).IsClass
                    ? fa?.Equals(fb) ?? (fb == null)
                    : fa!.Equals(fb);
        }

        return false;
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        var success = 97 *
            (typeof(TOk).IsClass
                ? _successContext?.GetHashCode() ?? 1
                : _successContext!.GetHashCode());

        var failure = 29 *
            (typeof(TOk).IsClass
                ? _failureContext?.GetHashCode() ?? 29
                : _failureContext!.GetHashCode());

        return Succeeded.GetHashCode() + success + failure * 13;
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return Succeeded
            ? $"Success: {_successContext}"
            : $"Failed: {_failureContext}";
    }

    #endregion ----------------------------------------------------- overrides
}


/// <summary>
/// Result class with a generic succeeded context and string error context.
/// </summary>
/// <typeparam name="TOk"></typeparam>
public class Result_<TOk> : IResult<TOk, string>
{
    #region ctor

    /// <summary>
    /// Initialize a new instance of the <see cref="Result{ TOk }"/> class.
    /// </summary>
    /// <param name="internalResult"></param>
    protected Result(Result<TOk, string> internalResult)
    {
        InternalResult = internalResult;
    }

    protected Result(Result<TOk, string> internalResult, Exception? exception)
    {
        InternalResult = internalResult;
        Exception = exception;
    }

    #endregion ----------------------------------------------------- ctor


    #region properties 

    /// <summary>
    /// <see cref="Error"/> when failed.
    /// </summary>
    public string Error
    {
        get {
            if (TryGetFailedContext(out var res)) {
                return res;
            }

            return "";
        }
    }

    /// <summary>
    /// <see cref="Result{TOk,TFailure}"/>.
    /// </summary>
    public Result<TOk, string> InternalResult { get; }

    /// <summary>
    /// See also <see cref="AggregateExceptionMessages(Exception)"/>.
    /// </summary>
    public Exception? Exception { get; }

    public bool IsException => Exception is not null;

    #endregion ----------------------------------------------------- properties


    #region IResult 

    /// <inheritdoc />
    public bool Succeeded => InternalResult.Succeeded;

    /// <inheritdoc />
    public bool Failed => InternalResult.Failed;

    /// <inheritdoc />
    /// <summary>
    /// Gets succeeded context. If <see cref="Result{TOk,TFailure}"/> failed then throws <see cref="ResultContextAccessException"/>.
    /// </summary>
    /// <exception cref="ResultContextAccessException"></exception>
    public TOk SuccessContext
    {
        get {
            if (TryGetSucceededContext(out var success)) {
                return success;
            }

            throw new ResultContextAccessException($"Result is failed: {Error}");
        }
    }

    /// <inheritdoc />
    public string FailedContext => InternalResult.FailedContext;

    /// <inheritdoc />
    public bool TryGetSucceededContext(out TOk succeededContext)
        => InternalResult.TryGetSucceededContext(out succeededContext);

    /// <inheritdoc />
    public bool TryGetFailedContext(out string failedContext)
    {
        var res = InternalResult.TryGetFailedContext(out failedContext);

        if (failedContext == null) {
            failedContext = res ? "Failed." : "";
        }

        return res;
    }

    #endregion ----------------------------------------------------- IResult


    #region static factory methods

    /// <summary>
    /// Creates succeeded <see cref="Result{TOk}"/>
    /// </summary>
    /// <param name="successContext"></param>
    /// <returns></returns>
    public static Result<TOk> Success(TOk successContext) => new(Result<TOk, string>.Success(successContext));

    /// <summary>
    /// Creates failed <see cref="Result{TOk}"/>
    /// </summary>
    /// <returns></returns>
    public static Result<TOk> Failure() => new(Result<TOk, string>.Failure(""));

    /// <summary>
    /// Creates failed <see cref="Result{TOk}"/>
    /// </summary>
    /// <param name="errors"></param>
    /// <returns></returns>
    public static Result<TOk> Failure(IEnumerable<string> errors)
        => new(Result<TOk, string>.Failure(string.Join(Environment.NewLine, errors ?? new string[0])));

    /// <summary>
    /// Creates failed <see cref="Result{TOk}"/>
    /// </summary>
    /// <param name="error"></param>
    /// <returns></returns>
    public static Result<TOk> Failure(string error) => new(Result<TOk, string>.Failure(error));

    /// <summary>
    /// Creates failed <see cref="Result{TOk}"/>
    /// </summary>
    /// <param name="ex"></param>
    /// <returns></returns>
    public static Result<TOk> Failure(Exception? ex)
    {
        return new Result<TOk>(Result<TOk, string>.Failure(ex?.Message ?? ""), ex);
    }

    /// <summary>
    /// Creates failed <see cref="Result{TOk}"/>
    /// </summary>
    /// <param name="ex"></param>
    /// <returns></returns>
    public static Result<TOk> Failure(string message, Exception? ex)
    {
        return new Result<TOk>(Result<TOk, string>.Failure(message), ex);
    }

    /// <summary>
    /// Creates failed <see cref="Result{TOk}"/> from <see cref="Result"/>.
    /// </summary>
    /// <param name="result"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"> If <paramref name="result"/> is succeeded. </exception>
    public static Result<TOk> Failure(Result result)
    {
        if (result.Succeeded)
        {
            throw new InvalidOperationException("Result is succeeded");
        }

        return new Result<TOk>(Result<TOk, string>.Failure(result.FailedContext), result.Exception);
    }

    /// <summary>
    /// Creates failed <see cref="Result{TOk}"/> from another failed 
    /// <see cref="Result{TSuccessSource}"/> with different <see cref="SuccessContext"/>.
    /// </summary>
    /// <typeparam name="TSuccessSource"></typeparam>
    /// <param name="result"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static Result<TOk> Failure<TSuccessSource>(Result<TSuccessSource> result)
    {
        if (result.Succeeded)
        {
            throw new InvalidOperationException("Result is succeeded");
        }

        return new Result<TOk>(Result<TOk, string>.Failure(result.FailedContext), result.Exception);
    }


    #endregion ----------------------------------------------------- static factory methods

    public static string AggregateExceptionMessages(Exception exception)
    {
        var errors = new StringBuilder();

        if (exception is AggregateException ae) {
            foreach (Exception e in ae.Flatten().InnerExceptions) {
                AddExceptionMessages(e);
            }
        }
        else {
            AddExceptionMessages(exception);
        }

        errors.AppendLine(exception.StackTrace);

        return errors.ToString();


        void AddExceptionMessages(Exception e)
        {
            errors.AppendLine(e.Message);
            if (e.InnerException != null) {
                errors.Append(e.InnerException.Message);
            }
        }
    }


    #region overrides

    /// <summary>
    /// 
    /// </summary>
    /// <param name="result"></param>
    public static implicit operator Result<TOk, string>(Result<TOk> result) => result.InternalResult;

    /// <inheritdoc />
    public override string ToString()
    {
        if (Succeeded) {
            return $"Succeeded. {SuccessContext}";
        }

        if (IsException) {
            string exceptionMessages = AggregateExceptionMessages(Exception!);
            return $"Failed. {FailedContext}\n{exceptionMessages}";
        }

        return $"Failed. {FailedContext}";
    }

    public override bool Equals(object? obj)
    {
        if (obj == null!) return false;
        return obj is Result<TOk> other && InternalResult.Equals(other.InternalResult);
    }

    public override int GetHashCode()
    {
        return InternalResult.GetHashCode();
    }


    /// <summary>
    /// Implicitly converts <see cref="Result{TOk,TFailure}"/> to <see cref="bool"/>.
    /// </summary>
    /// <param name="result"></param>
    public static implicit operator bool(Result<TOk> result) => result.Succeeded;

    #endregion ----------------------------------------------------- overrides
}


/// <summary>
/// <see cref="Result{TOk}"/> class with <see cref="Unit"/> succeeded context and <see cref="T:string[]"/> failed context.
/// </summary>
public class Result_ : Result< Unit >
{
    #region ctor

    /// <summary>
    /// Initialize a new instance of the <see cref="Result"/> class.
    /// </summary>
    /// <param name="internalResult"></param>
    protected Result(Result<Unit, string> internalResult)
        : base(internalResult)
    { }


    protected Result(Result<Unit, string> internalResult, Exception? exception)
        : base(internalResult, exception)
    { }

    #endregion ───────────────────────────────────────────────────── ctor ─┘


    #region static factory methods

    /// <summary>
    /// Creates succeeded <see cref="Result"/>
    /// </summary>
    /// <returns></returns>
    public static Result Success() => new( Result< Unit, string >.Success( new ValueTuple() ) );

    public static Result<TOk> Success<TOk>( TOk success ) => Result<TOk>.Success( success );

    /// <summary>
    /// Creates failed <see cref="Result"/>.
    /// </summary>
    /// <returns></returns>
    public new static Result Failure() => new( Result< Unit, string >.Failure( "" ) );

    /// <summary>
    /// Creates failed <see cref="Result"/> from <see cref="Result{TOk}"/>.
    /// </summary>
    /// <typeparam name="_"></typeparam>
    /// <param name="result"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"> If <paramref name="result"/> is succeeded. </exception>
    public static new Result Failure<_>(Result<_> result)
    {
        if (result.Succeeded)
        {
            throw new InvalidOperationException("Result is succeeded");
        }

        return new Result(Result<Unit, string>.Failure(result.FailedContext), result.Exception);
    }


    /// <summary>
    /// Creates failed <see cref="Result"/>.
    /// </summary>
    /// <param name="error"></param>
    /// <returns></returns>
    public new static Result Failure( string error ) => new( Result< Unit, string >.Failure( error ) );

    /// <summary>
    /// Creates failed <see cref="Result"/>.
    /// </summary>
    /// <param name="error"></param>
    /// <returns></returns>
    public static Result<TOk> Failure<TOk>( string error ) => Result<TOk>.Failure( error );

    /// <summary>
    /// Creates failed <see cref="Result"/>.
    /// </summary>
    /// <param name="errors"></param>
    /// <returns></returns>
    public new static Result Failure( IEnumerable<string> errors ) 
        => new( Result< ValueTuple, string >.Failure( string.Join( Environment.NewLine, errors ?? new string[0] ) ) );

    /// <summary>
    /// Creates failed <see cref="Result"/>
    /// </summary>
    /// <param name="ex"></param>
    /// <returns></returns>
    public new static Result Failure(Exception? ex)
    {
        return new Result(Result<Unit, string>.Failure(ex?.Message ?? ""), ex);
    }

    /// <summary>
    /// Creates failed <see cref="Result{TOk}"/>
    /// </summary>
    /// <param name="ex"></param>
    /// <returns></returns>
    public new static Result Failure(string message, Exception? ex)
    {
        return new Result(Result<Unit, string>.Failure(message), ex);
    }

    #endregion ----------------------------------------------------- static factory methods


    /// <summary>
    /// Implicitly converts <see cref="Result{TOk,TFailure}"/> to <see cref="bool"/>.
    /// </summary>
    /// <param name="result"></param>
    public static implicit operator bool( Result result ) => result?.Succeeded ?? false;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="result"></param>
    public static implicit operator Result<Unit, string>( Result result ) => result.InternalResult;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="result"></param>
    public static explicit operator Result( Result<Unit, string> result ) => new( result );


    /// <inheritdoc />
    public override string ToString()
    {
        if (Succeeded) {
            return $"Succeeded.";
        }

        if (IsException) {
            string exceptionMessages = AggregateExceptionMessages(Exception!);
            return $"Failed. {FailedContext}\n{exceptionMessages}";
        }

        return $"Failed. {FailedContext}";
    }
}
*/