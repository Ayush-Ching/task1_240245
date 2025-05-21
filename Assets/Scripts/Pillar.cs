using UnityEngine;

public class Pillar : MonoBehaviour {

    [Header("Colors")]
    [SerializeField] private Color initColor;
    [SerializeField] private Color touchedColor;
    [SerializeField] private Color openColor;

    [Space]
    [Header("CheckPoints")]
    [SerializeField] private GameObject[] checkPoints;

    private Animator anim;

    private void Awake() {
        anim = GetComponent<Animator>();
    }

    private void Start() {
        for(int i=0; i<checkPoints.Length; i++) {
            checkPoints[i].GetComponent<SpriteRenderer>().color = initColor;
        }
    }

    private void Update() {
        
    }

    private void 
}