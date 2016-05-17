using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Utils;

public class divScript : MonoBehaviour {

    public Queue record;
    private ArrayList recordBuffer;
    private Rigidbody2D rbody;
    private float speed = 3.0f;
    private float jumpForce = 7.0f;
    bool inJump = false;
    bool recording = true;
    private Vector3 oldPos;
    private Dictionary<string,object> savedData;

    public Sprite div;

    // Use this for initialization
    void Start() {
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
    }

    // Update is called once per frame
    void Update() {
        if (recording) {
            if (Input.GetKeyDown(GlooConstants.keyDivide)) {
                recording = false;
                transform.position = oldPos;
                SpriteRenderer mySprite = gameObject.GetComponent<SpriteRenderer>();
                mySprite.sprite = div;
                foreach(KeyValuePair<string,object> kvp in savedData) {
                    GlooGenericObject objScript = GameObject.Find(kvp.Key).GetComponent<MonoBehaviour>() as GlooGenericObject;
                    objScript.setData(kvp.Value);
                }
            }
        }        
    }

    void FixedUpdate() {
        bool right = false;
        bool left = false;
        bool jump = false;
        if (recording) {
            right = Input.GetKey(GlooConstants.keyRight);
            left = Input.GetKey(GlooConstants.keyLeft);
            jump = Input.GetKey(GlooConstants.keyJump);
            RecordKeys();
        }
        else {
            if(record.Count == 0) {
                Destroy(gameObject);
                return;
            }
            ArrayList step = (ArrayList)record.Dequeue();
            foreach (KeyCode k in step) {
                right |= k == GlooConstants.keyRight;
                left |= k == GlooConstants.keyLeft;
                jump |= k == GlooConstants.keyJump;
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
            rbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            inJump = true;
        }
        move *= speed;
        float vy = rbody.velocity.y;
        rbody.velocity = move + new Vector2((inJump && move.x == 0) ? rbody.velocity.x : 0, vy);
    }

    void OnCollisionStay2D(Collision2D coll) {

        if (!inJump)
            return;

        foreach (ContactPoint2D contact in coll.contacts)
        {
            if (contact.normal[1] > 0.7)
            {
                inJump = false;
                break;
            }
        }
    }

    void RecordKeys() {
        ArrayList keys = new ArrayList { GlooConstants.keyJump, GlooConstants.keyLeft, GlooConstants.keyRight};
        recordBuffer.Clear();
        foreach (KeyCode code in keys) {
            if (Input.GetKey(code)) {
                recordBuffer.Add(code);
            }
        }
        record.Enqueue((ArrayList)recordBuffer.Clone());
    }
}
