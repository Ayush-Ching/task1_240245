using UnityEngine;

public class Player : MonoBehaviour {

    private Rigidbody2D rb;

    [Space]
    [Header("Stats")]
    public float speed = 10;
    public float jumpForce = 50;
    public float slideSpeed = 5;
    public float wallJumpLerp = 10;
    public float dashSpeed = 20;

    [Space]
    [Header("Booleans")]
    [SerializeField] private bool canMove;
    [SerializeField] private bool wallGrab;
    [SerializeField] private bool wallJumped;
    [SerializeField] private bool wallSlide;
    [SerializeField] private bool isDashing;

    [Space]

    private bool groundTouch;
    private bool hasDashed;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        canMove = true;
        wallGrab = false;
        wallJumped = false;
        wallSlide = false;
        isDashing = false;
    }

    private void Update() {
        float x = 0, y = 0;
        if (Input.GetKey(KeyCode.W)) {
            y = +1f;
        }
        if (Input.GetKey(KeyCode.A)) {
            x = -1f;
        }
        if (Input.GetKey(KeyCode.S)) {
            y = -1f;
        }
        if (Input.GetKey(KeyCode.D)) {
            x = +1f;
        }

        Vector2 moveDir = new Vector2(x, y);
        Walk(moveDir);
    }

    private void Walk(Vector2 moveDir) {
        if (!canMove)
            return;

        if (wallGrab)
            return;

        if (!wallJumped) {
            rb.linearVelocity = new Vector2(moveDir.x * speed, rb.linearVelocity.y);
        }
        else {
            rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, (new Vector2(moveDir.x * speed, rb.linearVelocity.y)), wallJumpLerp * Time.deltaTime);
        }
    }

}