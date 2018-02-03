using UnityEngine;
using System.Collections;
using Assets.Scripts._3DMaze;

[RequireComponent(typeof(GameMaster))]
public class MazeConstructor : MonoBehaviour
{

    [SerializeField]
    private new Camera camera;

    [SerializeField]
    private Material WallMaterialBlank;

    [SerializeField]
    private Material WallMaterialTransparent;

    [SerializeField]
    private LayerMask _wallsMask;
    // Use this for initialization

    [SerializeField]
    private GameObject _saveMazeButton;

    private MazeGenerator.Flags[,,] _maze;

    void Start ()
	{
	    //GameMaster gm = GetComponent<GameMaster>();
        _saveMazeButton.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
	    
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 500, _wallsMask))
            {
                Transform objectHit = hit.transform;

                if (objectHit.GetComponent<MeshRenderer>().sharedMaterial == WallMaterialTransparent)
                {
                    objectHit.GetComponent<MeshRenderer>().sharedMaterial = WallMaterialBlank;
                    Vector2 position = objectHit.GetComponent<WallPosition>().Position;
                    if (objectHit.rotation == Quaternion.Euler(0, 0, 0))
                        _maze[(int) position.x, (int) position.y, 0] |= MazeGenerator.Flags.WALL_RIGHT;
                    else
                        _maze[(int)position.x, (int)position.y, 0] |= MazeGenerator.Flags.WALL_UP;
                    //Debug.Log(_maze[(int)position.x, (int)position.y, 0]);
                    //Debug.Log(position);
                }
                else
                {
                    objectHit.GetComponent<MeshRenderer>().sharedMaterial = WallMaterialTransparent;
                    Vector2 position = objectHit.GetComponent<WallPosition>().Position;
                    if (objectHit.rotation == Quaternion.Euler(0, 0, 0))
                        _maze[(int)position.x, (int)position.y, 0] ^= MazeGenerator.Flags.WALL_RIGHT;
                    else
                        _maze[(int)position.x, (int)position.y, 0] ^= MazeGenerator.Flags.WALL_UP;
                    //Debug.Log(_maze[(int)position.x, (int)position.y, 0]);
                }
                //Destroy(objectHit.gameObject);
            }
        }
    }

    public void SetMaze(MazeGenerator.Flags[,,] maze)
    {
        _maze = maze;
    }
}
