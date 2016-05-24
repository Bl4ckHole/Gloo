using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour {

    GameObject[] pauseObjects;
    GameObject pauseMenu;

    // Use this for initialization
    void Start()
    {
        //Time.timeScale = 1;
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        pauseMenu = GameObject.Find("PauseMenu");
        //hidePaused();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            LoadLevel("MainMenu");
        }

        if (Input.GetKey("enter"))
        {
           endPause();
        }

    }



    //controls the pausing of the scene
    public void endPause()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        
    }

   

    //loads inputted level
    public void LoadLevel(string level)
    {
        SceneManager.LoadScene(level);
    }
}
