using System;
using System.Collections.Generic;
using System.Linq;
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

        [Test]
        public void TraverseM_AllSucceeded_ReturnsSucceededResult()
        {
            // Arrange:
            var list = new int[] { 1, 2, 3, 4 };
            Func<int, Result<int> > f = (int i) => i.ToResult();

            // Action:
            var actual = list.TraverseM( f );

            // Assert:
            actual.Succeeded.ShouldBeTrue();
            actual.SuccessContext.ShouldBe( list );
        }

        [Test]
        public void TraverseM_SomeFailed_ReturnsFailedResult()
        {
            // Arrange:
            var list = new int[] { 1, 2, 3, 4 };
            bool invokedWithLastElem = false;
            Func<int, Result<int> > f = (int i) => {
                if ( i == 4 ) invokedWithLastElem = true;
                return i == 3 ? Result.Failure< int >( "error" ) : i.ToResult();
            };

            // Action:
            var actual = list.TraverseM( f );

            // Assert:
            actual.Succeeded.ShouldBeFalse();
            actual.FailedContext.ShouldBe( "error" );
            invokedWithLastElem.ShouldBeFalse();
        }

        [Test]
        public void TraverseM_AllSucceeded_DifferentTypes_ReturnsSucceededResult()
        {
            // Arrange:
            var list = new int[] { 1, 2, 3, 4 };
            Func<int, Result<string> > f = (int i) => i.ToString().ToResult();

            // Action:
            var actual = list.TraverseM( f );

            // Assert:
            actual.Succeeded.ShouldBeTrue();
            actual.SuccessContext.ShouldBe( list.Select( i => i.ToString() ) );
        }

        [Test]
        public void TraverseM_SomeFailed_DifferentTypes_ReturnsFailedResult()
        {
            // Arrange:
            var list = new int[] { 1, 2, 3, 4 };
            bool invokedWithLastElem = false;
            Func<int, Result<string> > f = (int i) => {
                if ( i == 4 ) invokedWithLastElem = true;
                return i == 3 ? Result.Failure< string >( "error" ) : i.ToString().ToResult();
            };

            // Action:
            var actual = list.TraverseM( f );

            // Assert:
            actual.Succeeded.ShouldBeFalse();
            actual.FailedContext.ShouldBe( "error" );
            invokedWithLastElem.ShouldBeFalse();
        }
    }
}
