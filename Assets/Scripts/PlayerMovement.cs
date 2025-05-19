using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private Rigidbody2D rb;

    [Header("Stats")]
    [SerializeField] private float speed = 10f;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start() {

    }

    private void Update() {
        rb.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * 10, rb.linearVelocity.y);

        if (Input.GetKeyDown(KeyCode.Space)) {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, speed);
        }
    }
}