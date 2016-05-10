using UnityEngine;
using System.Collections;

public class RushScript : OstileClass 
{
	private int distanceDeDetection = 20;
	private Rigidbody2D body;
	private BoxCollider2D boxcoll;
	private float speed = 10.0f;

	// Use this for initialization
	void Start () 
	{
		base.Start ();
		boxcoll = GetComponent<BoxCollider2D> ();
		body = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
//		Physics2D.Raycast ();
//		body.velocity =  + new Vector2((data.inJump && move.x == 0) ? rbody.velocity.x : 0, vy);
	}
}
