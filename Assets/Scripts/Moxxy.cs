using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moxxy : MonoBehaviour {
    public Transform player;
    public Foxy foxy;
    public Animator animator;
    public float delaySeconds = 1.0f;

    private Queue<RecordedFrame> frames = new Queue<RecordedFrame>();
    private float startTime;
    private bool initialized = false;
    private Vector3 initialEnemyPos;
    private Vector3 initialPlayerPos;

    public bool canMove;

    private class RecordedFrame {
        public Vector3 position;
        public Vector3 localScale;
        public float timestamp;
        public bool isGrounded;
        public bool isRunning;
        public bool isOnWall;
        public bool isDashing;
        public bool isGoingUp;

        public RecordedFrame(Vector3 pos, Vector3 scale, float time,
                             bool grounded, bool running, bool onWall, bool dashing, bool goingUp) {
            position = pos;
            localScale = scale;
            timestamp = time;
            isGrounded = grounded;
            isRunning = running;
            isOnWall = onWall;
            isDashing = dashing;
            isGoingUp = goingUp;
        }
    }

    void Start() {
        startTime = Time.time;
        initialEnemyPos = transform.position;
        initialPlayerPos = player.position;
        canMove = true;
    }

    void Update() {
        if (!canMove) return;

        float currentTime = Time.time;
        float elapsed = currentTime - startTime;

        frames.Enqueue(new RecordedFrame(
            player.position,
            player.localScale,
            currentTime,
            foxy.isGrounded,
            foxy.isRunning,
            foxy.isOnWall,
            foxy.isDashing,
            foxy.isGoingUp
        ));

        if (elapsed < delaySeconds) {
            float t = elapsed / delaySeconds;
            transform.position = Vector3.Lerp(initialEnemyPos, initialPlayerPos, t);
            return;
        }

        if (!initialized) {
            initialized = true;
            transform.position = initialPlayerPos;
        }

        while (frames.Count > 0 && frames.Peek().timestamp < currentTime - delaySeconds) {
            RecordedFrame frame = frames.Dequeue();
            if (frames.Count == 0 || frames.Peek().timestamp >= currentTime - delaySeconds) {
                transform.position = frame.position;
                transform.localScale = frame.localScale;

                animator.SetBool("isGrounded", frame.isGrounded);
                animator.SetBool("isRunning", frame.isRunning);
                animator.SetBool("isOnWall", frame.isOnWall);
                animator.SetBool("isDashing", frame.isDashing);
                animator.SetBool("isGoingUp", frame.isGoingUp);
            }
        }
    }
}
