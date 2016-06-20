using UnityEngine;
using System.Collections;
using System;

public class RushScript : KillingClass, GlooGenericObject
{
	private int distanceDeDetection = 20;
	private Rigidbody2D body;
	private BoxCollider2D boxcoll;
	private float speed = 7.0f;
    private Animator animator;

    private class RushData
    {
        public Vector2 velocity;
        public Vector2 position;

        public RushData(Vector2 vel, Vector3 pos)
        {
            velocity = vel;
            position = pos;
        }
    }


    // Use this for initialization
    override public void Start () 
	{
		base.Start ();
		boxcoll = GetComponent<BoxCollider2D> ();
		body = GetComponent<Rigidbody2D> ();
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		RaycastHit2D left = Physics2D.Raycast(new Vector2(this.transform.position.x, this.transform.position.y - 2f / 3 * (boxcoll.size.x * this.transform.localScale.y / 2)), new Vector2(-1,0));
        RaycastHit2D right = Physics2D.Raycast(new Vector2(this.transform.position.x, this.transform.position.y - 2f / 3 * (boxcoll.size.x * this.transform.localScale.y / 2)), new Vector2(1, 0));
        if (body.velocity == new Vector2(0, 0))
        {
            if (left)
            {
                if (left.collider.gameObject.tag == "Gloo" || left.collider.gameObject.tag == "GlooDiv")
                {
                    animator.SetBool("Move",true);
                    body.velocity = new Vector2(-speed, 0);
                }
            }

            if (right)
            {
                if (right.collider.gameObject.tag == "Gloo" || right.collider.gameObject.tag == "GlooDiv")
                {
                    animator.SetBool("Move", true);
                    body.velocity = new Vector2(speed, 0);
                }
            }
        }
        if (body.velocity.x==0) {
            animator.SetBool("Move", false);
        }
    }

    public object getData()
    {
        return new RushData(body.velocity, this.transform.position);
    }

    public void setData(object savedData)
    {
        RushData data = savedData as RushData;
        this.transform.position = data.position;
        body.velocity = data.velocity;
    }
}
