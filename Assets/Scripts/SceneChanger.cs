using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {

    [SerializeField] private float changeTime;
    [SerializeField] private string sceneName;

    private void Update() {
        while(changeTime > 0) {
            changeTime -= Time.deltaTime;
            return;
        }
        SceneManager.LoadScene(sceneName);
    }



}