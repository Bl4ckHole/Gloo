using UnityEngine;
using System.Collections;

public class SuiviDivision : MonoBehaviour {
    public GameObject player;
    // Use this for initialization
    void Start () {
	
	}
	

	void FixedUpdate () {

        player = GameObject.Find("Gloo");
        float PlayerPosx = player.transform.position.x;
        float PlayerPosy = player.transform.position.y;
        this.transform.position = new Vector3(PlayerPosx, PlayerPosy, 0.0f);

    }
}
