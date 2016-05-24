using UnityEngine;
using System.Collections;

public class OstileClass : MonoBehaviour 
{

	// Use this for initialization
	virtual public void Start () 
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
			coll.gameObject.SendMessage ("die");
		}

	}
}
