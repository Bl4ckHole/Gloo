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
        float pos;
        if (Mathf.Abs(PlayerPosx-CamPosx)>0.5f) {
            pos = Mathf.SmoothDamp(CamPosx, PlayerPosx, ref speed, 2f);
            transform.Translate(new Vector3(pos-CamPosx, 0.0f,0.0f));
        }
        float PlayerPosy = player.transform.position.y;
        float CamPosy = this.transform.position.y;
        if (Mathf.Abs(PlayerPosy - CamPosy) > 2.0f)
        {
            pos = Mathf.SmoothDamp(CamPosy, PlayerPosy, ref speed, 2f);
            transform.Translate(new Vector3(0.0f, pos - CamPosy, 0.0f));
        }



    }
}
