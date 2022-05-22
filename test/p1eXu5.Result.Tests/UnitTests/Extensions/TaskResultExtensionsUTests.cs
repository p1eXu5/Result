using System;
using System.Threading.Tasks;
using NUnit.Framework;
using p1eXu5.Result.Extensions;
using p1eXu5.Result.Generic;
using Shouldly;

#pragma warning disable 162

namespace p1eXu5.Result.Tests.UnitTests.Extensions
{
    public class TaskResultExtensionsUTests
    {
        [Test]
        public void Bind_ExceptionIsThrows_ThrowsRaisedException()
        {
            var result = "failure".ToResult();
            var actual = Assert.CatchAsync<InvalidOperationException>(
                async () =>
                    await result
                        .Retn()
                        .Bind( sc => Task.Run<Result<string>>( () => {
                            throw new InvalidOperationException( sc );
                            return sc.ToResult();
                        } ))
            );

            actual?.Message.ShouldBe( "failure" );
        }

        [Test]
        public async Task Bind_SuccesResultWithString_ToTaskResult_ReturnsSuccesResult()
        {
            Task<Result<string>> task1 () 
                => Task.FromResult(Result<string>.Success("sdf"));

            Task<Result> task2(string arg)
                => Task.FromResult(Result.Success());

            var resTsk =
                task1().Bind(task2);

            var res = await resTsk;

            res.Succeeded.ShouldBe(true);
        }
    }
}
