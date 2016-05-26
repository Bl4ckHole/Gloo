using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Utils;
using System;

public class divScript : MonoBehaviour, GlooGenericObject {

    public Queue record;
    private ArrayList recordBuffer;
    private Rigidbody2D rbody;
    private float speed = 3.0f;
    private float jumpForce = 9.0f;
    private int colorID = 0;
    bool inJump = false;
    bool recording = true;
    private Vector3 oldPos;
    private Dictionary<string,object> savedData;
    private BoxCollider2D boxcoll;
    public int wallJump = 0;
    bool finish = false;
	private GameObject filter;
	private	SpriteRenderer filter_renderer;
	public string filter_name;

    private class divData {
        public bool inJump;
        public bool recording;
        public Queue record;
        public Vector3 pos;
        public Vector3 velocity;
        
        public divData(bool inJump, bool recording, Queue record, Vector3 pos, Vector3 velocity) {
            this.inJump = inJump;
            this.record = record;
            this.pos = pos;
            this.velocity = velocity;
            this.recording = recording;
        }
    }

    public Sprite div;

    // Use this for initialization
    void Start() {
		filter = GameObject.Find (filter_name);
		filter_renderer = filter.GetComponent<SpriteRenderer> ();
		filter_renderer.enabled = true;
        boxcoll = GetComponent<BoxCollider2D>();

        oldPos = transform.position;
        rbody = GetComponent<Rigidbody2D>();
        record = new Queue();
        recordBuffer = new ArrayList();
        savedData = new Dictionary<string, object>();
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        foreach(GameObject obj in allObjects) {
            if(obj.GetComponent<MonoBehaviour>() is GlooGenericObject) {
                GlooGenericObject objScript = obj.GetComponent<MonoBehaviour>() as GlooGenericObject;
                savedData.Add(obj.name, objScript.getData());
            }
        }
        GameObject cam = GameObject.Find("Main Camera");
        cam.GetComponent<CameraBehavior>().currenttarget = gameObject.name;       
    }

    // Update is called once per frame
    void Update() {
        if (recording) {
            if (Input.GetKeyDown(GlooConstants.keyDivide)) {
				filter_renderer.enabled = false;
                recording = false;
                transform.position = oldPos;
                SpriteRenderer mySprite = gameObject.GetComponent<SpriteRenderer>();
                mySprite.sprite = div;
                foreach(KeyValuePair<string,object> kvp in savedData) {
                    GameObject obj = GameObject.Find(kvp.Key);
                    if (obj == null)
                        continue;
                    GlooGenericObject objScript = obj.GetComponent<MonoBehaviour>() as GlooGenericObject;
                    objScript.setData(kvp.Value);
                }
                GameObject cam = GameObject.Find("Main Camera");
                cam.GetComponent<CameraBehavior>().currenttarget = "Gloo";
            }
        }
    }

    void FixedUpdate() {
        bool right = false;
        bool left = false;
        bool jump = false;
        bool active = false;
        if (recording) {
            right = Input.GetKey(GlooConstants.keyRight);
            left = Input.GetKey(GlooConstants.keyLeft);
            jump = Input.GetKey(GlooConstants.keyJump);
            active = Input.GetKeyDown(GlooConstants.keyActivate);
            RecordKeys();
        }
        else {
            if(record.Count == 0) {
                // Destroy(gameObject);
                finish = true;
                return;
            }
            if (finish) {
                return;
            }
            ArrayList step = (ArrayList)record.Dequeue();
            foreach (KeyCode k in step) {
                right |= k == GlooConstants.keyRight;
                left |= k == GlooConstants.keyLeft;
                jump |= k == GlooConstants.keyJump;
                active |= k == GlooConstants.keyActivate;
            }
        }        

        Vector2 move = new Vector2(0, 0);
        if (left) {
            move += new Vector2(-1, 0);
        }
        if (right) {
            move += new Vector2(1, 0);
        }
        if (jump && !inJump) {            
            rbody.AddForce(new Vector2((jumpForce/1.0f)*wallJump, jumpForce), ForceMode2D.Impulse);
            if (wallJump != 0) {
                wallJump = 0;
            }
            inJump = true;
        }
        move *= speed;
        float vy = rbody.velocity.y;
        rbody.velocity = move + new Vector2((inJump && move.x == 0) ? rbody.velocity.x : 0, vy);

        if (active) {
            Collider2D[] nearbyObjects = Physics2D.OverlapCircleAll(this.transform.position, boxcoll.size.x / 2 * this.transform.localScale.x);
            foreach (Collider2D objColl in nearbyObjects) {
                if (objColl.gameObject.tag == "ManualTrigger") {
                    objColl.gameObject.SendMessage("TriggerActivate");
                }
            }
        }
    }

    void OnCollisionStay2D(Collision2D coll) {

        if (!inJump)
            return;

        foreach (ContactPoint2D contact in coll.contacts)
        {
            if (contact.normal[1] > 0.7)
            {
                inJump = false;
                wallJump = 0;
                break;
            }
            if(Math.Abs(contact.normal[1]) < 0.3) {
                inJump = false;
                wallJump = contact.normal[1] >= 0 ? -1 : 1;
                break;
            }
        }
    }

    void RecordKeys() {
        ArrayList keys = new ArrayList { GlooConstants.keyJump, GlooConstants.keyLeft, GlooConstants.keyRight};
        ArrayList keysDown = new ArrayList { GlooConstants.keyActivate };
        recordBuffer.Clear();
        foreach (KeyCode code in keys) {
            if (Input.GetKey(code)) {
                recordBuffer.Add(code);
            }
        }
        foreach( KeyCode code in keysDown) {
            if (Input.GetKeyDown(code)) {
                recordBuffer.Add(code);
            }
        }
        record.Enqueue((ArrayList)recordBuffer.Clone());
    }

    public object getData() {
        return new divData(inJump,recording,new Queue(record), transform.position, rbody.velocity);
    }

    public void setData(object savedData) {
        divData data = savedData as divData;
        if (data.recording)
            return;
        inJump = data.inJump;
        record = data.record;
        transform.position = data.pos;
        rbody.velocity = data.velocity;
    }


    public void setColorID(int id)
    {
        colorID = id;
    }

    public int getColorID()
    {
        return colorID;
    }

    public void die()
    {
        boxcoll.enabled = false;
        rbody.isKinematic = true;

        GameObject Gloo = GameObject.Find("Gloo");
        Gloo.SendMessage("die");
    }
}
