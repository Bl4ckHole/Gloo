using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour {

    // Use this for initialization

    public GameObject MenuPause;
    void Start()
    {

        MenuPause = GameObject.Find("Menu");
        Time.timeScale = 1;
        MenuPause.SetActive(false);
    }




    // Update is called once per frame
    void Update()
    {

        

        if (Input.GetKeyDown("p") || Input.GetKeyDown("joystick button 7")){
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                MenuPause.SetActive(true);
                
            } else {
                Time.timeScale = 1;
                MenuPause.SetActive(false);
            }
        }

        if (Input.GetKeyDown("escape") || Input.GetKeyDown("joystick button 6")) {
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
