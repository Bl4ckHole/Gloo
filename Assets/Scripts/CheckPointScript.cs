using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class CheckPointScript : MonoBehaviour {

    private Animator animator;
    public bool IsStartPoint;
    public GameObject GlooPrefab;
    private Dictionary<string, object> savedData;
    private bool ck = false;
    //private GameObject[] divisionHearts;
    //private GameObject[] divAndHeartsInAndOutsideGloo;
    //int maxDivision = 5;

    // Use this for initialization
    void Start ()
    {
        animator = GetComponent<Animator>();
        if (IsStartPoint)
        {
            CreateGloo();

            /*if (SceneManager.GetActiveScene().name == "Level2")
            {
                divAndHeartsInAndOutsideGloo = new GameObject[maxDivision];
                divisionHearts = new GameObject[maxDivision];
                Debug.Log("Coucou");
                GameObject Gloo = GameObject.Find("Gloo");
                divAndHeartsInAndOutsideGloo[0] = (GameObject)Instantiate(divisionHearts[0], new Vector3(0.0f, 0.0f, 0.0f), new Quaternion());
                Gloo.GetComponent<glooScript>().setDivHearts(divisionHearts);
                Gloo.GetComponent<glooScript>().setDivHeartsInAndOut(divAndHeartsInAndOutsideGloo);
            }*/

        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if ((coll.gameObject.tag == "Gloo") && (!ck))
        {
            bool[] divAvailable = coll.gameObject.GetComponent<glooScript>().getDivAvailable();
            bool[] divInGloo = coll.gameObject.GetComponent <glooScript> ().getDivInGloo();
            bool containAllDivs = true;
            for (int i=0; i< divAvailable.Length; i++)
            {
                containAllDivs = containAllDivs && (divAvailable[i] == divInGloo[i]);
            }

            if (containAllDivs)
            {
                // save world
                savedData = new Dictionary<string, object>();
                GameObject[] allObjects = FindObjectsOfType<GameObject>();
                foreach (GameObject obj in allObjects)
                {
                    if (obj.GetComponent<MonoBehaviour>() is GlooGenericObject)
                    {
                        GlooGenericObject objScript = obj.GetComponent<MonoBehaviour>() as GlooGenericObject;
                        savedData.Add(obj.name, objScript.getData());
                    }
                }
            

                // checkpoint checked
                ck= true;
                coll.gameObject.GetComponent<glooScript>().setSavePoint(this.gameObject);
                animator.SetTrigger("Checked");
            }
        }
    }

    void Reset()
    {
        bool[] divAvailable= new bool[1];

        // reset world
        foreach (KeyValuePair<string, object> kvp in savedData)
        {
            GameObject obj = GameObject.Find(kvp.Key);
            if (obj == null || obj.tag == "GlooDiv")
                continue;

            if (obj.tag == "Gloo")
            {
                divAvailable = obj.GetComponent<glooScript>().getDivAvailable();                
            }
            else
            {
                GlooGenericObject objScript = obj.GetComponent<MonoBehaviour>() as GlooGenericObject;
                objScript.setData(kvp.Value);
            }

        }
        CreateGloo(divAvailable);
    }


    void CreateGloo(bool[] divAvailable = null)
    {
        GameObject Gloo = (GameObject)Instantiate(GlooPrefab, transform.position, new Quaternion());
        Gloo.GetComponent<glooScript>().setSavePoint(this.gameObject);
        Gloo.name = "Gloo";

        if (divAvailable != null)
        {
            Gloo.GetComponent<glooScript>().setDivInGloo(divAvailable);
        }
    }
}
