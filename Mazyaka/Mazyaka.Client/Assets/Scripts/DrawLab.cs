using System;
using UnityEngine;
using System.Collections;
using Assets.Scripts._3DMaze;
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEngine.UI;

public class DrawLab : MonoBehaviour
{
    public int WallsLayer;

    [SerializeField]
    private Material GridMaterial;

	[SerializeField]
    private Transform LeftRightWall;

	[SerializeField]
	private Transform UpDownWall;

    [SerializeField]
    private Transform AboveBelowWall;

    public bool IsCeilingNeeded = true;
    public bool IsFloorNeeded = true;

    [Header("2D maze")]
    [SerializeField]
    private Sprite wallSprite;

    [SerializeField]
    private float wallWidth;

    [SerializeField]
    private float wallHeight;

    private float _cellSize;
    private float _wallWidth;
    private float _wallHeight;
    private float _outWallWidth;

    private readonly float _heightToWidthRatio = 3f;
    private readonly float _cellsizeToWidthRatio = 210;

    // Use this for initialization
    void Start () {
        _wallWidth = LeftRightWall.lossyScale.x;
        _outWallWidth = _wallWidth;
        _cellSize = LeftRightWall.lossyScale.z;
        _wallHeight = LeftRightWall.lossyScale.y + AboveBelowWall.lossyScale.y;
    }
	
	// Update is called once per frame
	void Update () {

	}

    /// <summary>
    /// Draws maze
    /// </summary>
    /// <param name="maze">maze matrix</param>
    /// <param name="mazeObject">object to contain maze walls</param>
    /// <returns>Position of center of the maze</returns>
    public void Draw (MazeGenerator.Flags[,,] maze, Transform mazeObject)
	{
	    float currentY = 0, currentX = 0, currentZ = 0;
	    int xSize = maze.GetLength(0), ySize = maze.GetLength(1), zSize = maze.GetLength(2);
	    Transform WallsContainer = new GameObject("wallsContainer").transform;

        for (int z = 0; z < zSize; z++)
	    {
            currentZ = 0;
            for (int x = 0; x < xSize; x++)
	        {
	            currentX = 0;
	            for (int y = 0; y < ySize; y++)
	            {
	                if ((maze[x, y, z] & MazeGenerator.Flags.WALL_UP) == MazeGenerator.Flags.WALL_UP && x != xSize - 1)
	                {
	                    Transform wall = Instantiate(UpDownWall);
	                    wall.position = new Vector3(currentX, currentY, currentZ + _cellSize / 2 - _wallWidth / 2);
	                    wall.SetParent(WallsContainer);
                    }
	                if ((maze[x, y, z] & MazeGenerator.Flags.WALL_RIGHT) == MazeGenerator.Flags.WALL_RIGHT && y != ySize - 1)
	                {
	                    Transform wall = Instantiate(LeftRightWall);
	                    wall.position = new Vector3(currentX + _cellSize / 2 - _wallWidth / 2, currentY, currentZ);
	                    wall.SetParent(WallsContainer);
                    }
                    if ((maze[x, y, z] & MazeGenerator.Flags.WALL_ABOVE) == MazeGenerator.Flags.WALL_ABOVE && z != zSize - 1)
                    {
                        Transform wall = Instantiate(AboveBelowWall);
                        wall.position = new Vector3(currentX, currentY + _wallHeight / 2, currentZ);
                        wall.SetParent(WallsContainer);
                    }
                    currentX += _cellSize - _wallWidth;
                    //Debug.Log(String.Format("{0}, {1}, {2}", x, y, z) + maze[x,y,z]);
	            }
	            currentZ += _cellSize - _wallWidth;
	        }
	        currentY += _wallHeight;
        }
	    mazeObject.position = DrawOuterWalls(currentX, currentY, currentZ, xSize, ySize, zSize, WallsContainer);
        WallsContainer.SetParent(mazeObject);
    }


    /// <summary>
    /// Auto fits maze map to the panel
    /// </summary>
    /// <param name="fieldSizeX">map panel x size</param>
    /// <param name="fieldSizeY">map panel y size</param>
    /// <param name="mazeSizeX">maze x size</param>
    /// <param name="mazeSizeY">maze y size</param>
    public void AutoFit(float fieldSizeX, float fieldSizeY, float mazeSizeX, float mazeSizeY)
    {
        float sizeX = fieldSizeX / mazeSizeX;
        float sizeY = fieldSizeY / mazeSizeY;

        _cellSize = sizeX > sizeY ? sizeY : sizeX;

        wallWidth = _cellSize / _cellsizeToWidthRatio;
        wallHeight = wallWidth * _heightToWidthRatio;
    }

    public void Draw2D(MazeGenerator.Flags[,,] maze, Transform mazeObject)
    {

        float currentY = 0, currentX = 0;
        int xSize = maze.GetLength(0), ySize = maze.GetLength(1);
        Transform wallsContainer = new GameObject("wallsContainer").transform;
        GameObject upWall = new GameObject("upWall"), rightWall = new GameObject("rightWall");
        upWall.AddComponent<Image>().sprite = wallSprite;
        upWall.transform.localScale = new Vector3(wallHeight, wallWidth, 0);
        rightWall.AddComponent<Image>().sprite = wallSprite;
        rightWall.transform.localScale = new Vector3(wallWidth, wallHeight, 0);

        float cellSize = upWall.GetComponent<RectTransform>().sizeDelta.x * (wallHeight - wallWidth);
        
        cellSize += wallWidth;

        /*Debug.Log(cellSize);
        Debug.Log(wallWidth);
        Debug.Log(wallHeight);*/

        for (int x = 0; x < xSize; x++)
        {
            currentX = 0;
            for (int y = 0; y < ySize; y++)
            {
                if (x == 0)
                {
                    Transform wall = Instantiate(upWall).transform;
                    wall.position = new Vector3(currentX, currentY - cellSize / 2 + wallWidth / 2, 0);
                    wall.SetParent(wallsContainer);
                }
                if (y == 0)
                {
                    Transform wall = Instantiate(rightWall).transform;
                    wall.position = new Vector3(currentX - cellSize / 2 + wallWidth / 2, currentY, 0);
                    wall.SetParent(wallsContainer);
                }

                if ((maze[x, y, 0] & MazeGenerator.Flags.WALL_UP) == MazeGenerator.Flags.WALL_UP)
                {
                    Transform wall = Instantiate(upWall).transform;
                    wall.position = new Vector3(currentX, currentY + cellSize / 2 - wallWidth / 2, 0);
                    wall.SetParent(wallsContainer);
                }
                if ((maze[x, y, 0] & MazeGenerator.Flags.WALL_RIGHT) == MazeGenerator.Flags.WALL_RIGHT)
                {
                    Transform wall = Instantiate(rightWall).transform;
                    wall.position = new Vector3(currentX + cellSize / 2 - wallWidth / 2, currentY, 0);
                    wall.SetParent(wallsContainer);
                }
                currentX += cellSize - wallWidth;
            }
            currentY += cellSize - wallWidth;
        }
        mazeObject.position = new Vector3(currentX / 2 + wallWidth - cellSize / 2, currentY / 2 + wallWidth / 2 - cellSize / 2);
        wallsContainer.SetParent(mazeObject);
        Destroy(rightWall);
        Destroy(upWall);
    }

    /// <summary>
    /// Draw outer walls
    /// </summary>
    /// <param name="currentX">currentX position of drawing maze</param>
    /// <param name="currentY">currentY position of drawing maze</param>
    /// <param name="currentZ">currentZ position of drawing maze</param>
    /// <param name="xSize">x size of maze</param>
    /// <param name="ySize">y size of maze</param>
    /// <param name="zSize">z size of maze</param>
    /// <param name="wallsContainer">container for walls</param>
    /// <returns>Center of maze</returns>
    public Vector3 DrawOuterWalls(float currentX, float currentY, float currentZ, int xSize, int ySize, int zSize, Transform wallsContainer)
    {
        var left = Instantiate(AboveBelowWall);
        left.position = new Vector3(-_cellSize / 2 + _outWallWidth / 2, currentY / 2 - _wallHeight / 2, 
            currentZ / 2 - _cellSize / 2 + _wallWidth / 2);
        left.localScale = new Vector3(_outWallWidth, currentY + _outWallWidth, 
            (_cellSize - _wallWidth) * xSize + _wallWidth);
        left.parent = wallsContainer;

        var right = Instantiate(left);
        right.position = new Vector3(currentX - _cellSize / 2 + _outWallWidth / 2, left.position.y, left.position.z);
        right.parent = wallsContainer;

        var down = Instantiate(AboveBelowWall);
        down.position = new Vector3(currentX / 2 - _cellSize / 2 + _wallWidth / 2, currentY / 2 - _wallHeight / 2,
            -_cellSize / 2 + _outWallWidth / 2);
        down.localScale = new Vector3(left.lossyScale.z, 
            left.lossyScale.y, left.lossyScale.x);
        down.parent = wallsContainer;

        var up = Instantiate(down);
        up.position = new Vector3(down.position.x, down.position.y, currentZ - _cellSize / 2 + _wallWidth / 2);
        up.parent = wallsContainer;

        float zPos = currentZ / 2 - _cellSize / 2 + _wallWidth / 2;

        if (IsFloorNeeded)
        {
            var bottom = Instantiate(AboveBelowWall);
            bottom.position = new Vector3(down.position.x, -_wallHeight / 2, zPos);
            bottom.localScale = new Vector3((_cellSize - _wallWidth) * ySize + _wallWidth,
                _outWallWidth, (_cellSize - _wallWidth) * xSize + _wallWidth);
            bottom.parent = wallsContainer;
        }

        /*if (IsCeilingNeeded)
        {
            var top = Instantiate(bottom);
            top.position = new Vector3(bottom.position.x, currentY - _wallHeight / 2, bottom.position.z);
            top.parent = wallsContainer;
            top.name = "top";
        }*/

        return new Vector3(down.position.x, left.position.y, zPos);
    }

    public void DrawGrid(int xSize, int ySize, int zSize, Transform mazeObject)
    {
        float currentY = 0, currentX = 0, currentZ = 0;
        Transform WallsContainer = new GameObject("wallsContainer").transform;
        Transform upWall = Instantiate(UpDownWall);
        upWall.gameObject.GetComponent<MeshRenderer>().sharedMaterial = GridMaterial;
        upWall.gameObject.layer = WallsLayer;
        //upWall.localScale = new Vector3(upWall.lossyScale.x * 2, upWall.lossyScale.y, upWall.lossyScale.z);
        //WallPosition upwWallPosition = upWall.gameObject.GetComponent<WallPosition>();

        Transform rightWall = Instantiate(LeftRightWall);
        rightWall.gameObject.GetComponent<MeshRenderer>().sharedMaterial = GridMaterial;
        rightWall.gameObject.layer = WallsLayer;
        //rightWall.localScale = new Vector3(rightWall.lossyScale.x * 2, rightWall.lossyScale.y, rightWall.lossyScale.z);
        //WallPosition rightwWallPosition = rightWall.gameObject.GetComponent<WallPosition>();

        for (int z = 0; z < zSize; z++)
        {
            currentZ = 0;
            for (int x = 0; x < xSize; x++)
            {
                currentX = 0;
                for (int y = 0; y < ySize; y++)
                {
                    if (x != xSize - 1)
                    {
                        //upwWallPosition.Position = new Vector2(x, y); 
                        Transform wall = Instantiate(upWall);
                        wall.position = new Vector3(currentX, currentY, currentZ + _cellSize / 2 - _wallWidth / 2);
                        wall.GetComponent<WallPosition>().Position = new Vector2(x, y);
                        if (y == 0)
                            wall.localScale = new Vector3(wall.lossyScale.x, wall.lossyScale.y, wall.lossyScale.z - _outWallWidth);
                        wall.SetParent(WallsContainer);
                    }
                    if (y != ySize - 1)
                    {
                        //rightwWallPosition.Position = new Vector2(x, y);
                        Transform wall = Instantiate(rightWall);
                        wall.position = new Vector3(currentX + _cellSize / 2 - _wallWidth / 2, currentY, currentZ);
                        wall.GetComponent<WallPosition>().Position = new Vector2(x, y);
                        if (x == 0)
                            wall.localScale = new Vector3(wall.lossyScale.x, wall.lossyScale.y, wall.lossyScale.z - _outWallWidth);
                        wall.SetParent(WallsContainer);
                    }
                    if (z < zSize)
                    {
                        Transform wall = Instantiate(AboveBelowWall);
                        wall.position = new Vector3(currentX, currentY - _wallHeight / 2, currentZ);
                        wall.SetParent(WallsContainer);
                    }
                    currentX += _cellSize - _wallWidth;
                }
                currentZ += _cellSize - _wallWidth;
            }
            currentY += _wallHeight;
        }
        mazeObject.position = DrawOuterWalls(currentX, currentY, currentZ, xSize, ySize, zSize, WallsContainer);
        WallsContainer.SetParent(mazeObject);
        Destroy(upWall.gameObject);
        Destroy(rightWall.gameObject);
    }
}
