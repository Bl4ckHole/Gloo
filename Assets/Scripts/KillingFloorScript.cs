using UnityEngine;
using System.Collections;

public class KillingFloorScript : MonoBehaviour {

	private EdgeCollider2D floor;
	// Use this for initialization
	void Start () {
		floor = GetComponent<EdgeCollider2D> ();
	}
	
	// Update is called once per frame
	void OnCollisionStay2D(Collision2D coll) {
		if (coll.gameObject.tag == "Gloo" || coll.gameObject.tag == "GlooDiv") {
			coll.gameObject.SendMessage ("die");
		}
	}
}
