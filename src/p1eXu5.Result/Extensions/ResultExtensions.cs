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


        public static Result<(TSuccessA, TSuccessB)> BindFlat<TSuccessA, TSuccessB>( this Result<TSuccessA> result, Func<Result<TSuccessB>> f)
        {
            if ( result.TryGetSucceededContext( out var scA) ) {
                var resultB = f();
                if ( resultB.TryGetSucceededContext( out var scB )) {
                    return Result<(TSuccessA, TSuccessB)>.Success( (scA, scB) );
                }

                return Result<(TSuccessA, TSuccessB)>.Failure(resultB.FailedContext);
            }

            return Result<(TSuccessA, TSuccessB)>.Failure(result.FailedContext);
        }

        public static Result<(TSuccessA, TSuccessB, TSuccessC)> BindFlat<TSuccessA, TSuccessB, TSuccessC>( this Result<(TSuccessA, TSuccessB)> result, Func<Result<TSuccessC>> f)
        {
            if ( result.TryGetSucceededContext( out var scA) ) {
                var resultB = f();
                if ( resultB.TryGetSucceededContext( out var sc )) {
                    return Result<(TSuccessA, TSuccessB, TSuccessC)>.Success( (scA.Item1, scA.Item2, sc) );
                }

                return Result<(TSuccessA, TSuccessB, TSuccessC)>.Failure(resultB.FailedContext);
            }

            return Result<(TSuccessA, TSuccessB, TSuccessC)>.Failure(result.FailedContext);
        }

        public static Result<(TSuccessA, TSuccessB, TSuccessC, TSuccessD)> BindFlat<TSuccessA, TSuccessB, TSuccessC, TSuccessD>( 
            this Result<(TSuccessA, TSuccessB, TSuccessC)> result, Func<Result<TSuccessD>> f)
        {
            if ( result.TryGetSucceededContext( out var scA) ) {
                var resultB = f();
                if ( resultB.TryGetSucceededContext( out var sc )) {
                    return Result<(TSuccessA, TSuccessB, TSuccessC, TSuccessD)>.Success( (scA.Item1, scA.Item2, scA.Item3, sc) );
                }

                return Result<(TSuccessA, TSuccessB, TSuccessC, TSuccessD)>.Failure(resultB.FailedContext);
            }

            return Result<(TSuccessA, TSuccessB, TSuccessC, TSuccessD)>.Failure(result.FailedContext);
        }

        public static Result<(TSuccessA, TSuccessB, TSuccessC, TSuccessD, TSuccessE)> BindFlat<TSuccessA, TSuccessB, TSuccessC, TSuccessD, TSuccessE>( 
            this Result<(TSuccessA, TSuccessB, TSuccessC, TSuccessD)> result, Func<Result<TSuccessE>> f)
        {
            if ( result.TryGetSucceededContext( out var scA) ) {
                var resultB = f();
                if ( resultB.TryGetSucceededContext( out var sc )) {
                    return Result<(TSuccessA, TSuccessB, TSuccessC, TSuccessD, TSuccessE)>.Success( (scA.Item1, scA.Item2, scA.Item3, scA.Item4, sc) );
                }

                return Result<(TSuccessA, TSuccessB, TSuccessC, TSuccessD, TSuccessE)>.Failure(resultB.FailedContext);
            }

            return Result<(TSuccessA, TSuccessB, TSuccessC, TSuccessD, TSuccessE)>.Failure(result.FailedContext);
        }

        public static Result<(TSuccessA, TSuccessB, TSuccessC, TSuccessD, TSuccessE, TSuccessF)> BindFlat<TSuccessA, TSuccessB, TSuccessC, TSuccessD, TSuccessE, TSuccessF>( 
            this Result<(TSuccessA, TSuccessB, TSuccessC, TSuccessD, TSuccessE)> result, Func<Result<TSuccessF>> f)
        {
            if ( result.TryGetSucceededContext( out var scA) ) {
                var resultB = f();
                if ( resultB.TryGetSucceededContext( out var sc )) {
                    return Result<(TSuccessA, TSuccessB, TSuccessC, TSuccessD, TSuccessE, TSuccessF)>.Success( (scA.Item1, scA.Item2, scA.Item3, scA.Item4, scA.Item5, sc) );
                }

                return Result<(TSuccessA, TSuccessB, TSuccessC, TSuccessD, TSuccessE, TSuccessF)>.Failure(resultB.FailedContext);
            }

            return Result<(TSuccessA, TSuccessB, TSuccessC, TSuccessD, TSuccessE, TSuccessF)>.Failure(result.FailedContext);
        }

        public static Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7)> BindFlat<TS1, TS2, TS3, TS4, TS5, TS6, TS7>( 
            this Result<(TS1, TS2, TS3, TS4, TS5, TS6)> result, Func<Result<TS7>> f)
        {
            if ( result.TryGetSucceededContext( out var scA) ) {
                var resultB = f();
                if ( resultB.TryGetSucceededContext( out var sc )) {
                    return Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7)>.Success( (scA.Item1, scA.Item2, scA.Item3, scA.Item4, scA.Item5, scA.Item6, sc) );
                }

                return Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7)>.Failure(resultB.FailedContext);
            }

            return Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7)>.Failure(result.FailedContext);
        }

        public static Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8)> BindFlat<TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8>( 
            this Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7)> result, Func<Result<TS8>> f)
        {
            if ( result.TryGetSucceededContext( out var scA) ) {
                var resultB = f();
                if ( resultB.TryGetSucceededContext( out var sc )) {
                    return Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8)>.Success( (scA.Item1, scA.Item2, scA.Item3, scA.Item4, scA.Item5, scA.Item6, scA.Item7, sc) );
                }

                return Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8)>.Failure(resultB.FailedContext);
            }

            return Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8)>.Failure(result.FailedContext);
        }

        public static Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9)> BindFlat<TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9>( 
            this Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8)> result, Func<Result<TS9>> f)
        {
            if ( result.TryGetSucceededContext( out var scA) ) {
                var resultB = f();
                if ( resultB.TryGetSucceededContext( out var sc )) {
                    return Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9)>.Success( 
                        (scA.Item1, scA.Item2, scA.Item3, scA.Item4, scA.Item5, scA.Item6, scA.Item7, scA.Item8, sc) );
                }

                return Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9)>.Failure(resultB.FailedContext);
            }

            return Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9)>.Failure(result.FailedContext);
        }

        public static Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9, TS10)> BindFlat<TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9, TS10>( 
            this Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9)> result, Func<Result<TS10>> f)
        {
            if ( result.TryGetSucceededContext( out var scA) ) {
                var resultB = f();
                if ( resultB.TryGetSucceededContext( out var sc )) {
                    return Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9, TS10)>.Success( 
                        (scA.Item1, scA.Item2, scA.Item3, scA.Item4, scA.Item5, scA.Item6, scA.Item7, scA.Item8, scA.Item9, sc) );
                }

                return Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9, TS10)>.Failure(resultB.FailedContext);
            }

            return Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9, TS10)>.Failure(result.FailedContext);
        }

        public static Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9, TS10, TS11)> BindFlat<TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9, TS10, TS11>( 
            this Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9, TS10)> result, Func<Result<TS11>> f)
        {
            if ( result.TryGetSucceededContext( out var scA) ) {
                var resultB = f();
                if ( resultB.TryGetSucceededContext( out var sc )) {
                    return Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9, TS10, TS11)>.Success( 
                        (scA.Item1, scA.Item2, scA.Item3, scA.Item4, scA.Item5, scA.Item6, scA.Item7, scA.Item8, scA.Item9, scA.Item10, sc) );
                }

                return Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9, TS10, TS11)>.Failure(resultB.FailedContext);
            }

            return Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9, TS10, TS11)>.Failure(result.FailedContext);
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

        public static Result<TS4> Map<TS1, TS2, TS3, TS4>(this Result<(TS1, TS2, TS3)> result, Func<TS1, TS2, TS3, TS4> f)
        {
            if (result.TryGetSucceededContext(out var sc))
            {
                return Result<TS4>.Success(f(sc.Item1, sc.Item2, sc.Item3));
            }

            return Result<TS4>.Failure(result.FailedContext);
        }

        public static Result<TS5> Map<TS1, TS2, TS3, TS4, TS5>(this Result<(TS1, TS2, TS3, TS4)> result, Func<TS1, TS2, TS3, TS4, TS5> f)
        {
            if (result.TryGetSucceededContext(out var sc))
            {
                return Result<TS5>.Success(f(sc.Item1, sc.Item2, sc.Item3, sc.Item4));
            }

            return Result<TS5>.Failure(result.FailedContext);
        }

        public static Result<TS11> Map<TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9, TS10, TS11>(
            this Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9, TS10)> result, Func<TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9, TS10, TS11> f)
        {
            if (result.TryGetSucceededContext(out var sc))
            {
                return Result<TS11>.Success(f(sc.Item1, sc.Item2, sc.Item3, sc.Item4, sc.Item5, sc.Item6, sc.Item7, sc.Item8, sc.Item9, sc.Item10));
            }

            return Result<TS11>.Failure(result.FailedContext);
        }

        public static Result<TS11> MapFlat<TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9, TS10, TS11>(
            this Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9, TS10)> result, Func<TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9, TS10, Result<TS11>> f)
        {
            if (result.TryGetSucceededContext(out var sc))
            {
                return f(sc.Item1, sc.Item2, sc.Item3, sc.Item4, sc.Item5, sc.Item6, sc.Item7, sc.Item8, sc.Item9, sc.Item10);
            }

            return Result<TS11>.Failure(result.FailedContext);
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


        public static async Task< Result<TSuccess>> SequenceTask< TSuccess >( this Result< Task<TSuccess>> result )
        {
            if ( result.TryGetSucceededContext( out var task ) ) {
                return (await task).ToResult();
            }

            return Result.Failure<TSuccess>( result.FailedContext );
        }


        public static async ValueTask< Result<TSuccess>> SequenceTask< TSuccess >( this Result< ValueTask<TSuccess>> result )
        {
            if ( result.TryGetSucceededContext( out var task ) ) {
                return (await task).ToResult();
            }

            return Result.Failure<TSuccess>( result.FailedContext );
        }


        public static Task<Result<TSuccessB>> MapTask<TSuccessA, TSuccessB>( this Result< TSuccessA > result, Func<TSuccessA, Task<Result<TSuccessB>>> f)
        {
            if ( result.TryGetSucceededContext( out var scA )) {
                return f(scA);
            }
            return Task.FromResult( Result<TSuccessB>.Failure(result.FailedContext) );
        }


        public static Task<Result> MapTask<TSuccessA>( this Result< TSuccessA > result, Func<TSuccessA, Task<Result>> f)
        {
            if ( result.TryGetSucceededContext( out var scA )) {
                return f(scA);
            }
            return Task.FromResult( Result.Failure(result.FailedContext) );
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
