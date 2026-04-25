using System.Collections.Generic;
using Maze.Common;
using Maze.Common.DTO;
using UnityEngine;

namespace MazeGame.Maze
{
    /// <summary>
    /// The ONLY class that knows about grid topology.
    /// Created from code by GameManager — no Inspector references needed.
    ///
    /// Centering: tracks the bounding box of all revealed cells and shifts an
    /// internal GridContainer so the explored centre aligns with world origin (0,0,0).
    /// Keep the camera fixed at (0, 0, -10).
    /// </summary>
    public class MazeGrid : MonoBehaviour
    {
        private float _cellSize;
        private int   _cols;

        private Transform _container;                         // moves for centering
        private readonly Dictionary<int, MazeCell> _cells = new();

        // Canonical connection key: min(a,b) * 1_000_000 + max(a,b)
        // Tracks which connections are currently displayed as walls.
        // Key → (cellIdA, dirFromA, cellIdB, dirFromB)
        private readonly Dictionary<int, (int cellA, MoveDirection dirA)> _shownWalls = new();

        private int _minRow = int.MaxValue, _maxRow = int.MinValue;
        private int _minCol = int.MaxValue, _maxCol = int.MinValue;

        private void Awake()
        {
            // Internal container — its world position is shifted during Recenter()
            var go = new GameObject("GridContainer");
            go.transform.SetParent(transform);
            _container = go.transform;
        }

        /// <summary>Call once after CreateGame returns rows/cols.</summary>
        public void Initialize(int cols, float cellSize = 1f)
        {
            _cols     = cols;
            _cellSize = cellSize;
        }

        /// <summary>Creates (or updates) a cell when the player enters it, then recenters.</summary>
        public MazeCell RevealCell(int cellId)
        {
            if (!_cells.TryGetValue(cellId, out var cell))
            {
                var go = new GameObject(); // named inside MazeCell.Initialize
                Debug.Log(cellId);
                go.transform.SetParent(_container);
                go.transform.localPosition = LocalPos(cellId);

                cell = go.AddComponent<MazeCell>();
                cell.Initialize(cellId, _cellSize);
                _cells[cellId] = cell;

                UpdateBounds(cellId);
                Recenter();
            }

            cell.SetRevealed(true);
            return cell;
        }

        public void HideWall(int connectionId)
        {
            if (!_shownWalls.TryGetValue(connectionId, out var w)) return;
            if (_cells.TryGetValue(w.cellA, out var cellA)) cellA.HideWall(w.dirA);
            _shownWalls.Remove(connectionId);
        }

        /// <summary>
        /// Records a blocked move as a wall.
        /// Uses the canonical connection ID (min/max cell pair) to avoid
        /// rendering the same physical wall twice from opposite sides.
        /// Also shows the wall on the neighbour's cell if it is already revealed.
        /// </summary>
        public void MarkWall(int fromCellId, MoveDirection direction, MoveBlocker blocker)
        {
            int key = blocker.BlockerId;

            if (_shownWalls.ContainsKey(key))
                return; // already displayed from the other side

            MoveDirection oppositeDir = direction.Opposite();
            _shownWalls[key] = (fromCellId, direction);

            if (_cells.TryGetValue(fromCellId, out var cell))
                cell.ShowWall(direction);
        }

        public void MarkExit(int fromCellId, MoveDirection direction)
        {
            if (_cells.TryGetValue(fromCellId, out var cell))
                cell.ShowExit(direction);
        }

        /// <summary>Removes a wall (e.g. after breaking it) from both sides.</summary>
        public void ClearWall(int wallId)
        {
            if (!_shownWalls.TryGetValue(wallId, out var w)) return;

            if (_cells.TryGetValue(w.cellA, out var cellA)) cellA.HideWall(w.dirA);

            _shownWalls.Remove(wallId);
        }

        /// <summary>World-space position for a cellId — topology math hidden from callers.</summary>
        public Vector3 GetWorldPos(int cellId) => _container.position + LocalPos(cellId);

        private (int row, int col) GridPos(int cellId) => (cellId / _cols, cellId % _cols);

        private Vector3 LocalPos(int cellId)
        {
            var (row, col) = GridPos(cellId);
            return new Vector3(col * _cellSize, -row * _cellSize, 0f);
        }

        private void UpdateBounds(int cellId)
        {
            var (row, col) = GridPos(cellId);
            if (row < _minRow) _minRow = row;
            if (row > _maxRow) _maxRow = row;
            if (col < _minCol) _minCol = col;
            if (col > _maxCol) _maxCol = col;
        }

        /// <summary>
        /// Shifts the container so the centre of the explored bounding box = world origin.
        ///   cell local pos  = (col * size,  -row * size,  0)
        ///   net world pos   = ((col - centreCol) * size,  -(row - centreRow) * size,  0)
        /// </summary>
        private void Recenter()
        {
            float centreCol = (_minCol + _maxCol) * 0.5f;
            float centreRow = (_minRow + _maxRow) * 0.5f;
            _container.position = new Vector3(-centreCol * _cellSize, centreRow * _cellSize, 0f);
        }
    }
}
