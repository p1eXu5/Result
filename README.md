# Result

| Package       | Versions                                                                                                                                |
| ------------- | --------------------------------------------------------------------------------------------------------------------------------------- |
| p1eXu5.Result | [![NuGet](https://img.shields.io/badge/nuget-0.1.3-green)](https://www.nuget.org/packages/p1eXu5.Result/0.1.3)     |
| p1eXu5.Result | [![NuGet](https://img.shields.io/badge/nuget-0.1.2--alpha5-yellowgreen)](https://www.nuget.org/packages/p1eXu5.Result/0.1.2-alpha5)     |


[Api Documentation](https://p1exu5.github.io/Result/api/index.html)



### 1. Creating Result object.

```csharp

using p1eXu5.Result;
	
Result<string> _ = Result.Failure<string>("error");

Result<int> _ = Result<int>.Failure("error");

Result<Task<int>> _ = Result.Success(Task.FromResult(12));

```


## Result Extension Methods

- Factory methods

	| Method | Description |
	| ------ | ----------- |
	| | |

	
- Map methods

	| Method | Description |
	| ------ | ----------- |
	| | |


- Bind methods

	| Method | Description |
	| ------ | ----------- |
	| | |


- Filter methods

	| Method | Description |
	| ------ | ----------- |
	| | |


- Iter methods

	| Method | Description |
	| ------ | ----------- |
	| | |


- Special methods

	| Method | Description |
	| ------ | ----------- |
	| | |


## Tsk Extension Methods

- Factory methods

	| Method | Description |
	| ------ | ----------- |
	| [ToTaskResult]() | |
	| [ToTaskResult]() | |
	| [ToValueTaskResult]() | |
	| [ToValueTaskResult]() | |
	| [ToValueTaskResult]() | |
	| [ToValueTaskResult]() | |
