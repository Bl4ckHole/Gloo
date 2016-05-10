using UnityEngine;
using System.Collections;

public abstract class OstileClass : MonoBehaviour 
{

	// Use this for initialization
	public void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnCollisionStay2D(Collision2D coll) 
	{
		if (coll.gameObject.tag=="Gloo" || coll.gameObject.tag=="GlooDiv")
		{
			coll.gameObject.SendMessage ("Die");
		}

	}
}
