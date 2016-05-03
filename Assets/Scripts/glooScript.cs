using UnityEngine;
using System.Collections;
using Utils;
using System;

public class glooScript : MonoBehaviour {

    private Animator animator;
    private Rigidbody2D rbody;
    private BoxCollider2D boxcoll;
    private float speed = 3.0f;
    private float jumpForce = 7.0f;
    private int hashLeft = Animator.StringToHash("run_left");
    private int hashRight = Animator.StringToHash("run_right");
    private int hashJumpL = Animator.StringToHash("jump_left");
    private int hashJumpR = Animator.StringToHash("jump_right");
    bool inJump = false;
    public bool recording = false;
    private int facing = 1;

    public GameObject div;
    private BoxCollider2D divcoll;

    // Use this for initialization
    void Start() {
        animator = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody2D>();
        boxcoll = GetComponent<BoxCollider2D>();
        divcoll = div.GetComponent<BoxCollider2D>();
    }        
	
	// Update is called once per frame
	void Update () {
        
        if (!recording) {
            if (Input.GetKeyDown(GlooConstants.keyDivide)) {
                recording = true;
                int facing_int = facing == 1 ? -1 : 1;
                GameObject div_instance = (GameObject) Instantiate(div, transform.position + new Vector3(boxcoll.size.x / 2.0f + divcoll.size.x, 0, 0)*facing_int, new Quaternion());
                div_instance.GetComponent<divScript>().parent = gameObject;
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
            animator.SetBool("Jump", inJump);
			if (Input.GetKeyDown (GlooConstants.keyActivate)) 
			{
				Collider2D[] nearbyObjects = Physics2D.OverlapCircleAll(this.transform.position, boxcoll.size.x/2);
				foreach (Collider2D objColl in nearbyObjects) 
				{
					if (objColl.gameObject.tag == "ManualTrigger") 
					{
						objColl.gameObject.SendMessage ("TriggerActivate");
					}
				}
			}
        }        
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
        if (Input.GetKey(GlooConstants.keyJump) && !inJump && !recording) {
            rbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            inJump = true;
        }
        move *= speed;
        float vy = rbody.velocity.y;
        rbody.velocity = move + new Vector2((inJump && move.x == 0) ? rbody.velocity.x : 0, vy);
    }

    void OnCollisionStay2D(Collision2D coll) {
        foreach(ContactPoint2D contact in coll.contacts)
        {
            if(Math.Abs(contact.normal[0]) < 0.1 && contact.normal[1] > 0)
            {
                inJump = false;
                break;
            }
        }
    }
}
