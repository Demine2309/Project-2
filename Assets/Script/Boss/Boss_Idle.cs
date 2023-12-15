using UnityEngine;

public class Boss_Idle : StateMachineBehaviour
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
        rb.velocity = Vector2.zero;
        TargetPlayerPosition(animator);

        if(Boss.Instance.attackCountdown <= 0)
        {
            Boss.Instance.AttackHandler();
            Boss.Instance.attackCountdown = Random.Range(Boss.Instance.attackTimer - 1, Boss.Instance.attackTimer + 1);
        }
    }

    private void TargetPlayerPosition(Animator animator)
    {
        if (Boss.Instance.Grounded())
        {
            Boss.Instance.SwapDirection();
            Vector2 target = new Vector2(DummyController.Instance.transform.position.x, rb.position.y);
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, Boss.Instance.walkSpeed * Time.fixedDeltaTime);

            rb.MovePosition(newPos);
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, -25); // if boss is not grounded, fall to ground
        }

        if(Vector2.Distance(DummyController.Instance.transform.position, rb.position) >= Boss.Instance.attackRange)
        {
            animator.SetBool("Walk", true);
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
