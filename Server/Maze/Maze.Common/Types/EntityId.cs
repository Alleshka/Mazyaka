using System;
using System.Threading;

namespace Maze.Common.Types
{
    using IdValue = Int32;

    public readonly struct EntityId : IEquatable<EntityId>
    {
        private static int _counter = 0;
        public IdValue Value { get; }

        private EntityId(IdValue value) => Value = value;

        public static EntityId New() => new EntityId(Interlocked.Increment(ref _counter));
        public static EntityId From(IdValue value) => new EntityId(value);
        public static EntityId Empty => new EntityId(Int32.MinValue);


        public bool Equals(EntityId other) => Value == other.Value;
        public override bool Equals(object obj) => obj is EntityId other && Equals(other);
        public override int GetHashCode() => Value.GetHashCode();
        public override string ToString() => Value.ToString();

        public static bool operator ==(EntityId left, EntityId right) => left.Equals(right);
        public static bool operator !=(EntityId left, EntityId right) => !left.Equals(right);
        public static bool operator <= (EntityId left, EntityId right)=> left.Value <= right.Value;
        public static bool operator >=(EntityId left, EntityId right) => left.Value >= right.Value;
    }
}
