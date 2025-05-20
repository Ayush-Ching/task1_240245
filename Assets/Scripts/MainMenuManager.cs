using UnityEngine;

public class MainMenuManager : MonoBehaviour {

    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject instructions;
    [SerializeField] private GameObject levelSelectScreen;

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
}
