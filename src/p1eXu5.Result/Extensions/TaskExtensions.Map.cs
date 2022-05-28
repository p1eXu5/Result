#nullable enable

namespace p1eXu5.Result.Extensions;

public static partial class TaskExtensions
{
    public static async Task<TContextB> TaskMap<TContextA, TContextB>(this Task<TContextA> task, Func<TContextA, TContextB> f)
    {
        var aResult = await task;
        return f(aResult);
    }
}