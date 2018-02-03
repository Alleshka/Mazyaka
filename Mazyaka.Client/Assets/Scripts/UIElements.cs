using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIElements : MonoBehaviour
{
    [SerializeField]
    private GameMaster _gm;

    [SerializeField]
    private Button _saveMazeButton;
	// Use this for initialization
	void Start ()
	{
        _gm.GetComponent<MenuController>().PauseEvent += PauseEvent;
	}

    private void PauseEvent()
    {
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
	
	}

    public void SaveConstructedMaze()
    {
        _gm.StartGame();
    }
}
