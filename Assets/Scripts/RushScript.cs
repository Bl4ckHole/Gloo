﻿using UnityEngine;
using System.Collections;
using System;

public class RushScript : OstileClass, GlooGenericObject
{
	private int distanceDeDetection = 20;
	private Rigidbody2D body;
	private BoxCollider2D boxcoll;
	private float speed = 10.0f;

    // Use this for initialization
    override public void Start () 
	{
		base.Start ();
		boxcoll = GetComponent<BoxCollider2D> ();
		body = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		RaycastHit2D left = Physics2D.Raycast(new Vector2(this.transform.position.x, this.transform.position.y), new Vector2(-1,0));
        RaycastHit2D right = Physics2D.Raycast(new Vector2(this.transform.position.x, this.transform.position.y), new Vector2(1, 0));

        if (body.velocity == new Vector2(0, 0))
        {
            if (left)
            {
                if (left.collider.gameObject.tag == "Gloo" || left.collider.gameObject.tag == "GlooDiv")
                {
                    body.velocity = new Vector2(-10, 0);
                }
            }

            if (right)
            {
                if (right.collider.gameObject.tag == "Gloo" || right.collider.gameObject.tag == "GlooDiv")
                {
                    body.velocity = new Vector2(10, 0);
                }
            }
        }
    }

    public object getData()
    {
        throw new NotImplementedException();
    }

    public void setData(object savedData)
    {
        throw new NotImplementedException();
    }
}
