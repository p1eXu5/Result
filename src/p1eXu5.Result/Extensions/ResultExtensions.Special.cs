#nullable enable

namespace p1eXu5.Result.Extensions;

public static partial class ResultExtensions
{
    public static Result<ICollection<TOkB>, TError> TraverseM<TOkA, TOkB, TError>(this ICollection<TOkA> elems, Func<TOkA, Result<TOkB, TError>> f)
    {
        ICollection<TOkB> list = new List<TOkB>(elems.Count);
        Result<ICollection<TOkB>, TError> res = list.ToOk<ICollection<TOkB>, TError>();

        foreach (var elem in elems)
        {
            res = f(elem)
                .Map(r => {
                    list.Add(r);
                    return list;
                });
            if (res.IsError())
            {
                return res;
            }
        }

        return res;
    }

    public static (Result<ICollection<TOkB>, TError> result, ICollection<TError> errors) TraverseA<TOkA, TOkB, TError>(
        this ICollection<TOkA> elems,
        Func<TOkA, Result<TOkB, TError>> f)
    {
        ICollection<TOkB> list = new List<TOkB>(elems.Count);
        Result<ICollection<TOkB>, TError> res = new Result<ICollection<TOkB>, TError>.Error(default!);

        List<TError> errors = new List<TError>(elems.Count);

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
                });
        }

        return (res, errors);
    }

    public static (Result<ICollection<TOkB>, TErrorA> result, ICollection<TErrorB> errors) TraverseA<TOkA, TOkB, TErrorA, TErrorB>(
        this ICollection<TOkA> elems,
        Func<TOkA, Result<TOkB, TErrorA>> f,
        Func<TErrorA, TErrorB> ferr)
    {
        ICollection<TOkB> list = new List<TOkB>(elems.Count);
        Result<ICollection<TOkB>, TErrorA> res = new Result<ICollection<TOkB>, TErrorA>.Error(default!);

        List<TErrorB> errors = new List<TErrorB>(elems.Count);

        foreach (var elem in elems)
        {
            res = f(elem)
                .Map(r => {
                    list.Add(r);
                    return list;
                })
                .IterError(error =>
                {
                    errors.Add(ferr(error));
                });
        }

        return (res, errors);
    }


    public static async Task<Result<TOk, TError>> SequenceTask<TOk, TError>(this Result<Task<TOk>, TError> result)
    {
        if (result.TryGetSuccessContext(out var task))
        {
            return (await task).ToOk<TOk, TError>();
        }

        return new Result<TOk, TError>.Error(result.FailedContext());
    }

    /*
    /// <summary>
    /// <see cref="Result{TOk,TFailure}"/> traverse.
    /// </summary>
    /// <typeparam name="TOkA"></typeparam>
    /// <typeparam name="TOkB"></typeparam>
    /// <typeparam name="__"></typeparam>
    /// <param name="result"></param>
    /// <param name="f"></param>
    /// <returns></returns>
    public static Task<Result<TOkB, __>> TraverseTask<TOkA, TOkB, __>(this Result<TOkA, __> result, Func<TOkA, Task<TOkB>> f)
    {
        if (result.TryGetSucceededContext(out var sc))
        {

            var fTaskResult = Task.FromResult(new Func<TOkB, Result<TOkB, __>>(Result<TOkB, __>.Success));
            var fB = f(sc);

            return fB.TaskApply(fTaskResult);
        }

        return Task.FromResult(Result<TOkB, __>.Failure(result.FailedContext));
    }

    /// <summary>
    /// <see cref="Result{TOk,TFailure}"/> traverse.
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


    


    public static async ValueTask<Result<TOk>> SequenceTask<TOk>(this Result<ValueTask<TOk>> result)
    {
        if (result.TryGetSucceededContext(out var task))
        {
            return (await task).ToSuccessResult();
        }

        return Result.Failure<TOk>(result.FailedContext);
    }
    */

}
