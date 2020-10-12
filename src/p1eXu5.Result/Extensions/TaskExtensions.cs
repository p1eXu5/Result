using System;
using System.Threading.Tasks;

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
    }
}
