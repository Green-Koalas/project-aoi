using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    public GameObject UI;
    public GameObject pauseMenu;
    public GameObject retryMenu;

    public bool paused = false;
    public bool lost = false;
    public bool lost2 = false;

    private void Start() {
        Time.timeScale = 1f;
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape) && !lost) {
            Change();
        }

        if(lost) {
            Lost();
        }
    }

    private void Lost() {
        if(!lost2) {
            retryMenu.SetActive(true);
            pauseMenu.SetActive(false);
            UI.SetActive(false);
            Time.timeScale = 0f;
        }
    }

    public void Change() {
        if(paused) {
            paused = false;
            Time.timeScale = 1f;
            UI.SetActive(true);
            pauseMenu.SetActive(false);
        }
        else if(!paused) {
            paused = true;
            Time.timeScale = 0f;
            UI.SetActive(false);
            pauseMenu.SetActive(true);
        }
    }

    public void ExitToMainMenu() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void Retry() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
    }
}
