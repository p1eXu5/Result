using System;
using NUnit.Framework;
using p1eXu5.Result.Extensions;
using p1eXu5.Result.Generic;
using Shouldly;

namespace p1eXu5.Result.Tests.UnitTests.Extensions
{
    public class ResultExtensionsUTests
    {
        [Test]
        public void Bind_FunctionReturnsSucceeded_ReturnsSucceededWithExpectedType()
        {
            // Arrange:
            Result<int> v = Result.Success(18);
            Func<int, Result<string>> f = i => Result.Success(i.ToString());

            // Action:
            var actual = v.Bind( f );

            // Assert:
            actual.ShouldBe( Result.Success("18") );
        }

        [Test]
        public void Bind_FunctionReturnsFailure_ReturnsFailureWithExpectedType()
        {
            // Arrange:
            Result<int> v = Result.Success(18);
            Func<int, Result<Type>> f = i => Result.Failure<Type>(i.ToString());

            // Action:
            var actual = v.Bind( f );

            // Assert:
            actual.ShouldBe( Result.Failure<Type>("18") );
        }
    }
}
