using UnityEngine;
using System.Collections;
using Assets.Scripts._3DMaze;
using UnityEngine.EventSystems;

[RequireComponent(typeof(DrawLab))]
[RequireComponent(typeof(MazeConstructor))]
[RequireComponent(typeof(MenuController))]
public class GameMaster : MonoBehaviour
{
    private MazeGenerator.Flags[,,] _mazeA;
    private MazeGenerator.Flags[,,] _mazeB;
    private MazeGenerator _mazeGenerator;
    private MazeConstructor _mazeConstructor;
    private DrawLab _drawLab;
    private MenuController _menuController;

    [SerializeField]
    private GameObject _mainCamera;

    [SerializeField]
    private GameObject _constructorCamera;

    [SerializeField]
    private bool _autoGenerate;

    [SerializeField]
    private Transform ball;

    [SerializeField]
    private RectTransform MazeMap;

    private Transform mazeATransform;
    private Transform mazeBTransform;

    [SerializeField]
    [Range(1, 20)]
    private int MazeSizeX;

    [SerializeField]
    [Range(1, 20)]
    private int MazeSizeY;

    [SerializeField]
    [Range(1, 20)]
    private int MazeSizeZ;

    [SerializeField]
    private float scale;

    [SerializeField]
    private bool autoFitMap;

    void Awake()
    {
        _mazeConstructor = GetComponent<MazeConstructor>();
        _drawLab = GetComponent<DrawLab>();
        _menuController = GetComponent<MenuController>();
    }

    // Use this for initialization
    void Start ()
    {
        _menuController.PauseEvent += PauseEvent;
        mazeBTransform = new GameObject("MazeB").transform;
        if (!_autoGenerate)
        {
            ActivateConstructor();
            return;
        }

        _mazeGenerator = new MazeGenerator(scale);
        _mazeA = _mazeGenerator.GenerateMaze(MazeSizeX, MazeSizeY, MazeSizeZ);
        _mazeB = _mazeGenerator.GenerateMaze(MazeSizeX, MazeSizeY, MazeSizeZ);
        mazeATransform = new GameObject("MazeA").transform;
        _drawLab.Draw(_mazeA, mazeATransform);
        
        DrawMazeMap(autoFitMap);
    }

    private void PauseEvent()
    {
        Debug.Log("paused");
    }

    // Update is called once per frame
    void Update ()
    {
      
    }

    public void StartGame()
    {
        _mazeConstructor.enabled = false;
        _constructorCamera.SetActive(false);
        _mainCamera.SetActive(true);
        MazeMap.gameObject.SetActive(true);
        Destroy(mazeBTransform.gameObject);
        mazeBTransform = new GameObject("MazeB").transform;
        DrawMazeMap(true);
    }

    private void ActivateConstructor()
    {
        _mazeConstructor.enabled = true;
        _mazeB = MazeGenerator.GetEmptyMaze(MazeSizeX, MazeSizeY, MazeSizeZ);
        _drawLab = GetComponent<DrawLab>();
        _drawLab.DrawGrid(MazeSizeX, MazeSizeY, MazeSizeZ, mazeBTransform);
        _mazeConstructor.SetMaze(_mazeB);
        _mainCamera.SetActive(false);
        _constructorCamera.SetActive(true);
    }

    private void DrawMazeMap(bool autoFit)
    {
        if (autoFit)
        {
            var size = MazeMap.sizeDelta;
            _drawLab.AutoFit(size.x, size.y, MazeSizeX, MazeSizeY);
        }
        _drawLab.Draw2D(_mazeB, mazeBTransform);
        mazeBTransform.position = MazeMap.position;
        mazeBTransform.SetParent(MazeMap);
    }
}
