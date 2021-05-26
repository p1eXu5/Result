using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using p1eXu5.Result.Extensions;
using p1eXu5.Result.Generic;
using Shouldly;

namespace p1eXu5.Result.Tests.UnitTests.Extensions
{
    public class TaskExtensionsUTests
    {
        [Test]
        public async Task ToResult_GenericTaskCompleted_ReturnsSucceededResult()
        {
            var task = Task.FromResult( "Ok" );

            Result< string > result = await (task.ToTaskResult( CancellationToken.None) );

            result.Succeeded.ShouldBe( true );
            result.SuccessContext.ShouldBe( "Ok" );
        }

        [Test]
        public async Task ToResult_GenericTaskCanceled_ReturnsFailedResult()
        {
            CancellationTokenSource source = new CancellationTokenSource();
            var token = source.Token;
            source.Cancel();

            var task = Task.FromCanceled<string>( token );

            Result<string> result = await task.ToTaskResult( token );

            result.Succeeded.ShouldBe( false );
            result.FailedContext.ShouldBe( "Task was canceled." );
        }

        [Test]
        public async Task ToResult_GenericTaskFailed_ReturnsFailedResult()
        {
            var task = Task.FromException<string>( new ArgumentException("error") );

            Result<string> result = await task.ToTaskResult( CancellationToken.None );

            result.Succeeded.ShouldBe( false );
            result.FailedContext.ShouldBe( "error" );
        }





        [Test]
        public async Task ToResult_TaskCompleted_ReturnsSucceededResult()
        {
            var task = Task.CompletedTask;

            Result result = await task.ToTaskResult( CancellationToken.None );

            result.Succeeded.ShouldBe( true );
        }

        [Test]
        public async Task ToResult_TaskCanceled_ReturnsFailedResult()
        {
            CancellationTokenSource source = new CancellationTokenSource();
            var token = source.Token;
            source.Cancel();

            var task = Task.FromCanceled( token );

            Result result = await task.ToTaskResult( token );

            result.Succeeded.ShouldBe( false );
            result.FailedContext.ShouldBe( "Task was canceled." );
        }

        [Test]
        public async Task ToResult_TaskFailed_ReturnsFailedResult()
        {
            var task = Task.FromException( new ArgumentException("error") );

            Result result = await task.ToTaskResult( CancellationToken.None );

            result.Succeeded.ShouldBe( false );
            result.FailedContext.ShouldBe( "error" );
        }
    }
}
