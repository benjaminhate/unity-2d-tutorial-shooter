using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public Canvas pauseMenu;
    public bool paused;

	// Use this for initialization
	void Start () {
        paused = false;
        pauseMenu.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            toggleMenu();
        }
	}

    void toggleMenu()
    {
        paused = !paused;
        pauseMenu.enabled = paused;
        if (paused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
}
