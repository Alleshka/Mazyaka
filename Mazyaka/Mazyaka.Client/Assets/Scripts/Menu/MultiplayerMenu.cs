using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MultiplayerMenu : MonoBehaviour
{

    [SerializeField]
    private int _multiplayerSceneIndex;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void MazeConstructor()
    {
        SceneManager.LoadScene(_multiplayerSceneIndex);
    }

    public void CreateLobby()
    {
        Debug.Log("Create lobby");
    }

    public void JoinLobby()
    {
        Debug.Log("Join lobby");
    }
}
