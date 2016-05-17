using UnityEngine;
using System.Collections;

public class PorteScript : MonoBehaviour {

	public Animation animationName;
	private PolygonCollider2D limite;
	private SpriteRenderer image;

	// Use this for initialization
	void Start () {
		limite = GetComponent<PolygonCollider2D> ();
		limite.enabled = true;
		image = GetComponent<SpriteRenderer> ();
		image.enabled = true;
	}
		
	public void Activate (bool isActivated) {
		limite.enabled = !isActivated;
		//animationName.Play ();
		image.enabled = !isActivated;
		Debug.Log(!isActivated);
	}
}
