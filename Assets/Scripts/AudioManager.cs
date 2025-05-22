using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour {

    [SerializeField] private AudioClip mainMenuTheme;
    [SerializeField] private AudioClip tutorial;
    [SerializeField] private AudioClip level1_4;
    [SerializeField] private AudioClip level5;
    [SerializeField] private AudioClip level6;

    private AudioSource source;

    private void Awake() {
        source = GetComponent<AudioSource>();
        DontDestroyOnLoad(source);
    }

    private void Start() {
        string scene = SceneManager.GetActiveScene().name;

        if(scene == "Level 0") {
            if (source.clip == tutorial) return;
            source.Stop();
            source.clip = tutorial;
            source.Play();
        }
        else if(scene == "Level 1" || scene == "Level 2" || scene == "Level 3" || scene == "Level 4") {
            if (source.clip == level1_4) return;
            source.Stop();
            source.clip = level1_4;
            source.Play();
        }
        else if (scene == "Level 5") {
            if (source.clip == level5) return;
            source.Stop();
            source.clip = level5;
            source.Play();
        }
        else if (scene == "Level 6") {
            if(source.clip == level6) return;
            source.Stop();
            source.clip = level6;
            source.Play();
        }
        else {
            if (source.clip == mainMenuTheme) return;
            source.Stop();
            source.clip = mainMenuTheme;
            source.Play();
        }
    }
}