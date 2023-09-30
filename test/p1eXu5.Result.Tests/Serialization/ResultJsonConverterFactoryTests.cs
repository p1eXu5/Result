using System.Text.Json;
using p1eXu5.Result.Serialization;

namespace p1eXu5.Result.Tests.Serialization;

public sealed class ResultJsonConverterFactoryTests
{
    [Test]
    public void OkResult_SerializeDeserialize_Test()
    {
        // Arrange:
        Result<string, string> resultIn = new Result<string, string>.Ok("ok");
        var options = new JsonSerializerOptions
        {
            Converters = { new ResultJsonConverterFactory() },
        };


        // Action:
        string json = JsonSerializer.Serialize(resultIn, options);
        var resultOut = JsonSerializer.Deserialize<Result<string, string>>(json, options);

        // Assert:
        resultIn.Should().Be(resultOut);
    }

    [Test]
    public void ErrorResult_SerializeDeserialize_Test()
    {
        // Arrange:
        Result<string, string> resultIn = new Result<string, string>.Error("error");
        var options = new JsonSerializerOptions
        {
            Converters = { new ResultJsonConverterFactory() },
        };


        // Action:
        string json = JsonSerializer.Serialize(resultIn, options);
        var resultOut = JsonSerializer.Deserialize<Result<string, string>>(json, options);

        // Assert:
        resultIn.Should().Be(resultOut);
    }
}
