using System;
using System.Threading.Tasks;

// ReSharper disable InconsistentNaming

namespace p1eXu5.Result.Extensions
{
    using Generic;

    /// <summary>
    /// TaskResult extensions.
    /// </summary>
    public static class TaskResultExtensions
    {
        /// <summary>
        /// TaskResult applicative functor.
        /// </summary>
        /// <typeparam name="TSuccessA"></typeparam>
        /// <typeparam name="TSuccessB"></typeparam>
        /// <typeparam name="__"></typeparam>
        /// <param name="task"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public static async Task< Result< TSuccessB, __ > > Apply<TSuccessA, TSuccessB, __>( 
            this Task< Result< TSuccessA, __ > > task, 
            Task< Result< Func<TSuccessA, TSuccessB>, __ > > f )
        {
            var fResult = await f;
            var aResult = await task;

            var isFResultSuccess = fResult.TryGetSucceededContext( out var fc );
            var isAResultSuccess = aResult.TryGetSucceededContext( out var ac );

            if ( isFResultSuccess && isAResultSuccess ) {
                return Result<TSuccessB, __>.Success( fc.Invoke( ac ) );
            }

            if ( isFResultSuccess ) {
                return Result<TSuccessB, __>.Failure( aResult.FailedContext );
            }

            if ( isAResultSuccess ) {
                return Result<TSuccessB, __>.Failure( fResult.FailedContext );
            }

            return Result<TSuccessB, __>.Failure( fResult.FailedContext );
        }

        public static async Task< Result< TSuccessB, __ > > Map<TSuccessA, TSuccessB, __>( 
            this Task< Result< TSuccessA, __ > > task, 
            Func<TSuccessA, TSuccessB> f )
        {
            var aResult = await task;

            return 
                aResult.Map( f );
        }


        public static async Task< Result<TS11>> MapFlat<TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9, TS10, TS11>(
            this Task<Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9, TS10)>> result, Func<TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9, TS10, Result<TS11>> f)
        {
            var res = await result;
            return res.MapFlat( f );
        }


        public static async Task< Result< TSuccessA, TError > > Filter<TSuccessA, TError >( 
            this Task< Result< TSuccessA, TError > > task, Predicate< TSuccessA> filter, TError defaultError )
        {
            var result = await task;
            return result.Filter( filter, defaultError );
        }

        public static async Task< Result< TSuccessA, TError > > FilterError<TSuccessA, TError >( 
            this Task< Result< TSuccessA, TError > > task, Predicate< TError> filter, TSuccessA defaultError )
        {
            var result = await task;
            return result.FilterError( filter, defaultError );
        }

        public static async Task< Result< _,TErrorB >> MapError<_, TErrorA, TErrorB>( this Task< Result< _, TErrorA >> task, Func< TErrorA, TErrorB > f )
        {
            var result = await task;
            return result.MapError( f );
        }

        public static async Task< Result< TSuccessB,TErrorB > > Bimap<TSuccessA, TSuccessB, TErrorA, TErrorB>( 
            this Task< Result< TSuccessA, TErrorA > > task, Func< TSuccessA, TSuccessB > fs, Func< TErrorA, TErrorB > fe )
        {
            var result = await task;
            return result.Bimap( fs, fe );
        }


        #region Retn

        /// <summary>
        /// Retn for TaskResult.
        /// </summary>
        /// <typeparam name="_"></typeparam>
        /// <param name="succeededContext"></param>
        /// <returns></returns>
        public static Task<Result<_, ValueTuple>> Retn<_>( this _ succeededContext )
            => Task.FromResult( Result<_, ValueTuple>.Success( succeededContext ) );

        /// <summary>
        /// Retn for TaskResult.
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static Task<Result<TSuccess, TError>> Retn<TSuccess, TError>( this Result<TSuccess, TError> result )
            => Task.FromResult( result );

        /// <summary>
        /// Retn for TaskResult.
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static Task<Result<TSuccess>> Retn<TSuccess>( this Result<TSuccess> result )
            => Task.FromResult( result );

        #endregion ---------------------------------------------------------- Retn


        /// <summary>
        /// TaskResult monad.
        /// </summary>
        /// <typeparam name="TSuccessA"></typeparam>
        /// <typeparam name="TSuccessB"></typeparam>
        /// <typeparam name="__"></typeparam>
        /// <param name="task"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public static async Task< Result< TSuccessB, __ > > Bind<TSuccessA, TSuccessB, __>( 
            this Task< Result< TSuccessA, __ > > task, 
            Func< TSuccessA, Task< Result< TSuccessB, __ >>> f )
        {
            var result = await task;

            if (result.TryGetSucceededContext( out var sc )) {
                return await f( sc );
            }

            return Result< TSuccessB, __ >.Failure( result.FailedContext );
        }


        public static async Task< Result<TSuccessB> > Bind<TSuccessA, TSuccessB>( this Task< Result<TSuccessA>> taskResult, Func<TSuccessA, Result<TSuccessB>> f)
        {
            var result = await taskResult;

            if (result.TryGetSucceededContext( out var sc )) {
                return f( sc );
            }

            return Result<TSuccessB>.Failure( result.FailedContext );
        }

        public static async Task< Result<TSuccessB> > BindV<TSuccessA, TSuccessB>( this ValueTask< Result<TSuccessA>> taskResult, Func<TSuccessA, Result<TSuccessB>> f)
        {
            var result = await taskResult;

            if (result.TryGetSucceededContext( out var sc )) {
                return f( sc );
            }

            return Result<TSuccessB>.Failure( result.FailedContext );
        }
        public static async Task< Result<TSuccessB> > BindV<TSuccessA, TSuccessB>( this ValueTask< Result<TSuccessA>> taskResult, Func<TSuccessA, ValueTask<Result<TSuccessB>>> f)
        {
            var result = await taskResult;

            if (result.TryGetSucceededContext( out var sc )) {
                return await f( sc );
            }

            return Result<TSuccessB>.Failure( result.FailedContext );
        }


        public static async ValueTask< Result<TSuccessB> > BindV<TSuccessA, TSuccessB>( this ValueTask< Result<TSuccessA>> taskResult, Func<TSuccessA, Task< Result<TSuccessB>>> f)
        {
            var result = await taskResult;

            if (result.TryGetSucceededContext( out var sc )) {
                return await f( sc );
            }

            return Result<TSuccessB>.Failure( result.FailedContext );
        }

        public static async Task< Result< TSuccessB > > Bind<TSuccessA, TSuccessB>( this Task< Result<TSuccessA>> taskResult, Func<TSuccessA, ValueTask<TSuccessB>> f)
        {
            var result = await taskResult;

            if (result.TryGetSucceededContext( out var sc )) {
                return (await f( sc )).ToResult();
            }

            return Result<TSuccessB>.Failure( result.FailedContext );
        }

        public static async Task< Result > Bind( 
            this Task< Result > task, 
            Func< Task< Result >> f )
        {
            var result = await task;

            if (result.Succeeded) {
                return await f();
            }

            return result;
        }


        public static async Task< Result<TSuccessB> > Bind< TSuccessA, TSuccessB >( 
            this Task< Result<TSuccessA> > task, 
            Func< TSuccessA, Task< Result<TSuccessB> >> f )
        {
            var result = await task;

            if (result.TryGetSucceededContext( out var sc )) {
                return await f( sc );
            }

            return Result<TSuccessB>.Failure( result.FailedContext );
        }


        public static async Task< Result< TSuccessB, TErrorB > > Bind<TSuccessA, TSuccessB, TErrorA, TErrorB>( 
            this Task< Result< TSuccessA, TErrorA > > task, 
            Func< TSuccessA, Task< Result< TSuccessB, TErrorB >>> f, Func< TErrorA, TErrorB> fe )
        {
            var result = await task;

            if (result.TryGetSucceededContext( out var sc )) {
                return await f( sc );
            }

            return Result< TSuccessB, TErrorB >.Failure( fe(result.FailedContext) );
        }


        public static async Task< Result< TSuccessB, TErrorB > > Bibind<TSuccessA, TSuccessB, TErrorA, TErrorB>( 
            this Task< Result< TSuccessA, TErrorA > > task, 
            Func< TSuccessA, Task< Result< TSuccessB, TErrorB >>> fs, Func< TErrorA, Task< Result< TSuccessB, TErrorB >>> fe )
        {
            var result = await task;

            if (result.TryGetSucceededContext( out var sc )) {
                return await fs( sc );
            }

            return await fe(result.FailedContext);
        }


        public static async Task< Result > Bind<TSuccessA>( 
            this Task< Result< TSuccessA, string > > task, 
            Func< TSuccessA, Task< Result>> f )
        {
            var result = await task;

            if (result.TryGetSucceededContext( out var sc )) {
                return await f( sc );
            }

            return Result.Failure( result.FailedContext );
        }


        public static async Task< Result< _, TErrorB > > BindError<_, TErrorA, TErrorB>( 
            this Task< Result< _, TErrorA > > task, 
            Func< TErrorA, Task< Result< _, TErrorB >>> f )
        {
            var result = await task;

            if (result.TryGetFailedContext( out var ec )) {
                return await f( ec );
            }

            return Result< _, TErrorB >.Success( result.SuccessContext );
        }
    }
}
