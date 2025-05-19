using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private Rigidbody2D rb;

    [Header("Stats")]
    [SerializeField] private float moveSpeed = 10f;

    [Header("Booleans")]
    [SerializeField] private bool isInAir;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        isInAir = false;
    }

    private void Start() {

    }

    private void Update() {
        float horizontalInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(horizontalInput * 10, rb.linearVelocity.y);

        if(horizontalInput > 0f) {
            transform.localScale = Vector3.one;
        }
        else if(horizontalInput < 0f) {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, moveSpeed);
        }
    }
}