using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour {

    GameObject[] pauseObjects;
    GameObject PauseMenu;
    bool first = true;

    // Use this for initialization
    void Start()
    {
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        PauseMenu = GameObject.Find("PauseMenu");
        //hidePaused();
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = 0;
        if(Input.GetKeyDown("enter")){
            Time.timeScale = 1;
            PauseMenu.SetActive(false);
        }

        if (Input.GetKeyDown("escape")) {

            LoadLevel("MainMenu");
        }
    }



    //controls the pausing of the scene
    public void pauseControl()
    {
        
           Time.timeScale = 1;
           hidePaused();
        
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
