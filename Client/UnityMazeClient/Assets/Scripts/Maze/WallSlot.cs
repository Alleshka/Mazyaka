using UnityEngine;

namespace MazeGame.Maze
{
    /// <summary>
    /// One directional edge of a MazeCell.
    /// Created entirely from code by MazeCell.Initialize — no prefab needed.
    /// Uses a single SpriteRenderer; connection type is expressed through colour.
    /// </summary>
    public class WallSlot : MonoBehaviour
    {
        private SpriteRenderer _renderer;

        private void Awake()
        {
            _renderer              = gameObject.AddComponent<SpriteRenderer>();
            _renderer.sprite       = Sprites.Square;
            _renderer.color        = WallColor;
            _renderer.sortingOrder = 1; // render above floor sprites (order 0)
            gameObject.SetActive(false); // unknown by default — hidden until confirmed
        }

        /// <summary>Makes this slot visible as a confirmed wall.</summary>
        public void ShowWall()
        {
            gameObject.SetActive(true);
            _renderer.color = WallColor;
        }

        public void ShowExit()
        {
            gameObject.SetActive(true);
            _renderer.color = Color.green;
        }

        /// <summary>Hides this slot (wall state unknown).</summary>
        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private static readonly Color WallColor = new Color(0.80f, 0.80f, 0.80f);
    }
}
