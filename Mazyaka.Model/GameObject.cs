using System;

namespace Mazyaka.Model
{
    public abstract class GameObject
    {
        public Point Position { get; set; }

        public abstract void Action(GameObject obj);
    }
}