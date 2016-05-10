using UnityEngine;
using System.Collections;

public class CameraBehavior : MonoBehaviour {

    public GameObject player;
    float speed =0.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        player = GameObject.Find("Gloo");
        float PlayerPosx = player.transform.position.x;
        float CamPosx = this.transform.position.x;
        float posx=0.0f;
        float posy=0.0f;
        if (Mathf.Abs(PlayerPosx-CamPosx)>2.0f) {
            posx = Mathf.SmoothDamp(CamPosx, PlayerPosx, ref speed, 2f);
        } else {
            posx = CamPosx;
        }
        float PlayerPosy = player.transform.position.y;
        float CamPosy = this.transform.position.y;
        if (Mathf.Abs(PlayerPosy - CamPosy) > 2.0f)
        {
            posy = Mathf.SmoothDamp(CamPosy, PlayerPosy, ref speed, 2f);
        }else {
            posy = CamPosy;
        }
        transform.Translate(new Vector3(posx - CamPosx, posy - CamPosy, 0.0f));


    }
}
