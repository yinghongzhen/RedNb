namespace RedNb.Core.Web;

public class DefaultJsonConverter : JsonConverter<long>
{
    public override long Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            var str = reader.GetString();

            if (!String.IsNullOrWhiteSpace(str))
                return Convert.ToInt64(str);
        }

        if (reader.TokenType == JsonTokenType.Number)
        {
            var val = 0L;

            if (reader.TryGetInt64(out val))
            {
                return val;
            }
        }

        return 0;
    }

    public override void Write(Utf8JsonWriter writer, long value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}