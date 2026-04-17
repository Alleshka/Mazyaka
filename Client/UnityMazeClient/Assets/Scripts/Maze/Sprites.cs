using UnityEngine;

namespace MazeGame.Maze
{
    /// <summary>
    /// Lazily creates and caches a single white 1x1 sprite used by all maze visuals.
    /// Colour is applied via SpriteRenderer.color so we only ever need one texture.
    /// </summary>
    internal static class Sprites
    {
        private static Sprite _square;

        public static Sprite Square
        {
            get
            {
                if (_square != null) return _square;

                var tex = new Texture2D(2, 2, TextureFormat.RGBA32, false)
                {
                    filterMode = FilterMode.Point
                };
                tex.SetPixels(new[] { Color.white, Color.white, Color.white, Color.white });
                tex.Apply();

                _square = Sprite.Create(tex, new Rect(0, 0, 2, 2), Vector2.one * 0.5f, 2f);
                return _square;
            }
        }
    }
}
