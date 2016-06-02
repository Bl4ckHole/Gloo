using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CheckPointScript : MonoBehaviour {

    private Animator animator;
    public bool IsStartPoint;
    public GameObject GlooPrefab;
    private Dictionary<string, object> savedData;
    private bool ck = false;

    // Use this for initialization
    void Start ()
    {
        animator = GetComponent<Animator>();
        if (IsStartPoint)
        {
            CreateGloo();
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if ((coll.gameObject.tag == "Gloo") && (!ck))
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

    void Reset(GameObject pauseMenu)
    {
        // reset world
        foreach (KeyValuePair<string, object> kvp in savedData)
        {
            GameObject obj = GameObject.Find(kvp.Key);
            if (obj == null || obj.tag=="Gloo" || obj.tag == "GlooDiv")
                    continue;
            GlooGenericObject objScript = obj.GetComponent<MonoBehaviour>() as GlooGenericObject;
            objScript.setData(kvp.Value);
        }
        CreateGloo(pauseMenu);
    }

    void CreateGloo()
    {
        GameObject pauseMenu = GameObject.Find("Menu");
        //pauseMenu.SetActive(false);
        CreateGloo(pauseMenu);
    }
    void CreateGloo(GameObject pauseMenu)
    {
        GameObject Gloo = (GameObject) Instantiate(GlooPrefab, transform.position, new Quaternion());
        Gloo.GetComponent<glooScript>().setSavePoint(this.gameObject);
        Gloo.name = "Gloo";
        Gloo.GetComponent<glooScript>().setPauseMenu(pauseMenu);
    }
}
