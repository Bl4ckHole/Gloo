using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnGUI()
    {
        if(GUI.Button(new Rect(450, 250, 200, 60), "PLAY"))
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Level1");
        }

        GUI.skin.GetStyle("label").fontSize = 40;
        GUI.Label(new Rect(495, 50, 300, 100), "GLOO");
    }
}
