﻿using UnityEngine;
using System.Collections;
using Utils;

public class glooScript : MonoBehaviour {

    private Animator animator;
    private Rigidbody2D rbody;
    private float speed = 1.5f;
    private int hashLeft = Animator.StringToHash("run_left");
    private int hashRight = Animator.StringToHash("run_right");
    private int hashJumpL = Animator.StringToHash("jump_left");
    private int hashJumpR = Animator.StringToHash("jump_right");
    bool inJump = false;

    // Use this for initialization
    void Start() {
        animator = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody2D>();
    }        
	
	// Update is called once per frame
	void Update () {
        bool right = Input.GetKey(GlooConstants.keyRight);
        bool left = Input.GetKey(GlooConstants.keyLeft);
        animator.SetBool("GoLeft", left);
        animator.SetBool("GoRight", right && !left);
        animator.SetBool("Jump", inJump);
    }

    void FixedUpdate() {
        Vector2 move = new Vector2(0, 0);
        int currentHash = animator.GetCurrentAnimatorStateInfo(0).shortNameHash;
        if (currentHash == hashLeft || (currentHash == hashJumpL && Input.GetKey(GlooConstants.keyLeft))) {
            move += new Vector2(-1, 0);
        }
        if (currentHash == hashRight || (currentHash == hashJumpR && Input.GetKey(GlooConstants.keyRight))) {
            move += new Vector2(1, 0);
        }
        if (Input.GetKey(GlooConstants.keyJump) && !inJump) {
            rbody.AddForce(new Vector2(0, 3.5f), ForceMode2D.Impulse);
            inJump = true;
        }
        move *= speed;
        float vy = rbody.velocity.y;
        rbody.velocity = move + new Vector2((inJump && move.x == 0) ? rbody.velocity.x : 0, vy);
    }

    void OnCollisionStay2D(Collision2D coll) {
        if (coll.relativeVelocity.y < 0.16) {
            inJump = false;
        }
    }
}