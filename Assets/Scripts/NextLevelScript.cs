using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class NextLevelScript : MonoBehaviour {

	public string nextLevel;
	private GameObject gloo;
	private bool[] divOk;
	private int compteur;
	public int divNeeded;

	// Use this for initialization
	void Start () {
		gloo = GameObject.Find ("Gloo");
	
	}
	
	// Update is called once per frame
	void OnCollisionStay2D(Collision2D coll) {
        gloo = GameObject.Find("Gloo");
        if (coll.gameObject.tag == "Gloo") {
			print ("OK");
			divOk = gloo.GetComponent<glooScript> ().getDivInGloo ();
			compteur = 0;
			foreach (bool i in divOk) {
				if (i) {
					compteur += 1;
				}
			}
			if (compteur >= divNeeded) {
				SceneManager.LoadScene(nextLevel);
			}
		}
	}
}
