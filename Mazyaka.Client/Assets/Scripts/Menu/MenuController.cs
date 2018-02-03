using System;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private Button[] _backButtons;

    [SerializeField]
    private GameObject _pauseMenu;

    public event Action PauseEvent;

    // Use this for initialization
    void Start()
    { 
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Cancel"))
        {
            InvokeClickOnButton();
        }
    }

    public void InvokeClickOnButton()
    {
        foreach (var backButton in _backButtons)
        {
            if (backButton.isActiveAndEnabled)
            {
                backButton.onClick.Invoke();
                return;
            }
        }

        if(PauseEvent != null)
            PauseEvent.Invoke();
        if(_pauseMenu!= null)
            _pauseMenu.SetActive(true);
    }
}
