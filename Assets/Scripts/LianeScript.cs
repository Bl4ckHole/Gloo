using UnityEngine;
using System.Collections;

public class LianeScript : MonoBehaviour
{

    public Animator animator;
    private PolygonCollider2D limite;
    private SpriteRenderer image;

    // Use this for initialization
    void Start()
    {
        limite = GetComponent<PolygonCollider2D>();
        limite.enabled = true;
        image = GetComponent<SpriteRenderer>();
        image.enabled = true;
        animator = GetComponent<Animator>();
    }

    public void Activate(bool isActivated)
    {
        limite.enabled = !isActivated;
        animator.SetBool("Activate",isActivated);
        //image.enabled = !isActivated;
    }
}
