using UnityEngine;
using System.Collections;
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

    public GameObject parent;
    public Sprite div;
    public object parentData;

    // Use this for initialization
    void Start() {
        oldPos = transform.position;
        rbody = GetComponent<Rigidbody2D>();
        record = new Queue();
        recordBuffer = new ArrayList();
    }

    // Update is called once per frame
    void Update() {
        if (recording) {
            if (Input.GetKeyDown(GlooConstants.keyDivide)) {
                recording = false;
                transform.position = oldPos;
                parent.GetComponent<glooScript>().setData(parentData);
                SpriteRenderer mySprite = gameObject.GetComponent<SpriteRenderer>();
                mySprite.sprite = div;
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
        if (coll.relativeVelocity.y < 0.16) {
            inJump = false;
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
