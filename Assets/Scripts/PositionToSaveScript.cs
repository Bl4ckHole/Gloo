using UnityEngine;
using System.Collections;

public class PositionToSaveScript : MonoBehaviour, GlooGenericObject {

    private Rigidbody2D body;


    private class RushData
    {
        public Vector2 position;

        public RushData(Vector3 pos)
        {
            position = pos;
        }
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public object getData()
    {
        return new RushData(this.transform.position);
    }

    public void setData(object savedData)
    {
        RushData data = savedData as RushData;
        this.transform.position = data.position;
    }

}
