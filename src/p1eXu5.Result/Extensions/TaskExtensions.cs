using System;
using System.Threading;
using System.Threading.Tasks;
using p1eXu5.Result.Generic;

// ReSharper disable once CheckNamespace
namespace p1eXu5.Result.Extensions
{
    public static class TaskExtensions
    {
        public static async Task< TContextB > TaskApply<TContextA, TContextB>( this Task< TContextA > task, Task< Func< TContextA, TContextB> > f )
        {
            var fResult = await f;
            var aResult = await task;
            return fResult( aResult);
        }

        public static async Task< TContextB > TaskMap<TContextA, TContextB>( this Task< TContextA > task, Func< TContextA, TContextB> f )
        {
            var aResult = await task;
            return f( aResult );
        }




        public static async Task< Result< TContext >> ToTaskResult< TContext >( this Task<TContext> task, CancellationToken cancellationToken )
        {
            if ( task.IsCanceled ) {
                return Result< TContext >.Failure( "Task was canceled." );
            }

            if ( task.IsFaulted ) {
                return Result< TContext >.Failure( task.Exception!.Flatten() );
            }

            try {
                var result = await task;
                return Result< TContext >.Success( result );
            }
            catch (Exception ex) {
                return Result< TContext >.Failure( ex );
            }
        }

        public static async Task< Result> ToTaskResult( this Task task, CancellationToken cancellationToken )
        {
            if ( task.IsCanceled ) {
                return Result.Failure( "Task was canceled." );
            }

            if ( task.IsFaulted ) {
                return Result.Failure( task.Exception!.Flatten() );
            }

            return await task.ContinueWith( 
                t => {
                    if (t.IsCompleted) {

                        return Result.Success();
                    }
                    else if (t.IsFaulted) {
                        return Result.Failure( t.Exception!.Flatten() );
                    }
                    else {
                        return Result.Failure( "Task was canceled." );
                    }
                }, cancellationToken);
        }
    }
}
