using Maze.Common;
using Maze.Common.Types;
using System.Collections.Generic;
using UnityEngine;

namespace MazeGame.Maze
{
    /// <summary>
    /// Topology-agnostic cell. Created entirely from code by MazeGrid — no prefab needed.
    /// Wall slots start hidden (state unknown). A wall is only shown when the server
    /// confirms a move in that direction failed.
    /// </summary>
    public class MazeCell : MonoBehaviour
    {
        public EntityId CellId { get; private set; }

        private readonly Dictionary<MoveDirection, WallSlot> _slots = new();
        private SpriteRenderer _floorRenderer;

        /// <summary>Called by MazeGrid after instantiation.</summary>
        public void Initialize(EntityId cellId, float cellSize)
        {
            CellId = cellId;
            gameObject.name = $"Cell_{cellId}";

            // Floor tile
            _floorRenderer = gameObject.AddComponent<SpriteRenderer>();
            _floorRenderer.sprite = Sprites.Square;
            _floorRenderer.color = new Color(0.12f, 0.12f, 0.12f);
            transform.localScale = Vector3.one * cellSize;

            // Slots sit on the cell boundary (±0.5) and are slightly thicker than the
            // cell gap so they overlap the matching slot on the neighbour — both cells
            // render the same region, making one visually unified wall.
            CreateSlot(MoveDirection.Up, new Vector3(0, 0.50f, 0), new Vector3(1.00f, 0.14f, 1));
            CreateSlot(MoveDirection.Down, new Vector3(0, -0.50f, 0), new Vector3(1.00f, 0.14f, 1));
            CreateSlot(MoveDirection.Left, new Vector3(-0.50f, 0, 0), new Vector3(0.14f, 1.00f, 1));
            CreateSlot(MoveDirection.Right, new Vector3(0.50f, 0, 0), new Vector3(0.14f, 1.00f, 1));
        }

        /// <summary>Shows a confirmed wall in the given direction.</summary>
        public void ShowWall(MoveDirection direction)
        {
            if (_slots.TryGetValue(direction, out var slot))
                slot.ShowWall();
        }

        public void ShowExit(MoveDirection direction)
        {
            if (_slots.TryGetValue(direction, out var slot))
                slot.ShowExit();
        }

        /// <summary>Hides the wall slot in the given direction (e.g. after breaking it).</summary>
        public void HideWall(MoveDirection direction)
        {
            if (_slots.TryGetValue(direction, out var slot))
                slot.Hide();
        }

        /// <summary>Hides all wall slots.</summary>
        public void ResetWalls()
        {
            foreach (var slot in _slots.Values)
                slot.Hide();
        }

        /// <summary>Reveals or hides this cell (fog-of-war).</summary>
        public void SetRevealed(bool visible)
        {
            _floorRenderer.enabled = visible;
        }

        // ── Private ──────────────────────────────────────────────────────

        private void CreateSlot(MoveDirection direction, Vector3 localPos, Vector3 localScale)
        {
            var go = new GameObject($"Slot_{direction}");
            go.transform.SetParent(transform);
            go.transform.localPosition = localPos;
            go.transform.localScale = localScale;

            var slot = go.AddComponent<WallSlot>();
            _slots[direction] = slot;
        }
    }
}
