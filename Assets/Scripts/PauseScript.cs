using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour {

    // Use this for initialization
 

    void Start()
    {
        Time.timeScale = 1;
    }




    // Update is called once per frame
    void Update()
    {

        

        if (Input.GetKeyDown("p")){
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                
            } else {
                Time.timeScale = 1;
            }
        }

        if (Input.GetKeyDown("escape")) {
            if (Time.timeScale == 0) {
                LoadLevel("MainMenu");
            }

            
        }
    }



    //loads inputted level
    public void LoadLevel(string level)
    {
        SceneManager.LoadScene(level);
    }
}
