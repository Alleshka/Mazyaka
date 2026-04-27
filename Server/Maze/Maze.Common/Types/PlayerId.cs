using System;

namespace Maze.Common.Types
{
    public readonly struct PlayerId : IEquatable<PlayerId>, IComparable<PlayerId>, IEntityId
    {
        public System.Guid Value { get; }

        private PlayerId(System.Guid value) => Value = value;

        public static PlayerId New() => new PlayerId(System.Guid.NewGuid());
        public static PlayerId From(System.Guid value) => new PlayerId(value);
        public static PlayerId From(string value) => new PlayerId(System.Guid.TryParse(value, out Guid result) ? result : Guid.Empty);
        public static PlayerId Empty => new PlayerId(System.Guid.Empty);

        public bool Equals(PlayerId other) => Value == other.Value;
        public override bool Equals(object obj) => obj is PlayerId o && Equals(o);
        public override int GetHashCode() => Value.GetHashCode();
        public override string ToString() => Value.ToString();
        public int CompareTo(PlayerId other) => Value.CompareTo(other.Value);

        public static bool operator ==(PlayerId l, PlayerId r) => l.Equals(r);
        public static bool operator !=(PlayerId l, PlayerId r) => !l.Equals(r);
        public static bool operator <(PlayerId l, PlayerId r) => l.CompareTo(r) < 0;
        public static bool operator >(PlayerId l, PlayerId r) => l.CompareTo(r) > 0;
        public static bool operator <=(PlayerId l, PlayerId r) => l.CompareTo(r) <= 0;
        public static bool operator >=(PlayerId l, PlayerId r) => l.CompareTo(r) >= 0;

        public sealed class JsonConverter : System.Text.Json.Serialization.JsonConverter<PlayerId>
        {
            public override PlayerId Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
            {
                var value = reader.GetString();
                return string.IsNullOrEmpty(value) ? Empty : From(value);
            }

            public override void Write(System.Text.Json.Utf8JsonWriter writer, PlayerId value, System.Text.Json.JsonSerializerOptions options)
            {
                writer.WriteStringValue(value.ToString());
            }
        }
    }
}
