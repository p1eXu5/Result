using System.Diagnostics;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace p1eXu5.Result.Serialization;

public class ResultJsonConverterFactory : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
         => typeToConvert.IsGenericType
            &&
            (typeToConvert.GetGenericTypeDefinition() == typeof(Result<,>));

    public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        Debug.Assert(typeToConvert.IsGenericType &&
            (typeToConvert.GetGenericTypeDefinition() == typeof(Result<,>)));

        Type okType = typeToConvert.GetGenericArguments()[0];
        Type errorType = typeToConvert.GetGenericArguments()[1];

        JsonConverter converter = (JsonConverter)Activator.CreateInstance(
            typeof(ResultJsonConverter<,>)
                .MakeGenericType(new Type[] { okType, errorType }),
            BindingFlags.Instance | BindingFlags.Public,
            binder: null,
            args: null,
            culture: null)!;

        return converter;
    }

    private class ResultJsonConverter<TOk, TError> : JsonConverter<Result<TOk, TError>>
    {
        public override Result<TOk, TError>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            reader.Read();

            Result<TOk, TError> result = null!;

            if (reader.TokenType == JsonTokenType.EndObject)
            {
                return result;
            }

            if (reader.TokenType != JsonTokenType.PropertyName)
            {
                return result;
            }

            reader.Read();
            var resultCase = reader.GetString();

            if (resultCase?.Equals("Ok", StringComparison.Ordinal) ?? false)
            {
                reader.Read();
                var successContext = JsonSerializer.Deserialize<TOk>(ref reader, options)!;
                result = new Result<TOk, TError>.Ok(successContext);
            }
            else if (resultCase?.Equals("Error", StringComparison.Ordinal) ?? false)
            {
                reader.Read();
                var failedContext = JsonSerializer.Deserialize<TError>(ref reader, options)!;
                result = new Result<TOk, TError>.Error(failedContext);
            }
            else
            {
                throw new JsonException();
            }

            reader.Read(); // EndOfObject

            return result;
        }

        public override void Write(Utf8JsonWriter writer, Result<TOk, TError> value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("Case");

            if (value is Result<TOk, TError>.Ok ok)
            {
                writer.WriteStringValue("Ok");
                writer.WritePropertyName(nameof(Result<TOk, TError>.Ok.SuccessContext));
                JsonSerializer.Serialize(writer, ok.SuccessContext, options);
            }

            if (value is Result<TOk, TError>.Error err)
            {
                writer.WriteStringValue("Error");
                writer.WritePropertyName(nameof(Result<TOk, TError>.Error.FailedContext));
                JsonSerializer.Serialize(writer, err.FailedContext, options);
            }

            writer.WriteEndObject();
        }
    }
}
