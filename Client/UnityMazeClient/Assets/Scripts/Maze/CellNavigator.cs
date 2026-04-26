using Maze.Common.Types;
using System.Collections;
using UnityEngine;

namespace MazeGame.Maze
{
    /// <summary>
    /// Attaches to any moving entity (player, etc.).
    /// Exposes address-based movement by cellId — hides all coordinate math from callers.
    ///
    /// Usage:
    ///   navigator.PlaceAt(cellId);          // instant placement
    ///   navigator.MoveTo(cellId);           // animated movement
    /// </summary>
    public class CellNavigator : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;

        private MazeGrid _grid;

        public EntityId CurrentCellId { get; private set; } = EntityId.Empty;

        private Coroutine _moveCoroutine;

        public void Initialize(MazeGrid grid) => _grid = grid;

        public void SetMoveSpeed(float speed) => moveSpeed = speed;

        /// <summary>Instantly places the entity at the given cell.</summary>
        public void PlaceAt(EntityId cellId)
        {
            CurrentCellId = cellId;
            transform.position = _grid.GetWorldPos(cellId);
        }

        /// <summary>Smoothly moves the entity to the given cell.</summary>
        public void MoveTo(EntityId cellId)
        {
            CurrentCellId = cellId;

            if (_moveCoroutine != null)
                StopCoroutine(_moveCoroutine);

            _moveCoroutine = StartCoroutine(SlideTo(_grid.GetWorldPos(cellId)));
        }

        private IEnumerator SlideTo(Vector3 target)
        {
            while (Vector3.Distance(transform.position, target) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position, target, moveSpeed * Time.deltaTime);
                yield return null;
            }

            transform.position = target;
        }
    }
}
