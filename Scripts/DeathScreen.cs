using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathScreen : MonoBehaviour
{

    
    public string mainMenu;
    public Text finalScore;
    private int score;
    public GameObject player;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = 0f;
        score = player.GetComponent<Player>().score;
        ShowScore();
    }

    public void TryAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Restart");
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(mainMenu);
        Debug.Log("Menu");
    }

    void ShowScore()
    {
        finalScore.text = "Your Score: " + score.ToString();
    }
}
