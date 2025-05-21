using UnityEngine;

public class Key : MonoBehaviour {

    [Header("Colors")]
    [SerializeField] private Color initColor;
    [SerializeField] private Color touchedColor;
    [SerializeField] private Color openColor;

    public int state = 0;
    public int defState = 1;

    private SpriteRenderer spriteRenderer;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        spriteRenderer.color = initColor;
    }

    public void changeState(int newState) {
        state = newState;
        renderColor();
    }

    public void renderColor() {
        if(state == 0) {
            spriteRenderer.color = initColor;
        }
        else if(state == 1) {
            spriteRenderer.color = touchedColor;
        }
        else {
            spriteRenderer.color = openColor;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Foxy") {
            state = defState;
            renderColor();
        }
    }

}