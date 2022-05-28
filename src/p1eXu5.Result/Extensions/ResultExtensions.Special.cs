#nullable enable

namespace p1eXu5.Result.Extensions;

public static partial class ResultExtensions
{
    public static Result<ICollection<TSuccessB>> TraverseM<TSuccessA, TSuccessB>(this ICollection<TSuccessA> elems, Func<TSuccessA, Result<TSuccessB>> f)
    {
        ICollection<TSuccessB> list = new List<TSuccessB>(elems.Count);
        Result<ICollection<TSuccessB>> res = list.ToSuccessResult();

        foreach (var elem in elems)
        {
            res = f(elem)
                .Map(r => {
                    list.Add(r);
                    return list;
                });
            if (res.Failed)
            {
                return res;
            }
        }

        return res;
    }


    public static (Result<ICollection<TSuccessB>> result, IReadOnlyCollection<string> errors) TraverseA<TSuccessA, TSuccessB>(this ICollection<TSuccessA> elems, Func<TSuccessA, Result<TSuccessB>> f)
    {
        ICollection<TSuccessB> list = new List<TSuccessB>(elems.Count);
        Result<ICollection<TSuccessB>> res = list.ToFailedResult("Has no successful results.");

        List<string> errors = new List<string>(elems.Count);

        foreach (var elem in elems)
        {
            res = f(elem)
                .Map(r => {
                    list.Add(r);
                    return list;
                })
                .IterError(error =>
                {
                    errors.Add(error);
                    return list;
                });
        }

        return (res, errors);
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
    public static Task<Result<TSuccessB, __>> TraverseTask<TSuccessA, TSuccessB, __>(this Result<TSuccessA, __> result, Func<TSuccessA, Task<TSuccessB>> f)
    {
        if (result.TryGetSucceededContext(out var sc))
        {

            var fTaskResult = Task.FromResult(new Func<TSuccessB, Result<TSuccessB, __>>(Result<TSuccessB, __>.Success));
            var fB = f(sc);

            return fB.TaskApply(fTaskResult);
        }

        return Task.FromResult(Result<TSuccessB, __>.Failure(result.FailedContext));
    }

    /// <summary>
    /// <see cref="Result{TSuccess,TFailure}"/> traverse.
    /// </summary>
    /// <typeparam name="_"></typeparam>
    /// <typeparam name="__"></typeparam>
    /// <param name="result"></param>
    /// <returns></returns>
    public static Task<Result<_, __>> TraverseTask<_, __>(this Result<_, __> result)
    {
        if (result.TryGetSucceededContext(out var sc))
        {

            var fTaskResult = Task.FromResult(new Func<_, Result<_, __>>(Result<_, __>.Success));
            var fB = Task.FromResult(sc);

            return fB.TaskApply(fTaskResult);
        }

        return Task.FromResult(result);
    }


    public static async Task<Result<TSuccess>> SequenceTask<TSuccess>(this Result<Task<TSuccess>> result)
    {
        if (result.TryGetSucceededContext(out var task))
        {
            return (await task).ToSuccessResult();
        }

        return Result.Failure<TSuccess>(result.FailedContext);
    }


    public static async ValueTask<Result<TSuccess>> SequenceTask<TSuccess>(this Result<ValueTask<TSuccess>> result)
    {
        if (result.TryGetSucceededContext(out var task))
        {
            return (await task).ToSuccessResult();
        }

        return Result.Failure<TSuccess>(result.FailedContext);
    }
}
