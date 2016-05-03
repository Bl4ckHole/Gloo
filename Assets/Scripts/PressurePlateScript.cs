using UnityEngine;
using System.Collections;

public class PressurePlateScript : TriggerClass {

	// Use this for initialization
	void Start () 
	{
		base.Start ();
	}
	
	// Update is called once per frame
	void Update () 
	{
	}

	void OnCollisionStay2D(Collision2D coll)
	{
		foreach (ContactPoint2D contact in coll.contacts) 
		{
			if (contact.normal [1] < -0.7) 
			{
				TriggerActivate (true);
			}
		}
	}
		
	void OnCollisionExit2D(Collision2D coll)
	{
		TriggerActivate (false);
	}
}
