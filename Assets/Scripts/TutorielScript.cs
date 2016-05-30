using UnityEngine;
using System.Collections;

public class TutorielScript : MonoBehaviour {

	GameObject target;
	public float distance;
	private SpriteRenderer image;

	// Use this for initialization
	void Start () {
		target = GameObject.Find ("Gloo");
		image = GetComponent<SpriteRenderer> ();
		image.enabled = false;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (target != null)
        {
            if (Mathf.Abs(this.transform.position.x - target.transform.position.x) <= distance)
            {
                image.enabled = true;
            }
            else {
                image.enabled = false;
            }
        }
        else
        {
            // Gloo has been destroyed (die or restart) need to find the new Gloo
            target = GameObject.Find("Gloo");
        }
    }
}
