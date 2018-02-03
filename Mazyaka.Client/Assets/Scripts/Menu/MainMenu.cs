using UnityEngine;
using System.Collections;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void PlayGame()
    {
       
    }

    public void Settings()
    {
        
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
