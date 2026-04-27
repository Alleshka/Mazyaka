using System;

namespace Maze.Common.Types
{

    public readonly struct EntityId : IEquatable<EntityId>, IComparable<EntityId>, IEntityId
    {
        private static int _counter = default;
        public int Value { get; }

        private EntityId(int value) => Value = value;

        public static EntityId New() => new EntityId(System.Threading.Interlocked.Increment(ref _counter));
        public static EntityId From(int value) => new EntityId(value);
        public static EntityId From(string value) => new EntityId(int.Parse(value));
        public static EntityId Empty => new EntityId(int.MinValue);

        public bool Equals(EntityId other) => Value == other.Value;
        public override bool Equals(object obj) => obj is EntityId o && Equals(o);
        public override int GetHashCode() => Value.GetHashCode();
        public override string ToString() => Value.ToString();
        public int CompareTo(EntityId other) => Value.CompareTo(other.Value);

        public static bool operator ==(EntityId l, EntityId r) => l.Equals(r);
        public static bool operator !=(EntityId l, EntityId r) => !l.Equals(r);
        public static bool operator <(EntityId l, EntityId r) => l.CompareTo(r) < 0;
        public static bool operator >(EntityId l, EntityId r) => l.CompareTo(r) > 0;
        public static bool operator <=(EntityId l, EntityId r) => l.CompareTo(r) <= 0;
        public static bool operator >=(EntityId l, EntityId r) => l.CompareTo(r) >= 0;

        public sealed class JsonConverter : System.Text.Json.Serialization.JsonConverter<EntityId>
        {
            public override EntityId Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
            {
                var value = reader.GetString();
                return string.IsNullOrEmpty(value) ? Empty : From(value);
            }

            public override void Write(System.Text.Json.Utf8JsonWriter writer, EntityId value, System.Text.Json.JsonSerializerOptions options)
            {
                writer.WriteStringValue(value.ToString());
            }
        }
    }
}
