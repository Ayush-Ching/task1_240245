using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour {

    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject instructions;
    [SerializeField] private GameObject levelSelectScreen;

    [Space]
    [Header("Level Buttons")]
    [SerializeField] private Button[] buttons;

    private void Awake() {
        mainMenu.SetActive(true);
        instructions.SetActive(false);
        levelSelectScreen.SetActive(false);
    }

    public void LevelSelectScreen() {
        mainMenu.SetActive(false);
        instructions.SetActive(false);
        levelSelectScreen.SetActive(true);
    }

    public void InstructionsScreen() {
        mainMenu.SetActive(false);
        instructions.SetActive(true);
        levelSelectScreen.SetActive(false);
    }

    public void MainMenu() {
        mainMenu.SetActive(true);
        instructions.SetActive(false);
        levelSelectScreen.SetActive(false);
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void LevelSelect(int level) {
        SceneManager.LoadScene("Level " +  level);
    }
}
