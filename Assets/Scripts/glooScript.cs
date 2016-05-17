﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Utils;
using System;
using UnityEngine.SceneManagement;

public class glooScript : MonoBehaviour, GlooGenericObject {

    private Animator animator;
    private Rigidbody2D rbody;
    public BoxCollider2D boxcoll;
    public BoxCollider2D divcoll;
    private float speed = 3.0f;
    private float jumpForce = 7.0f;
    private int hashLeft = Animator.StringToHash("run_left");
    private int hashRight = Animator.StringToHash("run_right");
    private int hashJumpToR = Animator.StringToHash("JumpToStandR");
    private int hashJumpToL = Animator.StringToHash("JumpToStandL");
    private int hashJumpL = Animator.StringToHash("jump_left");
    private int hashJumpR = Animator.StringToHash("jump_right");
    private int hashIdleLeft = Animator.StringToHash("stand_left");
    private int hashIdleRight = Animator.StringToHash("stand_right");
    /*bool inJump = false;
    public bool recording = false;*/
    public int facing = 1;

    private class glooData {

        public bool inJump = false;
        public bool recording = false;
        public Vector3 position;
        public string createFace;

        public glooData() {

        }

        public glooData(glooData model) {
            inJump = model.inJump;
            recording = model.recording;
            position = model.position;
            createFace = model.createFace;
        }        
    }

    private glooData data = new glooData();

    public GameObject div;
    public static int divID = 0;

    // Use this for initialization
    void Start() {
        animator = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody2D>();
        boxcoll = GetComponent<BoxCollider2D>();
        divcoll = div.GetComponent<BoxCollider2D>();
    }        
	
	// Update is called once per frame
	void Update () {
        
        if (!data.recording) {
            int currentHash = animator.GetCurrentAnimatorStateInfo(0).shortNameHash;
            if (Input.GetKeyDown(GlooConstants.keyDivide) && (currentHash == hashIdleLeft || currentHash == hashIdleRight)) {
                data.recording = true;
                animator.SetBool("DoCreate", true);
                /*int facing_int = facing == 1 ? -1 : 1;
                Instantiate(div, transform.position + new Vector3(boxcoll.size.x / 2.0f + divcoll.size.x, 0, 0)*facing_int, new Quaternion());*/
                if (facing == 1) {
                    animator.SetTrigger("CreateLeft");
                }else {
                    animator.SetTrigger("CreateRight");
                }
            }
            bool right = Input.GetKey(GlooConstants.keyRight);
            bool left = Input.GetKey(GlooConstants.keyLeft);
            if (left) {
                facing = 1;
            } else if (right) {
                facing = 2;
            }
            animator.SetBool("GoLeft", left);
            animator.SetBool("GoRight", right && !left);
            animator.SetBool("Jump", data.inJump);
			if (Input.GetKeyDown (GlooConstants.keyActivate)) 
			{
				Collider2D[] nearbyObjects = Physics2D.OverlapCircleAll(this.transform.position, boxcoll.size.x/2 * this.transform.localScale.x);
				foreach (Collider2D objColl in nearbyObjects) 
				{
					if (objColl.gameObject.tag == "ManualTrigger") 
					{
						objColl.gameObject.SendMessage ("TriggerActivate");
					}
				}
			}

            if (Input.GetKeyDown(GlooConstants.keyTest)) {
            }
        }        
    }

    void FixedUpdate() {
        /*
        // check floor orientation
        List<Vector2> hitPoints = new List<Vector2>();
        float moy_x = 0.0f;
        float moy_y = 0.0f;
        float double_product_sum = 0.0f;
        float square_sum = 0.0f;
        Vector2 linePoint;
        Vector2 lineDirection;

        for(int i = 0; i < 500; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(-4.5f + i*9.0f/499, 5.0f, 0.0f), -transform.up, 6.0f);
            if (hit.collider != null)
            {
                hitPoints.Add(hit.point);
                moy_x += hit.point.x;
                moy_y += hit.point.y;
                double_product_sum += hit.point.x * hit.point.y;
                square_sum += (float) Math.Pow(hit.point.x, 2);
            }
        }

        if (hitPoints.Count > 2)
        {
            moy_x /= hitPoints.Count;
            moy_y /= hitPoints.Count;
            double_product_sum /= hitPoints.Count;
            square_sum /= hitPoints.Count;

            float a = (double_product_sum - moy_x * moy_y)/(square_sum - (float) Math.Pow(moy_x, 2));
            float b = moy_y - a * moy_x;

            linePoint = new Vector2(0.0f, b);
            lineDirection = new Vector2(1.0f, a);

            float angle = Vector2.Angle(transform.right, lineDirection);
            print(angle);
            transform.Rotate(new Vector3(0.0f, 0.0f, angle));
        }

        else
        {
            transform.Rotate(new Vector3(0.0f, 0.0f, Vector2.Angle(transform.right, Vector2.right)));
        }*/

        Vector2 move = new Vector2(0, 0);
        int currentHash = animator.GetCurrentAnimatorStateInfo(0).shortNameHash;
        if (currentHash == hashLeft || (currentHash== hashJumpToL && Input.GetKey(GlooConstants.keyLeft)) || (currentHash == hashJumpL && Input.GetKey(GlooConstants.keyLeft))) {
            move += new Vector2(-1, 0);
        }
      
        if (currentHash == hashRight || (currentHash == hashJumpToR && Input.GetKey(GlooConstants.keyRight)) || (currentHash == hashJumpR && Input.GetKey(GlooConstants.keyRight))) {
            move += new Vector2(1, 0);
        }

        if (currentHash == hashJumpL && Input.GetKey(GlooConstants.keyRight))
        {
            move += new Vector2(0.5f, 0);
        }
        if (currentHash == hashJumpR && Input.GetKey(GlooConstants.keyLeft))
        {
            move += new Vector2(-0.5f, 0);
        }

        if (Input.GetKey(GlooConstants.keyJump) && !data.inJump && !data.recording) {
            rbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            data.inJump = true;
        }
        move *= speed;
        float vy = rbody.velocity.y;
        rbody.velocity = move + new Vector2((data.inJump && move.x == 0) ? rbody.velocity.x : 0, vy);
    }

    void OnCollisionStay2D(Collision2D coll) {

        if (!data.inJump)
            return;

        foreach (ContactPoint2D contact in coll.contacts)
        {
			if(contact.normal[1] > 0.7)
            {
                data.inJump = false;
                break;
            }
        }
    }

    public object getData() {
        glooData toSave = new glooData(data);
        toSave.recording = false;
        toSave.position = transform.position;
        toSave.createFace = facing == 1 ? "CreateLeft" : "CreateRight";
        return toSave;
    }

    public void setData(object savedData) {
        data = (glooData) savedData;
        transform.position = data.position;
        animator.SetBool("DoCreate", false);
        animator.SetTrigger(data.createFace);
    }
}
