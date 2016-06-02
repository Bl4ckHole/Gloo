using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    GameObject Menu;
    // Use this for initialization
    void Start () {
        Menu = GameObject.Find("Menu");
    }
	
	// Update is called once per frame
	void Update () {
	
	}



    void OnGUI() {

        if (GUI.Button(new Rect(300, 100, 200, 60), "Retour au jeu (P)")) {
            Time.timeScale = 1;
            Menu.SetActive(false);

        }
        if (GUI.Button(new Rect(300, 200, 200, 60), "Retour au Menu (Echap)"))
        {
            LoadLevel("MainMenu");
        }



    }


    //loads inputted level
    public void LoadLevel(string level)
    {
        SceneManager.LoadScene(level);
    }

}
