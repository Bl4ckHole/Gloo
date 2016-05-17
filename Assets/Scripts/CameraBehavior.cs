using UnityEngine;
using System.Collections;


public class CameraBehavior : MonoBehaviour
{

    private float interpVelocity;
    private float distanceMin;
    public GameObject target;
    public string currenttarget;
    Vector3 targetPos;
    // Use this for initialization
    void Start()
    {
        targetPos = transform.position;
        currenttarget = "Gloo";
        distanceMin = 1.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        target = GameObject.Find(currenttarget);
        Vector3 posNoZ = transform.position;
        posNoZ.z = target.transform.position.z;

        Vector3 targetDirection = (target.transform.position - posNoZ);

        interpVelocity = targetDirection.magnitude * 5f;

        targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime);

        Vector3 resultat = Vector3.Lerp(transform.position, targetPos, 0.25f);

        Vector3 Id= Vector3.Lerp(transform.position, transform.position, 0.25f);

        if (Mathf.Abs(targetDirection.x) <0.5f) {
            resultat.x = Id.x;
        }
        if (Mathf.Abs(targetDirection.y)<2.0f) {
            resultat.y = Id.y;
        }

        transform.position = resultat;

    }


    void setCurrentTarget(string nom) {
        currenttarget = nom;
    }


    string getCurrentTarget() {
        return currenttarget;
    }


}