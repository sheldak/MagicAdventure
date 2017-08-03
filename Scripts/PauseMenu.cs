using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public string mainMenu;
    public bool isPaused;
    public GameObject pauseMenuCanvas;
    public GameObject player;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(isPaused)
        {
            pauseMenuCanvas.SetActive(true);
            Time.timeScale = 0f;
            player.GetComponent<Player>().enabled = false;
        }
        else
        {
            pauseMenuCanvas.SetActive(false);
            player.GetComponent<Player>().enabled = true;
            Time.timeScale = 1f;
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }
    }

    public void Resume()
    {
        isPaused = false;
    }
    public void QuitToMainMenu()
    {
        SceneManager.LoadScene(mainMenu);
    }
    public void QuitToWindows()
    {
        Application.Quit();
    }
}
