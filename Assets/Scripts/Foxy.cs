using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Foxy : MonoBehaviour {

    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D coll;

    [Header("Stats")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpSpeed = 10f;
    [SerializeField] private float scale = 7f;
    [SerializeField] private float gravityScale_up = 2f;
    [SerializeField] private float gravityScale_down = 2.5f;

    //[Header("Booleans")]

    [Header("Layer Masks")]
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private LayerMask wallLayerMask;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
    }

    private void Start() {

    }

    private void Update() {
        float horizontalInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);

        if(horizontalInput > 0f) {
            transform.localScale = Vector3.one * scale;
        }
        else if(horizontalInput < 0f) {
            transform.localScale = new Vector3(-1, 1, 1) * scale;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded()) {
            Jump();
        }

        anim.SetBool("isRunning", horizontalInput != 0);
        anim.SetBool("isGrounded", isGrounded());

        rb.gravityScale = (rb.angularVelocity <= 0f) ? gravityScale_down : gravityScale_up;

        print(onWall());
    }

    private void Jump() {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpSpeed);
        anim.SetTrigger("jump");
    }

    private bool isGrounded() {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0, Vector2.down, 0.1f, groundLayerMask);
        return raycastHit2D.collider != null;
    }

    private bool onWall() {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayerMask);
        return raycastHit2D.collider != null;
    }
}