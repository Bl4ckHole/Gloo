using UnityEngine;
using System.Collections;

public abstract class TriggerClass : MonoBehaviour 
{
	public GameObject cible;
	private bool isActivated = false;
	private Animator animator;

	// Use this for initialization
	public void Start () 
	{
		animator = GetComponent<Animator>();
	}


	void TriggerActivate()
	{
		isActivated = !isActivated;
		animator.SetBool("isActivated", isActivated);
		if (cible == null) 
		{
			print ("This Trigger is't linked with any object");
		} 
		else 
		{
			cible.SendMessage ("Activate", isActivated);
		}
	}
}
