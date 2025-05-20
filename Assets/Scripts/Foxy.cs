using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Foxy : MonoBehaviour {

    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;

    [Header("Constants")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpSpeed = 10f;
    [SerializeField] private float climbSpeed = 10f;
    [SerializeField] private float dashSpeed = 20f;
    [SerializeField] private float scale = 7f;
    [SerializeField] private float gravityScale_up = 2f;
    [SerializeField] private float gravityScale_down = 2.5f;
    [SerializeField] private float dashDamping = 2.0f;
    [SerializeField] private float deathWaitTime = 1f;

    //[Header("Booleans")]
    private bool canDash;
    private bool canMove;
    private bool hitSpike;
    private bool hasWon;

    [Header("Layer Masks")]
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private LayerMask wallLayerMask;
    [SerializeField] private LayerMask spikesLayerMask;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        hitSpike = false;
        canMove = true;
        anim.SetBool("isDead", false);
    }

    private void Update() {
        float horizontalInput = 0;
        float verticalInput = 0;

        if (Input.GetKey(KeyCode.LeftArrow)) {
            horizontalInput = -1f;
        }
        if (Input.GetKey(KeyCode.RightArrow)) {
            horizontalInput = 1f;
        }

        if (Input.GetKey(KeyCode.UpArrow)) {
            verticalInput = 1f;
        }
        if (Input.GetKey(KeyCode.DownArrow)) {
            verticalInput = -1f;
        }

        if (isGrounded()) {
            canDash = true;
        }

        if(horizontalInput > 0f) {
            transform.localScale = Vector3.one * scale;
        }
        else if(horizontalInput < 0f) {
            transform.localScale = new Vector3(-1, 1, 1) * scale;
        }

        if (isTouchingWall() && isGrounded()) {
            horizontalInput = (horizontalInput == transform.localScale.x) ? 0 : horizontalInput;
        }
        else if (isTouchingWall()) {
            horizontalInput = 0;
        }

        if (canMove) rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.C)) && isGrounded()) {
            if (canMove) rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpSpeed);
        }

        anim.SetBool("isRunning", horizontalInput != 0);
        anim.SetBool("isGrounded", isGrounded());
        anim.SetBool("isGoingUp", rb.linearVelocity.y > 0f);

        rb.gravityScale = (rb.angularVelocity <= 0f) ? gravityScale_down : gravityScale_up;

        if(isTouchingWall() && Input.GetKey(KeyCode.Z)) {
            anim.SetBool("isOnWall", true);
            rb.gravityScale = 0f;
            if (canMove) rb.linearVelocity = new Vector2(0, verticalInput * climbSpeed);
            anim.SetBool("isClimbing", verticalInput != 0);
        }
        else {
            anim.SetBool("isClimbing", false);
            anim.SetBool("isOnWall", false);
        }

        if(canMove && canDash && Input.GetKeyDown(KeyCode.X)) {
            Vector2 dir = new Vector2(horizontalInput, verticalInput);
            canDash = false;
            StartCoroutine(Dash(dir));
            rb.linearDamping = 0;
        }

        if (hitSpike) {
            StartCoroutine(Die());
        }
    }
    private bool isGrounded() {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0, Vector2.down, 0.1f, groundLayerMask);
        return raycastHit2D.collider != null;
    }

    private bool isTouchingWall() {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayerMask);
        return raycastHit2D.collider != null;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Spike") {
            hitSpike = true;
        }
        else {
            hitSpike = false;
        }
    }

    private IEnumerator Dash(Vector2 dir) {
        anim.SetBool("isDashing", true);
        float elapsedTime = 0f;
        while(elapsedTime < 0.3f) {
            elapsedTime += Time.deltaTime;

            canDash = false;
            rb.linearVelocity = dir.normalized * dashSpeed;
            rb.linearDamping = dashDamping;

            yield return null;
        }
        rb.linearDamping = 0;
        anim.SetBool("isDashing", false);
        
        yield return null;
    }

    private IEnumerator Die() {
        anim.SetBool("isDead", true);
        canMove = false;
        rb.linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(deathWaitTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}