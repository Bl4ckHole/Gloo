using UnityEngine;
using System.Collections;

public class CheckPointScript : MonoBehaviour {

    private Animator animator;
    public bool IsStartPoint;
    public GameObject GlooPrefab;

	// Use this for initialization
	void Start ()
    {
        animator = GetComponent<Animator>();
        if (IsStartPoint)
        {
            Reset();
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Gloo")
        {
            coll.gameObject.GetComponent<glooScript>().setSavePoint(this.gameObject);
            animator.SetTrigger("Checked");
        }
    }

    void Reset()
    {
        GameObject Gloo = (GameObject) Instantiate(GlooPrefab, transform.position, new Quaternion());
        Gloo.GetComponent<glooScript>().setSavePoint(this.gameObject);
        Gloo.name = "Gloo";
    }
}
