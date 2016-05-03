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


	public void TriggerActivate()
	{
		isActivated = !isActivated;
		TriggerActivate (isActivated);
	}
	public void TriggerActivate(bool requested)
	{
		animator.SetBool("isActivated", requested);
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
