using UnityEngine;
using System.Collections;
using Utils;

public class TutorielScript : MonoBehaviour {

	GameObject target;
	GameObject target2;
	public float distance;
	private SpriteRenderer image;
    public int id;
    private GameObject[] keys;
    public float scaling;

	// Use this for initialization
	void Start () {
		target = GameObject.Find ("Gloo");
		target2 = GameObject.Find ("division_0");
		image = GetComponent<SpriteRenderer> ();

        if (id == 1)
        {
            keys = new GameObject[3];
            keys[0] = GameObject.Find(GlooConstants.keyLeft.ToString());
            keys[0].transform.position = transform.position + new Vector3(-3.8f, -0.13f);
            keys[1] = GameObject.Find(GlooConstants.keyRight.ToString());
            keys[1].transform.position = transform.position + new Vector3(-2.43f, -0.13f);
            keys[2] = GameObject.Find(GlooConstants.keyJump.ToString());
            keys[2].transform.position = transform.position + new Vector3(-0.65f, -0.6f);
        }

        else if (id == 2)
        {
            keys = new GameObject[1];
            keys[0] = GameObject.Find(GlooConstants.keyActivate.ToString());
            keys[0].transform.position = transform.position + new Vector3(0.82f, 0.25f);
        }

        else if (id == 3)
        {
            keys = new GameObject[1];
            keys[0] = GameObject.Find(GlooConstants.keyDivide.ToString());
            keys[0].transform.position = transform.position + new Vector3(1.45f, 0.4f);
        }

        else
            keys = new GameObject[0];

        image.enabled = false;
        for (int i = 0; i < keys.Length; i++)
        {
            keys[i].GetComponent<SpriteRenderer>().enabled = false;
            keys[i].transform.localScale *= scaling;
        }
    }

	// Update is called once per frame
	void Update ()
    {
        if (target != null || target2 != null)
        {
			if (Mathf.Abs(this.transform.position.x - target.transform.position.x) <= distance || Mathf.Abs(this.transform.position.x - target2.transform.position.x) <= distance)
            {
                image.enabled = true;
                for(int i = 0; i < keys.Length; i++)
                    keys[i].GetComponent<SpriteRenderer>().enabled = true;
            }
            else {
                image.enabled = false;
                for (int i = 0; i < keys.Length; i++)
                    keys[i].GetComponent<SpriteRenderer>().enabled = false;
            }
        }
        else
        {
            // Gloo has been destroyed (die or restart) need to find the new Gloo
            target = GameObject.Find("Gloo");
			target2 = GameObject.Find ("division_0");
        }
    }
}
