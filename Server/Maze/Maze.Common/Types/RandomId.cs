using System;

namespace Maze.Common.Types
{
    using UserIdValue = Guid;

    public readonly struct RandomId : IEquatable<RandomId>
    {
        public UserIdValue Value { get; }

        private RandomId(UserIdValue value) => Value = value;

        public static RandomId New() => new RandomId(Guid.NewGuid());
        public static RandomId From(UserIdValue value) => new RandomId(value);
        public static RandomId Empty => new RandomId(Guid.Empty);

        public bool Equals(RandomId other) => Value == other.Value;
        public override bool Equals(object obj) => obj is RandomId other && Equals(other);
        public override int GetHashCode() => Value.GetHashCode();
        public override string ToString() => Value.ToString();

        public static bool operator ==(RandomId left, RandomId right) => left.Equals(right);
        public static bool operator !=(RandomId left, RandomId right) => !left.Equals(right);
    }
}
