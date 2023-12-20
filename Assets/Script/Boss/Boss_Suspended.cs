using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Suspended : StateMachineBehaviour
{
    private Rigidbody2D rb;

    public float heightOffset = 3f;
    private Vector2 originalGravity;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponentInParent<Rigidbody2D>();

        if (rb != null && Boss.Instance != null)
        {
            originalGravity = rb.gravityScale * Physics2D.gravity.normalized;

            Vector2 targetPosition = new Vector2(DummyController.Instance.transform.position.x, DummyController.Instance.transform.position.y + heightOffset);

            rb.transform.SetPositionAndRotation(targetPosition, rb.transform.rotation);
            rb.gravityScale = 0f;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (rb != null)
        {
            rb.gravityScale = originalGravity.magnitude > 0f ? 1f : 0f;
        }
    }
}