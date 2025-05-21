using System.Collections;
using UnityEngine;

public class Pillar : MonoBehaviour {

    

    [Space]
    [Header("CheckPoints")]
    [SerializeField] private Key[] keys;

    private Animator anim;

    private void Awake() {
        anim = GetComponent<Animator>();
    }

    private void Start() {
        anim.SetBool("isOpen", false);
    }

    private void Update() {
        int sum = 0;
        for(int i=0; i<keys.Length; i++) {
            sum += keys[i].state;
        }
        if(sum >= keys.Length) {
            for (int i = 0; i < keys.Length; i++) {
                keys[i].state = 2;
                keys[i].defState = 2;
                keys[i].renderColor();
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