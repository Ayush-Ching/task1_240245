using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Foxy : MonoBehaviour {

    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D coll;

    [Header("Stats")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float scale = 7f;

    //[Header("Booleans")]

    [Header("Layer Masks")]
    [SerializeField] private LayerMask groundLayerMask;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
    }

    private void Start() {

    }

    private void Update() {
        float horizontalInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(horizontalInput * 10, rb.linearVelocity.y);

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
    }

    private void Jump() {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, moveSpeed);
        anim.SetTrigger("jump");
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Ground") {
        }
    }

    private bool isGrounded() {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0, Vector2.down, 0.2f, groundLayerMask);
        return raycastHit2D.collider != null;
    }
}