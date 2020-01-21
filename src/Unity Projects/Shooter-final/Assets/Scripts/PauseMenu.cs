using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [Header("Pause Menu reference")]
    public Canvas pauseMenu;
    private bool _paused;
    // Start is called before the first frame update
    private void Start()
    {
        pauseMenu.enabled = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    private void Pause()
    {
        _paused = !_paused;
        pauseMenu.enabled = _paused;
        Time.timeScale = _paused ? 0f : 1f;
    }
}
