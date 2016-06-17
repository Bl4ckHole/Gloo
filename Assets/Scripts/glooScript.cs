using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Utils;
using System;
using UnityEngine.SceneManagement;

public class glooScript : MonoBehaviour, GlooGenericObject {

    //*****************************************************************************************************************
    //******************************************************CLASS******************************************************
    //*****************************************************************************************************************
    private class glooData {

        public bool inJump = false;
        public bool recording = false;
        public Vector3 position;
        public string createFace;

        public glooData()
        {
        }

        public glooData(glooData model) {
            inJump = model.inJump;
            recording = model.recording;
            position = model.position;
            createFace = model.createFace;
        }        
    }

    //*****************************************************************************************************************
    //****************************************************Variables****************************************************
    //*****************************************************************************************************************

    private Animator animator;
    private Rigidbody2D rbody;
    public BoxCollider2D boxcoll;
    private float speed = 3.0f;
    private float jumpForce = 30.0f;
    private int hashLeft = Animator.StringToHash("run_left");
    private int hashRight = Animator.StringToHash("run_right");
    private int hashJumpToR = Animator.StringToHash("JumpToStandR");
    private int hashJumpToL = Animator.StringToHash("JumpToStandL");
    private int hashJumpL = Animator.StringToHash("jump_left");
    private int hashJumpR = Animator.StringToHash("jump_right");
    private int hashIdleLeft = Animator.StringToHash("stand_left");
    private int hashIdleRight = Animator.StringToHash("stand_right");
    private int hashDivLeft = Animator.StringToHash("glooCreationDivisionLeft");
    private int hashDivRight = Animator.StringToHash("glooCreationDivisionRight");

    /*bool inJump = false;
    public bool recording = false;*/
    public int facing = 1;
    private GameObject savePoint;
    private GameObject filter;
    private SpriteRenderer filter_renderer;
    public string filter_name;
    private bool divAnimationAsStarted;
    private bool alreadyReseted = false;
    
    private glooData data = new glooData();
    private bool paused = false;
    private bool divisionRequested = false;

    private bool updateDivInGlooRequest = false;
    public GameObject[] divisions;
    public GameObject[] divisionHearts;
    private bool[] divisionsInGloo;
    private GameObject[] divAndHeartsInAndOutsideGloo;   // when divisionsInGloo[i] = true : divAndHeartsInAndOutsideGloo[i] stoquera le coeur de la div ; else le coeur sera détruit et on gardera en mémoire la division sortie de gloo
    public static int divID = 0;
    int division_selectionnee = 0;
    int maxDivision = 5;



    //*****************************************************************************************************************
    //*************************************************Unity Functions*************************************************
    //*****************************************************************************************************************

    // Use this for initialization
    void Start() {
		filter = GameObject.Find (filter_name);
		filter_renderer = filter.GetComponent<SpriteRenderer> ();
		filter_renderer.enabled = false;

        if (divisionsInGloo == null)
        {
            divisionsInGloo = new bool[maxDivision];
            
        }
            divAndHeartsInAndOutsideGloo = new GameObject[maxDivision];
        if(SceneManager.GetActiveScene().name=="Level2") {
            divisionsInGloo[0] = true;
            updateDivInGloo();
        }

        animator = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody2D>();
        boxcoll = GetComponent<BoxCollider2D>();
    }


    public void setDivHearts(GameObject[] dHearts) {
        divisionHearts = dHearts;
    }

    public void setDivHeartsInAndOut(GameObject[] dHearts)
    {
        divAndHeartsInAndOutsideGloo = dHearts;
    }


    // Update is called once per frame
    void Update () {

        if(updateDivInGlooRequest)
        {
            updateDivInGloo();
        }
        if (divisionRequested)
        {
            openDivMenu();
        }

        if (!data.recording) {
            int currentHash = animator.GetCurrentAnimatorStateInfo(0).shortNameHash;
            if (Input.GetKeyDown(GlooConstants.keyDivide) && (currentHash == hashIdleLeft || currentHash == hashIdleRight))
            {
                divisionRequested=true;
            }
            bool right = Input.GetKey(GlooConstants.keyRight);
            bool left = Input.GetKey(GlooConstants.keyLeft);
            if (left) {
                facing = 1;
            } else if (right) {
                facing = 2;
            }
            animator.SetBool("GoLeft", left);
            animator.SetBool("GoRight", right && !left);
            animator.SetBool("Jump", data.inJump);
			if (Input.GetKeyDown (GlooConstants.keyActivate)) 
			{
				Collider2D[] nearbyObjects = Physics2D.OverlapCircleAll(this.transform.position, boxcoll.size.x/2 * this.transform.localScale.x);
				foreach (Collider2D objColl in nearbyObjects) 
				{
					if (objColl.gameObject.tag == "ManualTrigger") 
					{
						objColl.gameObject.SendMessage ("TriggerActivate");
					}
				}
			}

            if (Input.GetKeyDown(GlooConstants.keyAbsorb))
            {
                Collider2D[] nearbyObjects = Physics2D.OverlapCircleAll(this.transform.position, boxcoll.size.x * this.transform.localScale.x * 2);
                foreach (Collider2D objColl in nearbyObjects)
                {
                    if (objColl.gameObject.tag == "GlooDiv")
                    {
                        // TODO : animations disparition
                        // destroy the division
                        Destroy(objColl.gameObject);
                        int i = objColl.gameObject.GetComponent<divScript>().getColorID();
                        // Recreate the heart of the division inside Gloo
                        divAndHeartsInAndOutsideGloo[i] = (GameObject)Instantiate(divisionHearts[i], new Vector3(0.0f, 0.0f, 0.0f), new Quaternion());
                        divisionsInGloo[i] = true;
                    }
                }
            }

        }        
    }

    void FixedUpdate() {
        float rotation = 0.0f;
        Debug.DrawLine(transform.position, transform.position + transform.right, Color.red);
        Debug.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y) + Vector2.right, Color.blue);
        if (transform.right.y > 0.0f)
            rotation = -1.0f;
        if (transform.right.y < 0.0f)
            rotation = 1.0f;

        if (data.inJump)
            transform.Rotate(new Vector3(0.0f, 0.0f, rotation));

        else
        {
            float width = 0.09f;
            float ray_size = 0.2f;
            Physics2D.queriesHitTriggers = false;
            RaycastHit2D hit1 = Physics2D.Raycast(transform.position - width * boxcoll.size.x * transform.right, -transform.up, ray_size * boxcoll.size.y);
            RaycastHit2D hit2 = Physics2D.Raycast(transform.position + width * boxcoll.size.x * transform.right, -transform.up, ray_size * boxcoll.size.y);
            Physics2D.queriesHitTriggers = true;
            Debug.DrawLine(transform.position - width * boxcoll.size.x * transform.right, transform.position - width * boxcoll.size.x * transform.right - ray_size * boxcoll.size.y * transform.up, Color.green);
            Debug.DrawLine(transform.position + width * boxcoll.size.x * transform.right, transform.position + width * boxcoll.size.x * transform.right - ray_size * boxcoll.size.y * transform.up, Color.green);
            if (hit1.collider != null && hit2.collider != null)
            {
                float balance = hit2.distance - hit1.distance;
                if (balance > 0.001f)
                    rotation = -1.0f;
                else if (balance < -0.001f)
                    rotation = 1.0f;
            }

            transform.Rotate(new Vector3(0.0f, 0.0f, rotation));
        }
        

        Vector2 move = new Vector2(0, 0);
        int currentHash = animator.GetCurrentAnimatorStateInfo(0).shortNameHash;
        if (currentHash == hashLeft || (currentHash== hashJumpToL && Input.GetKey(GlooConstants.keyLeft)) || (currentHash == hashJumpL && Input.GetKey(GlooConstants.keyLeft))) {
            move += new Vector2(-1, 0);
        }
      
        if (currentHash == hashRight || (currentHash == hashJumpToR && Input.GetKey(GlooConstants.keyRight)) || (currentHash == hashJumpR && Input.GetKey(GlooConstants.keyRight))) {
            move += new Vector2(1, 0);
        }

        if (currentHash == hashJumpL && Input.GetKey(GlooConstants.keyRight))
        {
            move += new Vector2(0.5f, 0);
        }
        if (currentHash == hashJumpR && Input.GetKey(GlooConstants.keyLeft))
        {
            move += new Vector2(-0.5f, 0);
        }

        if (Input.GetKey(GlooConstants.keyJump) && !data.inJump && !data.recording) {
            rbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            data.inJump = true;
        }
        move *= speed;
        float vy = rbody.velocity.y;
        rbody.velocity = move + new Vector2((data.inJump && move.x == 0) ? rbody.velocity.x : 0, vy);


        if (Input.GetKeyDown(GlooConstants.keyReset) && !alreadyReseted)
        {
            this.die();
        }

    }

    void OnCollisionStay2D(Collision2D coll) {

        if (!data.inJump)
            return;

        foreach (ContactPoint2D contact in coll.contacts)
        {
			if(contact.normal[1] > 0.7)
            {
                data.inJump = false;
                break;
            }
        }
    }

    //*****************************************************************************************************************
    //**************************************************Our Fonctions**************************************************
    //*****************************************************************************************************************

    public void openDivMenu()
    {
        int divAvailable = 0;
        for (int i=0; i< divisionsInGloo.Length; i++)
        {
            if (divisionsInGloo[i])
            {
                division_selectionnee = i;
                divAvailable++;
            }
        }


        if(divAvailable==0)
        {
            divisionRequested = false;
        }
        else
        {
            if (divAvailable == 1)
            {
                data.recording = true;
                animator.SetBool("DoCreate", true);
                Time.timeScale = 1;
            }
            else
            {
                if (!animator.GetBool("DoCreate"))
                {
                    Time.timeScale = 0;
                    division_selectionnee = 0;

                    if (Input.GetKeyDown(GlooConstants.keyRight))
                    {
                        division_selectionnee += 1;
                        division_selectionnee %= 1;
                    }
                    if (Input.GetKeyDown(GlooConstants.keyRight))
                    {
                        division_selectionnee -= 1;
                        division_selectionnee %= 1;
                    }
                    if (Input.GetKeyDown(GlooConstants.keyDivide))
                    {
                        data.recording = true;
                        animator.SetBool("DoCreate", true);
                        Time.timeScale = 1;
                    }
                }
            }
            if(animator.GetBool("DoCreate"))
            { 
                int currentHash = animator.GetCurrentAnimatorStateInfo(0).shortNameHash;
                if (currentHash == hashDivLeft || currentHash == hashDivRight)
                {
                    divAnimationAsStarted = true;
                }
                else
                {
                    if (divAnimationAsStarted)
                    {
                        divisionRequested = false;
                        animator.SetBool("DoCreate", false);
                        divAnimationAsStarted = false;

                        int facing_int = facing == 1 ? -1 : 1;
                        GameObject newDiv = (GameObject)Instantiate(divisions[division_selectionnee], transform.position + new Vector3((boxcoll.size.x / 1.2f * transform.localScale.x) * facing_int, -boxcoll.size.y / 3.0f * transform.localScale.y, 0), new Quaternion());

                        newDiv.name = "division_" + division_selectionnee;
                        newDiv.GetComponent<divScript>().setColorID(division_selectionnee);

                        divisionsInGloo[division_selectionnee] = false;
                        Destroy(divAndHeartsInAndOutsideGloo[division_selectionnee]);
                        divAndHeartsInAndOutsideGloo[division_selectionnee] = newDiv;
                    }
                }
            }
        }
    }


    public void die()
    {
        if (data.recording)
        {
            resetThisRecording();
        }
        else
        {
            divisionRequested = false;
            alreadyReseted = true;
            boxcoll.enabled = false;
            rbody.isKinematic = true;

            savePoint.SendMessage("Reset");

            for (int i = 0; i < divisionsInGloo.Length; i++)
            {
                Destroy(divAndHeartsInAndOutsideGloo[i]);
            }
            animator.SetTrigger("Dead");

            Destroy(this.gameObject, 1.3f);
        }
    }

    public void resetThisRecording()
    {
        // TODO reset au début du mode fantome, on reset juste l'enregistrement!!
    }

    //*****************************************************************************************************************
    //************************************************Getters & Setters************************************************
    //*****************************************************************************************************************

    public object getData() {
        glooData toSave = new glooData(data);
        toSave.recording = false;
        toSave.position = transform.position;
        return toSave;
    }

    public void setData(object savedData) {
        data = (glooData) savedData;
        transform.position = data.position;
    }

    public GameObject getSavePoint()
    {
        return savePoint;
    }
    public void setSavePoint(GameObject checkpoint)
    {
        this.savePoint = checkpoint;
    }

    public void setDivInGloo(bool[] tab)
    {
        updateDivInGlooRequest = true;
        divisionsInGloo = tab;

    }
    public bool[] getDivAvailable()
    {
        bool[] res = new bool[divisionsInGloo.Length];
        for (int i = 0; i < divisionsInGloo.Length; i++)
        {
            if (divAndHeartsInAndOutsideGloo[i]!=null)
            {
                res[i] = divisionsInGloo[i] || (divAndHeartsInAndOutsideGloo[i].tag == "GlooDiv");
            }
        }
        return res;
    }
    public void updateDivInGloo()
    {
        updateDivInGlooRequest = false;
        
        for (int i = 0; i < divisionsInGloo.Length; i++)
        {
            if (divisionsInGloo[i])
            {
                divAndHeartsInAndOutsideGloo[i] = (GameObject)Instantiate(divisionHearts[i], new Vector3(0.0f, 0.0f, 0.0f), new Quaternion());
            }
        }
    }

    public bool[] getDivInGloo()
    {
        return divisionsInGloo;
    }


    public void setDivisionSelectionnee(int n)
    {
        division_selectionnee = n;
    }

    public void setDataRecording(bool b)
    {
        data.recording = b;
    }

    public bool getDataRecording()
    {
        return data.recording;
    }


}

