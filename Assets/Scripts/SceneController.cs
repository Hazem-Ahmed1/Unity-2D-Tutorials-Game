using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject winPanelUI;
    public GameObject losePanelUI;
    public GameObject tipPanelUI;
    private bool isPaused = false;


    void Start()
    {
        pauseMenuUI.SetActive(false);
        winPanelUI.SetActive(false);
        losePanelUI.SetActive(false);

        tipPanelUI.SetActive(true);
        StartCoroutine(HideTipPanel());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }
    private IEnumerator HideTipPanel()
    {
        yield return new WaitForSeconds(3f);
        tipPanelUI.SetActive(false);
    }


    public void TogglePause()
    {
        isPaused = !isPaused;
        pauseMenuUI.SetActive(isPaused);

        Time.timeScale = isPaused ? 0f : 1f;
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Win()
    {
        winPanelUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Lose()
    {
        losePanelUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Restart(){
        SceneManager.LoadScene(SceneManager
        .GetActiveScene()
        .name);
        Time.timeScale = 1f;
    }
    public void NextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
            Time.timeScale = 1f; // Reset time scale for the next level
        }

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
