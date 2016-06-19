using UnityEngine;
using System.Collections;
using Utils;
using UnityEngine.SceneManagement;

public class SphereScript : MonoBehaviour {

    GameObject Gloo;
    Animator animator;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>(); 
    }
	
	// Update is called once per frame
	void Update () {
        Gloo = GameObject.Find("Gloo");
        Vector2 pos = new Vector2(Gloo.transform.position.x,Gloo.transform.position.y); 
        transform.position = pos;
        if (Input.GetKeyDown(GlooConstants.keyAbsorb) || Input.GetKeyDown("joystick button 2"))
        {
            animator.SetBool("GoSphere", true);
        }
        if (Input.GetKeyUp(GlooConstants.keyAbsorb) || Input.GetKeyUp("joystick button 2"))
        {
            animator.SetBool("GoSphere", false);
        }



    }
}
