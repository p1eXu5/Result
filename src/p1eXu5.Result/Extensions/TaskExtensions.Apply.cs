#nullable enable

namespace p1eXu5.Result.Extensions;

public static partial class TaskExtensions
{
    public static async Task<TContextB> TaskApply<TContextA, TContextB>(this Task<TContextA> task, Task<Func<TContextA, TContextB>> f)
    {
        var fResult = await f;
        var aResult = await task;
        return fResult(aResult);
    }


}


