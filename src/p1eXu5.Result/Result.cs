using System;
using System.Collections.Generic;
using System.Linq;

namespace p1eXu5.Result
{
    using Generic;

    using Unit = ValueTuple;


    /// <summary>
    /// <see cref="Result{TSuccess}"/> class with <see cref="Unit"/> succeeded context and <see cref="T:string[]"/> failed context.
    /// </summary>
    public class Result : Result< Unit >
    {
        /// <summary>
        /// Initialize a new instance of the <see cref="Result"/> class.
        /// </summary>
        /// <param name="internalResult"></param>
        protected Result( Result< Unit, string > internalResult )
            : base( internalResult )
        { }


        #region static factory methods
        
        /// <summary>
        /// Creates succeeded <see cref="Result"/>
        /// </summary>
        /// <returns></returns>
        public static Result Success() => new Result( Result< Unit, string >.Success( new ValueTuple() ) );

        public static Result<TSuccess> Success<TSuccess>( TSuccess success ) => Result<TSuccess>.Success( success );

        /// <summary>
        /// Creates failed <see cref="Result"/>.
        /// </summary>
        /// <returns></returns>
        public new static Result Failure() => new Result( Result< Unit, string >.Failure( "" ) );


        /// <summary>
        /// Creates failed <see cref="Result"/>.
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public new static Result Failure( string error ) => new Result( Result< Unit, string >.Failure( error ) );

        /// <summary>
        /// Creates failed <see cref="Result"/>.
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public static Result<TSuccess> Failure<TSuccess>( string error ) => Result<TSuccess>.Failure( error );

        /// <summary>
        /// Creates failed <see cref="Result"/>.
        /// </summary>
        /// <param name="errors"></param>
        /// <returns></returns>
        public new static Result Failure( IEnumerable<string> errors ) 
            => new Result( Result< ValueTuple, string >.Failure( string.Join( Environment.NewLine, errors ?? new string[0] ) ) );

        /// <summary>
        /// Creates failed <see cref="Result{TSuccess}"/>
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public new static Result Failure( Exception ex )
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

            return Result.Failure( errors );


            void AddExceptionMessages( Exception e )
            {
                errors.Add( e.Message );
                if (e.InnerException != null) {
                    errors.Add( e.InnerException.Message );
                }

            }
        }

        #endregion ----------------------------------------------------- static factory methods
        

        /// <summary>
        /// Implicitly converts <see cref="Result{TSuccess,TFailure}"/> to <see cref="bool"/>.
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
        public static explicit operator Result( Result<Unit, string> result ) => new Result( result );


        /// <inheritdoc />
        public override string ToString()
        {
            if (Error?.Any() == true) {
                string error = Error.Aggregate( "", ( acc, err ) => acc + err + "; " );
                return "Failed: " + error.Substring( 0, error.Length - 2 ) + "."; // cut "; "
            }

            return
                Succeeded ? "Succeeded." : "Failed.";
        }
    }
}
