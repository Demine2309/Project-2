using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkBehavior : StateMachineBehaviour
{
    [SerializeField] private float speed = 10f;

    private Transform dummy;
    private Boss boss;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        dummy = GameObject.FindGameObjectWithTag("Dummy").GetComponent<Transform>();
        boss = animator.GetComponentInParent<Boss>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.FollowPlayer();


        Vector2 target = new Vector2(dummy.position.x, animator.transform.position.y);
        animator.transform.position = Vector3.MoveTowards(animator.transform.position, target, speed);

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
