﻿using System;

namespace Mazyaka.Model
{
    public abstract class GameObject
    {
        public Guid ID { get; set; } = Guid.NewGuid(); // Уникальный ID объекта

        public Point Position { get; set; }

        public abstract void Action(GameObject obj);
    }
}