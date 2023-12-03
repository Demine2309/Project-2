using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAttack : StateMachineBehaviour
{
    private Transform dummy;
    private Boss boss;
    private Rigidbody2D rb;
    private float distanceBToD;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        dummy = GameObject.FindGameObjectWithTag("Dummy").GetComponent<Transform>();
        rb = animator.GetComponentInParent<Rigidbody2D>();
        boss = animator.GetComponentInParent<Boss>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        distanceBToD = Vector2.Distance(boss.transform.position, dummy.position);


    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
