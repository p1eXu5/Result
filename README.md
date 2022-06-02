# Result

| Package       | Versions                                                                                                                                |
| ------------- | --------------------------------------------------------------------------------------------------------------------------------------- |
| p1eXu5.Result | [![NuGet](https://img.shields.io/badge/nuget-1.0.0-green)](https://www.nuget.org/packages/p1eXu5.Result/1.0.0)     |
| p1eXu5.Result | [![NuGet](https://img.shields.io/badge/nuget-0.1.3-yellowgreen)](https://www.nuget.org/packages/p1eXu5.Result/0.1.3)     |
| p1eXu5.Result | [![NuGet](https://img.shields.io/badge/nuget-0.1.2--alpha5-yellowgreen)](https://www.nuget.org/packages/p1eXu5.Result/0.1.2-alpha5)     |


[Api Documentation](https://p1exu5.github.io/Result/api/index.html)



### 1. Creating Result object.

```csharp

using p1eXu5.Result;
	
Result<string> _ = Result.Failure<string>("error");

Result<int> _ = Result<int>.Failure("error");

Result<Task<int>> _ = Result.Success(Task.FromResult(12));

```

<br/>

<i>Readme is in progress, see [Api Documentation](https://p1exu5.github.io/Result/api/index.html) for details</i>
