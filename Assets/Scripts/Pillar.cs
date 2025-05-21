using System.Collections;
using UnityEngine;

public class Pillar : MonoBehaviour {

    

    [Space]
    [Header("CheckPoints")]
    [SerializeField] private CheckPoint[] checkPoints;

    private Animator anim;

    private void Awake() {
        anim = GetComponent<Animator>();
    }

    private void Start() {
        anim.SetBool("isOpen", false);
    }

    private void Update() {
        int sum = 0;
        for(int i=0; i<checkPoints.Length; i++) {
            sum += checkPoints[i].state;
        }
        if(sum >= checkPoints.Length) {
            for (int i = 0; i < checkPoints.Length; i++) {
                checkPoints[i].state = 2;
                checkPoints[i].defState = 2;
                checkPoints[i].renderColor();
            }
            StartCoroutine(OpenPillar());
        }
    }

    private IEnumerator OpenPillar() {
        anim.SetBool("isOpen", true);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}