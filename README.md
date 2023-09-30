# Result

| Package       | Versions                                                                                                                                |
| ------------- | --------------------------------------------------------------------------------------------------------------------------------------- |
| p1eXu5.Result | [![NuGet](https://img.shields.io/badge/nuget-2.0.0-green)](https://www.nuget.org/packages/p1eXu5.Result/2.0.0)     |



[Api Documentation](https://p1exu5.github.io/Result/api/index.html)



### 1. Creating Result object.

```csharp

using p1eXu5.Result;
using p1eXu5.Result.Extensions;
using Unit = System.ValueTuple;
	
// using constructors
var successResult = new Result<string, string>.Ok("1");
var errorResult = new Result<string, string>.Error("error");

// using extension methods
Result<int, Unit> intSuccessResult = 18.ToOk();
Result<string, string> r2 = "1".ToError<string>();

```

<br/>

<i>See [Api Documentation](https://p1exu5.github.io/Result/api/index.html) for other extension methods</i>
