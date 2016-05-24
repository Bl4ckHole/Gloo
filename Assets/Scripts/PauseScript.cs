using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour {

    GameObject[] pauseObjects;
    GameObject ButPlay;

    // Use this for initialization
    void Start()
    {
        Time.timeScale = 1;
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        ButPlay = GameObject.Find("Play");
        //hidePaused();
    }

    // Update is called once per frame
    void Update()
    {

    }



    //controls the pausing of the scene
    public void pauseControl()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            hidePaused();
        }
    }

    //shows objects with ShowOnPause tag
    public void showPaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(true);
        }
    }

    //hides objects with ShowOnPause tag
    public void hidePaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(false);
        }
    }

    //loads inputted level
    public void LoadLevel(string level)
    {
        SceneManager.LoadScene(level);
    }
}
