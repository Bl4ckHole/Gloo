using UnityEngine;
using System.Collections;

public class AnimatorCreateDivScript : StateMachineBehaviour {

    public int facing;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
/*    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (!animator.GetBool("DoCreate"))
            return;
        int facing_int = facing == 1 ? -1 : 1;
        Object divInst = Instantiate(animator.gameObject.GetComponent<glooScript>().div, animator.gameObject.transform.position + new Vector3(animator.gameObject.GetComponent<glooScript>().divcoll.size.x /2.0f+1.2f, 0, 0) * facing_int, new Quaternion());
        divInst.name = "divInstance" +glooScript.divID++;
    }
    */
	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
