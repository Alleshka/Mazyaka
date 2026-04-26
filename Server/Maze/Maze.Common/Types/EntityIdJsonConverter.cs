using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Maze.Common.Types
{
    public class EntityIdJsonConverter : JsonConverter<EntityId>
    {
        public override EntityId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Number)
            {
                return EntityId.From(reader.GetInt32());
            }

            if (reader.TokenType == JsonTokenType.StartObject)
            {
                int value = 0;

                while (reader.Read())
                {
                    if (reader.TokenType == JsonTokenType.EndObject) break;
                    if (reader.TokenType == JsonTokenType.Number)
                    {
                        value = reader.GetInt32();
                        break;
                    }
                    if (reader.TokenType == JsonTokenType.PropertyName && reader.GetString() == "Value")
                    {
                        reader.Read();
                        value = reader.GetInt32();
                    }
                }

                return EntityId.From(value);
            }

            return EntityId.Empty;
        }

        public override void Write(Utf8JsonWriter writer, EntityId value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value.Value);
        }
    }
}
