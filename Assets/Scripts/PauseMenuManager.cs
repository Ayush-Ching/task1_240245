using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour {

    [SerializeField] private GameObject pausedMenupanel;

    private bool isPaused;

    private void Start() {
        pausedMenupanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            if (isPaused) {
                ResumeGame();
            }
            else if(!isPaused) {
                PauseGame();
            }
        }
    }

    public void PauseGame() {
        isPaused = true;
        pausedMenupanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame() {
        isPaused = false;
        pausedMenupanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void HomeButton() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }

    public void RestartGame() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}