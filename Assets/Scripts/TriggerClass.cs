using UnityEngine;
using System.Collections;
using System;

public abstract class TriggerClass : MonoBehaviour, GlooGenericObject
{
	public GameObject cible;
	private bool isActivated = false;
	private Animator animator;

    private class TriggerData {
        public bool isActivated;
        public TriggerData(bool activ) {
            isActivated = activ;
        }
    }

	// Use this for initialization
	virtual public void Start () 
	{
		animator = GetComponent<Animator>();
	}


	public void TriggerActivate()
	{
		isActivated = !isActivated;
		TriggerActivate (isActivated);
	}

	public void TriggerActivate(bool requested)
	{
		animator.SetBool("isActivated", requested);
        isActivated = requested;
		if (cible == null) 
		{
			print ("This Trigger is't linked with any object");
		} 
		else 
		{
			cible.SendMessage ("Activate", requested);
		}
	}

    public object getData() {
        return new TriggerData(isActivated);
    }

    public void setData(object savedData) {
        TriggerData data = savedData as TriggerData;
        TriggerActivate(data.isActivated);
        animator.SetBool("isActivated", isActivated);
        animator.SetTrigger(data.isActivated ? "forceActive" : "forceInactive");
    }
}
