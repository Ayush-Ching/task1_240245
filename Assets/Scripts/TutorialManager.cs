using UnityEngine;

public class TutorialManager : MonoBehaviour {

    [SerializeField] private Transform foxy;

    [Space]
    [SerializeField] private GameObject jumpTut;
    [SerializeField] private GameObject dashTut;
    [SerializeField] private GameObject wallClimbTut;
    [SerializeField] private GameObject keyTut;

    private void Start() {
        jumpTut.SetActive(false);
        dashTut.SetActive(false);
        wallClimbTut.SetActive(false);
        keyTut.SetActive(false);
    }

    private void Update() {
        if(foxy.position.x >= 5 && foxy.position.x < 15) {
            jumpTut .SetActive(true);
        }
        if (foxy.position.x >= 15 && foxy.position.x < 32) {
            jumpTut.SetActive(false);
            dashTut .SetActive(true);
        }
        if (foxy.position.x >= 32 && foxy.position.x < 41) {
            dashTut.SetActive(false);
            wallClimbTut.SetActive(true);
        }
        if (foxy.position.x >= 41 && foxy.position.x < 64) {
            wallClimbTut.SetActive(false);
            keyTut .SetActive(true);
        }
        if(foxy.position.x >= 64) {
            keyTut.SetActive(true);
        }
    }
}