using System;
using System.Collections.Generic;
using System.Linq;

namespace p1eXu5.Result.Generic
{
    using Exceptions;

    using Unit = ValueTuple;


    /// <summary>
    /// Result class with generic succeeded and failed contexts.
    /// </summary>
    /// <typeparam name="TSuccess"></typeparam>
    /// <typeparam name="TFailure"></typeparam>
    public class Result< TSuccess, TFailure > : IResult< TSuccess, TFailure >
    {
        #region fields

        private readonly TSuccess _successContext;
        private readonly TFailure _failureContext;

        #endregion ----------------------------------------------------- fields


        #region ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="Result{TSuccess,TFailure}"/> class
        /// with succeeded context.
        /// </summary>
        /// <param name="successContext"></param>
        protected Result( TSuccess successContext )
        {
            _successContext = successContext;
            Succeeded = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Result{TSuccess,TFailure}"/> class
        /// with failed context. 
        /// </summary>
        /// <param name="failureContext"></param>
        protected Result( TFailure failureContext )
        {
            _failureContext = failureContext;
            Succeeded = false;
        }

        #endregion ----------------------------------------------------- ctor


        #region properties

        /// <summary>
        /// Gets a value indicating whether the <see cref="Result{TSuccess,TFailure}"/> is succeeded.
        /// </summary>
        public bool Succeeded { get; }

        /// <summary>
        /// Gets a value indicating whether the <see cref="Result{TSuccess,TFailure}"/> is failed.
        /// </summary>
        public bool Failed => !Succeeded;

        /// <summary>
        /// Gets succeeded context. If <see cref="Result{TSuccess,TFailure}"/> failed then throws <see cref="ResultContextAccessException"/>.
        /// </summary>
        /// <exception cref="ResultContextAccessException"></exception>
        public TSuccess SuccessContext
        {
            get {
                if ( TryGetSucceededContext( out var success ) ) {
                    return success;
                }

                throw new ResultContextAccessException( "Result is failed." );
            }
        }

        /// <summary>
        /// Gets failed context. If <see cref="Result{TSuccess,TFailure}"/> failed then throws <see cref="ResultContextAccessException"/>.
        /// </summary>
        /// <exception cref="ResultContextAccessException"></exception>
        public TFailure FailedContext
        {
            get {
                if ( TryGetFailedContext( out var failure ) ) {
                    return failure;
                }

                throw new ResultContextAccessException( "Result is succeeded." );
            }
        }

        #endregion ----------------------------------------------------- properties


        #region static factory methods 

        /// <summary>
        /// Creates succeeded <see cref="Result{TSuccess,TFailure}"/>
        /// </summary>
        /// <param name="successContext"></param>
        /// <returns></returns>
        public static Result< TSuccess, TFailure > Success( TSuccess successContext ) => new Result< TSuccess, TFailure >( successContext );

        /// <summary>
        /// Creates failed <see cref="Result{TSuccess,TFailure}"/>
        /// </summary>
        /// <param name="failureContext"></param>
        /// <returns></returns>
        public static Result< TSuccess, TFailure > Failure( TFailure failureContext ) => new Result< TSuccess, TFailure >( failureContext );

        #endregion ----------------------------------------------------- static factory methods


        #region methods

        /// <summary>
        /// Gets the succeeded result context.
        /// </summary>
        /// <param name="succeededContext"></param>
        /// <returns></returns>
        public bool TryGetSucceededContext( out TSuccess succeededContext )
        {
            succeededContext = default;
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
        public bool TryGetFailedContext( out TFailure failedContext )
        {
            failedContext = default;
            if (Failed) {
                failedContext = _failureContext;
                return true;
            }

            return false;
        }

        #endregion ----------------------------------------------------- methods


        #region overrides

        /// <summary>
        /// Implicitly converts <see cref="Result{TSuccess,TFailure}"/> to <see cref="bool"/>.
        /// </summary>
        /// <param name="result"></param>
        public static implicit operator bool( Result<TSuccess, TFailure> result ) => result?.Succeeded ?? false;

        /// <inheritdoc />
        public override bool Equals( object obj )
        {
            if (obj == null || obj.GetType() != GetType()) {
                return false;
            }

            var other = ( Result<TSuccess, TFailure> )obj;

            if ( TryGetSucceededContext( out var sa ) && TryGetSucceededContext( out var sb ) ) {
                return 
                    typeof(TSuccess).IsClass
                        ? sa?.Equals( sb ) ?? (sb == null)
                        : sa.Equals( sb );
            }

            if ( TryGetFailedContext( out var fa ) && TryGetFailedContext( out var fb ) ) {
                return 
                    typeof(TFailure).IsClass
                        ? fa?.Equals( fb ) ?? (fb == null)
                        : fa.Equals( fb );
            }

            return false;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            var success = 97 * 
                (typeof( TSuccess ).IsClass
                    ? _successContext?.GetHashCode() ?? 1
                    : _successContext.GetHashCode());

            var failure = 29 *
                (typeof( TSuccess ).IsClass
                    ? _failureContext?.GetHashCode() ?? 29
                    : _failureContext.GetHashCode());

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
    /// <typeparam name="TSuccess"></typeparam>
    public class Result< TSuccess > : IResult< TSuccess, string >
    {

        #region ctor

        /// <summary>
        /// Initialize a new instance of the <see cref="Result{ TSuccess }"/> class.
        /// </summary>
        /// <param name="internalResult"></param>
        protected Result( Result<TSuccess, string> internalResult )
        {
            InternalResult = internalResult;
        }


        #endregion ----------------------------------------------------- ctor


        #region properties 

        /// <summary>
        /// <see cref="Error"/> when failed.
        /// </summary>
        public string Error
        {
            get {
                if (TryGetFailedContext( out var res )) {
                    return res;
                }

                return "";
            }
        }

        /// <summary>
        /// <see cref="Result{TSuccess,TFailure}"/>.
        /// </summary>
        public Result<TSuccess, string> InternalResult { get; }

        #endregion ----------------------------------------------------- properties


        #region IResult 

        /// <inheritdoc />
        public bool Succeeded => InternalResult.Succeeded;

        /// <inheritdoc />
        public bool Failed => InternalResult.Failed;

        /// <inheritdoc />
        public TSuccess SuccessContext => InternalResult.SuccessContext;

        /// <inheritdoc />
        public string FailedContext => InternalResult.FailedContext;

        /// <inheritdoc />
        public bool TryGetSucceededContext( out TSuccess succeededContext )
            => InternalResult.TryGetSucceededContext( out succeededContext );

        /// <inheritdoc />
        public bool TryGetFailedContext( out string failedContext )
        {
            var res = InternalResult.TryGetFailedContext( out failedContext );
            
            if ( failedContext == null ) {
                failedContext = res ? "Failed." : "";
            }

            return res;
        }

        #endregion ----------------------------------------------------- IResult


        #region static factory methods

        /// <summary>
        /// Creates succeeded <see cref="Result{TSuccess}"/>
        /// </summary>
        /// <param name="successContext"></param>
        /// <returns></returns>
        public static Result< TSuccess > Success( TSuccess successContext ) => new Result< TSuccess >( Result< TSuccess, string >.Success( successContext ) );

        /// <summary>
        /// Creates failed <see cref="Result{TSuccess}"/>
        /// </summary>
        /// <returns></returns>
        public static Result< TSuccess > Failure() => new Result< TSuccess >( Result< TSuccess, string >.Failure( "" ) );

        /// <summary>
        /// Creates failed <see cref="Result{TSuccess}"/>
        /// </summary>
        /// <param name="errors"></param>
        /// <returns></returns>
        public static Result< TSuccess > Failure( IEnumerable< string > errors ) 
            => new Result< TSuccess >( Result< TSuccess, string >.Failure( string.Join( Environment.NewLine, errors ?? new string[0] ) ) );

        /// <summary>
        /// Creates failed <see cref="Result{TSuccess}"/>
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public static Result< TSuccess > Failure( string error ) => new Result< TSuccess >( Result< TSuccess, string >.Failure( error ) );

        /// <summary>
        /// Creates failed <see cref="Result{TSuccess}"/>
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static Result< TSuccess > Failure( Exception ex )
        {
            var errors = new List<string>();

            if (ex is AggregateException ae) {
                foreach (Exception e in ae.Flatten().InnerExceptions) {
                    AddExceptionMessages( e );
                }
            }
            else {
                AddExceptionMessages( ex );
            }

            return Result< TSuccess >.Failure( errors );


            void AddExceptionMessages( Exception e )
            {
                errors.Add( e.Message );
                if (e.InnerException != null) {
                    errors.Add( e.InnerException.Message );
                }
            }
        }

        #endregion ----------------------------------------------------- static factory methods


        #region overrides

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        public static implicit operator Result<TSuccess, string>( Result<TSuccess> result ) => result.InternalResult;

        /// <inheritdoc />
        public override string ToString()
        {
            if (Error?.Any() == true) {
                string error = Error.Aggregate( "", ( acc, err ) => acc + err + "; " );
                return error.Substring( 0, error.Length - 2 ) + "."; // cut "; "
            }

            return
                Succeeded ? "Succeeded." : "Failed.";
        }

        public override bool Equals( object obj )
        {
            if ( obj == null ) return  false;
            return obj is Result< TSuccess > other && InternalResult.Equals( other.InternalResult );
        }

        public override int GetHashCode()
        {
            return InternalResult.GetHashCode();
        }

        #endregion ----------------------------------------------------- overrides
    }
}
