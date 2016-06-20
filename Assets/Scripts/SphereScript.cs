using UnityEngine;
using System.Collections;
using Utils;
using UnityEngine.SceneManagement;

public class SphereScript : MonoBehaviour {

    private GameObject Gloo;
    private Animator animator;
    private AudioSource bruitage;
    private bool sphereAbsorptionVisibility=false;

    // Use this for initialization
    void Start ()
    {
        this.name = "sphereAbsorption";
        animator = GetComponent<Animator>();
        bruitage = GetComponent<AudioSource>();
        bruitage.mute = true;
    }
	
	// Update is called once per frame
	void Update ()
    {

        Gloo = GameObject.Find("Gloo");
        Vector2 pos = new Vector2(Gloo.transform.position.x,Gloo.transform.position.y); 
        transform.position = pos;
    

        if ( (Input.GetKeyDown(GlooConstants.keyAbsorb))|| Input.GetKeyDown("joystick button 2"))
        {
            bruitage.mute = false;
            animator.SetBool("GoSphere", true);
        }
        if (Input.GetKeyUp(GlooConstants.keyAbsorb) || Input.GetKeyUp("joystick button 2"))
        {
            bruitage.mute = true;
            animator.SetBool("GoSphere", false);
        }
    


        /*
        if (sphereAbsorptionVisibility)
        {
            displaySphereAbsorption();
        }
        else
        {
            closeSphereAbsorption();
        }*/
    }

    public void displaySphereAbsorption()
    {
        bruitage.mute = false;
        animator.SetBool("GoSphere", true);
    }

    public void closeSphereAbsorption()
    {
        bruitage.mute = true;
        animator.SetBool("GoSphere", false);
    }

    public void setSphereAbsorptionVisibility(bool visibility)
    {
        sphereAbsorptionVisibility = visibility;
    }
}
