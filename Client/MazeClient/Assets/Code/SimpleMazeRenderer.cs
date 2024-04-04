using Maze.Common;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMazeRenderer : IMazeRenderer
{
    Dictionary<string, Material> _textureMap = new Dictionary<string, Material>();

    private int _rowCount;
    private int _colCount;
    private float _cellSize;

    private GameObject _fieldCubePrefab; // Prefab for field cubes representing cells

    public SimpleMazeRenderer(int rowCount, int colCount, float cellSize)
    {
        _textureMap.Add("default", Resources.Load<Material>("Materials/WallMaterial"));

        _rowCount = rowCount;
        _colCount = colCount;
        _cellSize = cellSize;

        _fieldCubePrefab = GameObject.CreatePrimitive(PrimitiveType.Cube);
    }

    public void RenderField()
    {
        for (int i = 0; i < _rowCount; i++)
        {
            for (int j = 0; j < _colCount; j++)
            {
                Vector3 position = new Vector3(i * _cellSize, 0f, j * _cellSize);
                GameObject.Instantiate(_fieldCubePrefab, position, Quaternion.identity);
            }
        }
    }

    public GameObject InitPlayer(GameObject player, int row, int col)
    {
        var result = GameObject.Instantiate(player, new Vector3(row * _cellSize, _cellSize / 2f, col * _cellSize), Quaternion.identity);
        return result;
    }

    public void RenderWall(int row, int col, MoveDirection direction, string texture)
    {
        if (!_textureMap.TryGetValue(texture, out Material material))
        {
            Debug.LogError("Texture not found: " + texture);
            material = _textureMap["default"];
        }

        Vector3 position = new Vector3(row, _cellSize / 2f, col);
        Quaternion rotation = Quaternion.identity;

        switch (direction)
        {
            case MoveDirection.Up:
                {
                    position += Vector3.left * _cellSize / 2;
                    rotation = Quaternion.Euler(0f, 90f, 0f);
                    break;
                }
            case MoveDirection.Right:
                {
                    position += Vector3.forward * _cellSize / 2;
                    break;
                }
            case MoveDirection.Down:
                {
                    position += Vector3.right * _cellSize / 2;
                    rotation = Quaternion.Euler(0f, -90f, 0f);
                    break;
                }
            case MoveDirection.Left:
                {
                    position += Vector3.back * _cellSize / 2;
                    break;
                }
        }

        GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        wall.transform.position = position;
        wall.transform.rotation = rotation;
        wall.GetComponent<Renderer>().material = material;
        wall.transform.localScale = new Vector3(_cellSize, _cellSize, 0.1f);
        wall.tag = "Wall";
    }

    public void RemoveWall(int row, int col, MoveDirection direction)
    {
        Vector3 position = new Vector3(row * _cellSize, _cellSize / 2f, col * _cellSize);

        switch (direction)
        {
            case MoveDirection.Up:
                {
                    position += Vector3.left * _cellSize / 2;
                    break;
                }
            case MoveDirection.Right:
                {
                    position += Vector3.forward * _cellSize / 2;
                    break;
                }
            case MoveDirection.Down:
                {
                    position += Vector3.right * _cellSize / 2;
                    break;
                }
            case MoveDirection.Left:
                {
                    position += Vector3.back * _cellSize / 2;
                    break;
                }
        }

        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
        foreach (GameObject wall in walls)
        {
            if (Vector3.Distance(wall.transform.position, position) < 0.01f) // Adjust the threshold as needed
            {
                GameObject.Destroy(wall);
            }
        }
    }

    public void MovePlayer(GameObject player, int row, int col, int newRow, int newCol)
    {
        Vector3 newPosition = new Vector3(newRow * _cellSize, _cellSize / 2f, newCol * _cellSize);
        player.transform.position = newPosition;
    }
}
