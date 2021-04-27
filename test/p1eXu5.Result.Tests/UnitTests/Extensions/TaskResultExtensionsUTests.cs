using System;
using System.Threading.Tasks;
using NUnit.Framework;
using p1eXu5.Result.Extensions;
using p1eXu5.Result.Generic;
using Shouldly;

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
#pragma warning disable 162
                            return sc.ToResult();
#pragma warning restore 162
                        } ))
            );

            actual?.Message.ShouldBe( "failure" );
        }
    }
}
