using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    [SerializeField]
    private GameObject _background;

	// Use this for initialization
	void Start () {
	}

    void OnEnable()
    {
        _background.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void Resume()
    {
        Debug.Log("Resume");
    }
}
