# Result

| Package       | Versions                                                                                                                                |
| ------------- | --------------------------------------------------------------------------------------------------------------------------------------- |
| p1eXu5.Result | [![NuGet](https://img.shields.io/badge/nuget-3.0.0-green)](https://www.nuget.org/packages/p1eXu5.Result/3.0.0)     |
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

### 2. Serialization with System.Text.Json

```cs
using System.Text.Json;
using p1eXu5.Result.Serialization;

Result<string, string> resultIn = new Result<string, string>.Ok("ok");
var options = new JsonSerializerOptions
{
    Converters = { new ResultJsonConverterFactory() },
};

string json = JsonSerializer.Serialize(resultIn, options);
var resultOut = JsonSerializer.Deserialize<Result<string, string>>(json, options);
```

<br/>

<i>See [Api Documentation](https://p1exu5.github.io/Result/api/index.html) for other extension methods</i>

<i>See [tests](https://github.com/p1eXu5/Result/tree/master/test/p1eXu5.Result.Tests) for additional examples</i>