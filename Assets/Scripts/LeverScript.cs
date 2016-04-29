using UnityEngine;
using System.Collections;

public class LeverScript : MonoBehaviour 
{
	public GameObject cible;
	private bool isActivated = false;
	private Animator animator;

	// Use this for initialization
	void Start () 
	{
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void TriggerActivate()
	{
		isActivated = !isActivated;
		animator.SetBool("isActivated", isActivated);
		//cible.SendMessage ("Activate", isActivated);
	}
}
