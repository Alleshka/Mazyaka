using System.Collections.Generic;
using System.Data;
using Maze.Common;
using Maze.Common.DTO;
using Maze.Common.Types;
using UnityEngine;
using UnityEngine.Rendering;

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

        private Transform _container;                         // moves for centering

        private readonly Dictionary<EntityId, MazeCell> _cells = new();
        private readonly Dictionary<EntityId, (int row, int col)> _gridPositions = new();

        // Canonical connection key: min(a,b) * 1_000_000 + max(a,b)
        // Tracks which connections are currently displayed as walls.
        // Key → (cellIdA, dirFromA, cellIdB, dirFromB)
        private readonly Dictionary<EntityId, (EntityId cellA, MoveDirection dirA)> _shownWalls = new();

        private int _minRow = int.MaxValue, _maxRow = int.MinValue;
        private int _minCol = int.MaxValue, _maxCol = int.MinValue;

        private int _curRow = 0, _curCol = 0;

        private void Awake()
        {
            // Internal container — its world position is shifted during Recenter()
            var go = new GameObject("GridContainer");
            go.transform.SetParent(transform);
            _container = go.transform;
        }

        /// <summary>Call once after CreateGame returns rows/cols.</summary>
        public void Initialize(float cellSize = 1f)
        {
            _cellSize = cellSize;
        }

        /// <summary>Creates (or updates) a cell when the player enters it, then recenters.</summary>
        public MazeCell RevealCell(EntityId cellId, MoveDirection moveDirection = MoveDirection.None)
        {
            ChangeCur(moveDirection);
            if (!_cells.TryGetValue(cellId, out var cell))
            {
                _gridPositions.Add(cellId, (_curRow, _curCol));

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

        public void HideWall(EntityId connectionId)
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
        public void MarkWall(EntityId fromCellId, MoveDirection direction, MoveBlocker blocker)
        {
            EntityId key = blocker.BlockerId;

            if (_shownWalls.ContainsKey(key))
                return; // already displayed from the other side

            MoveDirection oppositeDir = direction.Opposite();
            _shownWalls[key] = (fromCellId, direction);

            if (_cells.TryGetValue(fromCellId, out var cell))
                cell.ShowWall(direction);
        }

        public void MarkExit(EntityId fromCellId, MoveDirection direction)
        {
            if (_cells.TryGetValue(fromCellId, out var cell))
                cell.ShowExit(direction);
        }

        /// <summary>Removes a wall (e.g. after breaking it) from both sides.</summary>
        public void ClearWall(EntityId wallId)
        {
            if (!_shownWalls.TryGetValue(wallId, out var w)) return;

            if (_cells.TryGetValue(w.cellA, out var cellA)) cellA.HideWall(w.dirA);

            _shownWalls.Remove(wallId);
        }

        /// <summary>World-space position for a cellId — topology math hidden from callers.</summary>
        public Vector3 GetWorldPos(EntityId cellId) => _container.position + LocalPos(cellId);

        private (int row, int col) GridPos(EntityId cellId)
        {
            if (!_gridPositions.TryGetValue(cellId, out (int row, int col) value))
            {
                Debug.LogWarning($"Cell with id = {cellId} not found in GridPos");
            }

            return value;
        }

        private Vector3 LocalPos(EntityId cellId)
        {
            var (row, col) = GridPos(cellId);
            return new Vector3(col * _cellSize, -row * _cellSize, 0f);
        }

        private void UpdateBounds(EntityId cellId)
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

        private void ChangeCur(MoveDirection direction)
        {
            Dictionary<MoveDirection, (int row, int col)> moves = new Dictionary<MoveDirection, (int row, int col)>
            {
                { MoveDirection.None, (0, 0) },
                { MoveDirection.Up, (-1, 0) },
                { MoveDirection.Down, (1, 0) },
                { MoveDirection.Right, (0, 1) },
                { MoveDirection.Left, (0, -1) }
            };

            (int dRow, int dCol) = moves[direction];
            _curRow += dRow;
            _curCol += dCol;
        }
    }
}
