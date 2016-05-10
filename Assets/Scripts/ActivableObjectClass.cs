using UnityEngine;
using System.Collections;

public class ActivableObjectClass : MonoBehaviour {

	public Animation animationName;
	private EdgeCollider2D limite;
	private SpriteRenderer image;

	// Use this for initialization
	void Start () {
		limite = GetComponent<EdgeCollider2D> ();
		limite.enabled = false;
		image = GetComponent<SpriteRenderer> ();
		image.enabled = false;
	}
	
	// Update is called once per frame
	public void Activate (bool isActivated) {
		limite.enabled = isActivated;
		//animationName.Play ();
		image.enabled = isActivated;
	}
}
