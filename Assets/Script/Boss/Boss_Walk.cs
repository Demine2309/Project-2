using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Walk : StateMachineBehaviour
{
    Rigidbody2D rb;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponentInParent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        TargetPlayerPosition(animator);

        if (Boss.Instance.attackCountdown <= 0)
        {
            Boss.Instance.AttackHandler();
            Boss.Instance.attackCountdown = Random.Range(Boss.Instance.attackTimer - 1, Boss.Instance.attackTimer + 1);
        }
    }

    private void TargetPlayerPosition(Animator animator)
    {
        if (Boss.Instance.Grounded())
        {
            Boss.Instance.Flip();

            Vector2 target = new Vector2(DummyController.Instance.transform.position.x, rb.position.y);
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, Boss.Instance.runSpeed * Time.fixedDeltaTime);

            rb.MovePosition(newPos);
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, -25);
        }

        if (Vector2.Distance(DummyController.Instance.transform.position, rb.position) <= Boss.Instance.attackRange)
        {
            animator.SetBool("Walk", false);
        }
        else
        {
            return;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Walk", false);
    }
}
