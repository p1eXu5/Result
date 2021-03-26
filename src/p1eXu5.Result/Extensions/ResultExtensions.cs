using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace p1eXu5.Result.Extensions
{
    using Generic;
    using Unit = ValueTuple;


    /// <summary>
    /// <see cref="Result{TSuccess, TFailure}"/> extensions.
    /// </summary>
    public static class ResultExtensions
    {
        public static Result< Unit, int > ToSuccessResult( this int result )
            => Result< Unit, int >.Success( new Unit() );

        public static Result< Unit, int > ToFiledResult( this int result )
            => Result< Unit, int >.Failure( result );

        public static Result< Unit, int > ToResult( this int result, Predicate< int > isSuccessful )
            => 
                isSuccessful( result )
                    ? result.ToSuccessResult()
                    : result.ToFiledResult();

        public static Result< TSuccess > ToResult< TSuccess >( this TSuccess success )
            => Result< TSuccess >.Success( success );


        #region bind 

        /// <summary>
        /// <see cref="Result{TSuccess,TFailure}"/> monad.
        /// </summary>
        /// <typeparam name="_"></typeparam>
        /// <typeparam name="__"></typeparam>
        /// <param name="result"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public static Result<_, __> Bind<_, __>(this Result<_, __> result, Func<_, Result<_, __>> f)
        {
            if (result.TryGetSucceededContext(out var sc))
            {
                return f(sc);
            }

            return result;
        }

        public static Result<TSuccessB> Bind<TSuccessA, TSuccessB>(this Result<TSuccessA> result, Func<TSuccessA, Result<TSuccessB>> f)
        {
            if (result.TryGetSucceededContext(out var sc))
            {
                return f(sc);
            }

            return Result.Failure<TSuccessB>(result.FailedContext);
        }

        #endregion ----------------------------------------------------- bind


        #region map 

        /// <summary>
        /// <see cref="Result{TSuccess,TFailure}"/> functor.
        /// </summary>
        /// <typeparam name="TSuccessA"></typeparam>
        /// <typeparam name="TSuccessB"></typeparam>
        /// <typeparam name="__"></typeparam>
        /// <param name="result"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public static Result<TSuccessB, __> Map<TSuccessA, TSuccessB, __>(this Result<TSuccessA, __> result, Func<TSuccessA, TSuccessB> f)
        {
            if (result.TryGetSucceededContext(out var sc))
            {
                return Result<TSuccessB, __>.Success(f(sc));
            }

            return Result<TSuccessB, __>.Failure(result.FailedContext);
        }


        /// <summary>
        /// <see cref="Result{TSuccess}"/> functor.
        /// </summary>
        /// <typeparam name="TSuccessA"></typeparam>
        /// <typeparam name="TSuccessB"></typeparam>
        /// <param name="result"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public static Result<TSuccessB> Map<TSuccessA, TSuccessB>(this Result<TSuccessA> result, Func<TSuccessA, TSuccessB> f)
        {
            if (result.TryGetSucceededContext(out var sc))
            {
                return Result<TSuccessB>.Success(f(sc));
            }

            return Result<TSuccessB>.Failure(result.FailedContext);
        }



        /// <summary>
        /// <see cref="Result{TSuccess,TFailure}"/> error functor.
        /// </summary>
        /// <typeparam name="_"></typeparam>
        /// <typeparam name="TErrorA"></typeparam>
        /// <typeparam name="TErrorB"></typeparam>
        /// <param name="result"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public static Result<_, TErrorB> MapError<_, TErrorA, TErrorB>(this Result<_, TErrorA> result, Func<TErrorA, TErrorB> f)
        {
            if (result.TryGetFailedContext(out var fc))
            {
                return Result<_, TErrorB>.Failure(f(fc));
            }

            return Result<_, TErrorB>.Success(result.SuccessContext);
        }

        public static Result<TSuccessB, TErrorB> Bimap<TSuccessA, TSuccessB, TErrorA, TErrorB>(this Result<TSuccessA, TErrorA> result, Func<TSuccessA, TSuccessB> fs, Func<TErrorA, TErrorB> fe)
        {

            if (result.TryGetSucceededContext(out var s))
            {
                return Result<TSuccessB, TErrorB>.Success(fs(s));
            }

            return Result<TSuccessB, TErrorB>.Failure(fe(result.FailedContext));
        }

        #endregion ----------------------------------------------------- map


        #region filter 

        public static Result<TSuccessA, TError> Filter<TSuccessA, TError>(
            this Result<TSuccessA, TError> result, Predicate<TSuccessA> filter, TError defaultError)
        {

            if (result.TryGetSucceededContext(out var s))
            {
                return
                    filter(s)
                        ? result
                        : Result<TSuccessA, TError>.Failure(defaultError);
            }

            return result;
        }


        public static Result<TSuccessA, TError> FilterError<TSuccessA, TError>(
            this Result<TSuccessA, TError> result, Predicate<TError> filter, TSuccessA defaultError)
        {

            if (result.TryGetFailedContext(out var f))
            {
                return
                    filter(f)
                        ? result
                        : Result<TSuccessA, TError>.Success(defaultError);
            }

            return result;
        }

        #endregion ----------------------------------------------------- filter


        public static Result< ICollection<TSuccessB> > TraverseM<TSuccessA,TSuccessB>( this ICollection<TSuccessA> elems, Func<TSuccessA, Result<TSuccessB>> f)
        {
            ICollection<TSuccessB> list = new List<TSuccessB>();
            Result<ICollection<TSuccessB> > res = list.ToResult();

            foreach ( var elem in elems ) {
                res = f( elem )
                    .Map( r => {
                        list.Add( r );
                        return list;
                    } );
                if ( res.Failed ) {
                    return res;
                }
            }
            
            return res;
        }


        /// <summary>
        /// <see cref="Result{TSuccess,TFailure}"/> traverse.
        /// </summary>
        /// <typeparam name="TSuccessA"></typeparam>
        /// <typeparam name="TSuccessB"></typeparam>
        /// <typeparam name="__"></typeparam>
        /// <param name="result"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public static Task< Result< TSuccessB,__ >> TraverseTask< TSuccessA, TSuccessB, __ >( this Result< TSuccessA, __ > result, Func< TSuccessA, Task< TSuccessB>> f)
        {
            if ( result.TryGetSucceededContext( out var sc ) ) {

                var fTaskResult = Task.FromResult( new Func<TSuccessB, Result<TSuccessB,__>>( Result<TSuccessB,__>.Success ) );
                var fB = f(sc);

                return fB.TaskApply( fTaskResult );
            }

            return Task.FromResult( Result<TSuccessB,__>.Failure( result.FailedContext ) );
        }

        /// <summary>
        /// <see cref="Result{TSuccess,TFailure}"/> traverse.
        /// </summary>
        /// <typeparam name="_"></typeparam>
        /// <typeparam name="__"></typeparam>
        /// <param name="result"></param>
        /// <returns></returns>
        public static Task< Result< _, __ >> TraverseTask< _, __ >( this Result< _, __ > result)
        {
            if ( result.TryGetSucceededContext( out var sc ) ) {

                var fTaskResult = Task.FromResult( new Func<_, Result<_, __>>( Result<_, __>.Success ) );
                var fB = Task.FromResult( sc );

                return fB.TaskApply( fTaskResult );
            }

            return Task.FromResult( result );
        }


        #region iter 

        public static Result<TSuccess> Iter<TSuccess>(this Result<TSuccess> result, Action<TSuccess> action)
        {
            if (result.TryGetSucceededContext(out TSuccess success))
            {
                action(success);
            }

            return result;
        }


        public static async ValueTask<Result<TSuccess>> IterAsync<TSuccess>(this Result<TSuccess> result, Func<TSuccess, ValueTask> actionAsync)
        {
            if (result.TryGetSucceededContext(out TSuccess success))
            {
                await actionAsync(success);
            }

            return result;
        }

        #endregion ----------------------------------------------------- iter

    }
}
