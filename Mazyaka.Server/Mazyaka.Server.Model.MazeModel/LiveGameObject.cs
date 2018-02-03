using System;

namespace Mazyaka.Server.Model.MazeModel
{
    public enum MoveDirection { UP, RIGHT, DONW, LEFT }

    public abstract class LiveGameObject : GameObject
    {
        public double ActionPoint { get; set; }
        public double HealthPoint { get; set; }
        public double Observe { get; set; }

        public abstract void Move(MoveDirection direction);
    }
}