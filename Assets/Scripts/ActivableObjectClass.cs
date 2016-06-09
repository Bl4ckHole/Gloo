using UnityEngine;
using System.Collections;

public class ActivableObjectClass : MonoBehaviour {

	private EdgeCollider2D limite;
	private SpriteRenderer image;
	private Animator animator;
	private int hashisActivated = Animator.StringToHash("isActivated");

	// Use this for initialization
	void Start () {
		limite = GetComponent<EdgeCollider2D> ();
		limite.enabled = false;
//		image = GetComponent<SpriteRenderer> ();
//		image.enabled = false;
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	public void Activate (bool isActivated) {
		limite.enabled = isActivated;
		animator.SetBool(hashisActivated,isActivated);
//		image.enabled = isActivated;
	}
}
