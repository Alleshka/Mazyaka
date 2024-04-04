using Maze.Common;
using UnityEngine;

public interface IMazeRenderer
{
    public void RenderField();
    public GameObject InitPlayer(GameObject player, int row, int col);

    public void RenderWall(int row, int col, MoveDirection direction, string texture);
    public void RemoveWall(int row, int col, MoveDirection direction);

    // TODO: Change
    public void MovePlayer(GameObject player, int row, int col, int newRow, int newCol);
}
