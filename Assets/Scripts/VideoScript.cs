using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class VideoScript : MonoBehaviour {

    private MovieTexture movie;
    public string nextScene = "MainMenu";
    private float timer = 0;
    public bool stop = false; 

    // Use this for initialization
	void Start () {
        movie = (MovieTexture)GetComponent<Renderer>().material.mainTexture;
        timer = movie.duration;
        movie.Play();
    }
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        Debug.Log(timer);
        if (Input.anyKey) {
            stop = true;
        }
        if(timer <= 0 || stop) {
            //movie.Stop();
            SceneManager.LoadScene("MainMenu");
        }
	}
}
