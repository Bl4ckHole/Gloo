using UnityEngine;
using System.Collections;

public class DivisionMenuScript : MonoBehaviour {

    public int choix = 0;
    GameObject[] divisionsDispo;
    GameObject Gloo;
    private Animator animator;
    int hashright;
    int hashleft;
    int currenthash;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update () {
        Gloo = GameObject.Find("Gloo");
        hashleft = Gloo.GetComponent<glooScript>().getHashleft();
        hashright = Gloo.GetComponent<glooScript>().getHashright();
        currenthash = Gloo.GetComponent<glooScript>().getcurrentHash();
        if (Input.GetKeyDown("e") && (hashleft==currenthash || hashright==currenthash) && (!Gloo.GetComponent<glooScript>().getDataRecording()))
        {
            if (Time.timeScale == 1)
            {
                
                choix = 0;
                divisionsDispo = Gloo.GetComponent<glooScript>().get_divisions_dispo();
                Time.timeScale = 0;
                dessine_choix();
            }
            else {
                Time.timeScale = 1;
                detruit_choix();
            }
        }


        if (Time.timeScale == 0) {

            if (Input.GetKeyDown("d")) {
                choix += 1;
                choix %= divisionsDispo.Length; 
            }
            if (Input.GetKeyDown("q")) {
                choix -= 1;
                choix %= divisionsDispo.Length;
            }
            if (Input.GetKey(KeyCode.Return))
            {
                Gloo.GetComponent<glooScript>().setDivisionSelectionnee(choix);
                Gloo.GetComponent<glooScript>().setDataRecording(true);
                Gloo.GetComponent<Animator>().SetBool("DoCreate", true);
                Time.timeScale = 1;
            }


        }



    }


    void dessine_choix() {

    }

    void detruit_choix() {


    }


}
