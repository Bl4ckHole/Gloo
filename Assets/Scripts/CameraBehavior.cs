using UnityEngine;
using System.Collections;


public class CameraBehavior : MonoBehaviour
{

    private float interpVelocity;
    private float distanceMin;
    public GameObject target;
    public string currenttarget;
    Vector3 targetPos;
    public float speed;
    // Use this for initialization
    void Start()
    {
        targetPos = transform.position;
        currenttarget = "Gloo";
        distanceMin = 1.0f;
        speed = 0.15f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 move = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.UpArrow) | Input.GetKey(KeyCode.DownArrow) | Input.GetKey(KeyCode.RightArrow) | Input.GetKey(KeyCode.LeftArrow)) {

            if (Input.GetKey(KeyCode.UpArrow))
            {
                move += new Vector3(0, 1, 0);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                move += new Vector3(0, -1, 0);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                move += new Vector3(1, 0, 0);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                move += new Vector3(-1, 0, 0);
            }
            move *= speed;

        }
        
        else {
            target = GameObject.Find(currenttarget);
            Vector3 posNoZ = transform.position;
            posNoZ.z = target.transform.position.z;

            Vector3 targetDirection = (target.transform.position - posNoZ);

            interpVelocity = targetDirection.magnitude * 5f;

            targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime);

            Vector3 resultat = Vector3.Lerp(transform.position, targetPos, 0.25f);

            Vector3 Id = Vector3.Lerp(transform.position, transform.position, 0.25f);

            if (Mathf.Abs(targetDirection.x) < 0.5f)
            {
                resultat.x = Id.x;
            }
            if (Mathf.Abs(targetDirection.y) < 2.0f)
            {
                resultat.y = Id.y;
            }

            transform.position = resultat;
        }
        transform.position += move;

        }


    void setCurrentTarget(string nom) {
        currenttarget = nom;
    }


    string getCurrentTarget() {
        return currenttarget;
    }


}