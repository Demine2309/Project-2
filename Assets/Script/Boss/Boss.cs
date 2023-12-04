using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    private Animator animator;
    public Transform boss;
    public Transform dummy;
    private Rigidbody2D rb;


    private bool isFlip = false;
    private Vector3 eMoveDelta;
    private float distanceBToD;
    private string currentState;

    [SerializeField] private int health;
    [SerializeField] private float speed;

    // Animation States
    const string BOSS_IDLE = "Boss_Idle";
    const string BOSS_WALK = "Boss_Walk";
    const string BOSS_JUMP = "Boss_Jump";
    const string BOSS_LAND = "Boss_Land";
    const string BOSS_SWIPE = "Boss_Swipe";
    const string BOSS_SPIT = "Boss_Spit";
    const string BOSS_BUFF = "Boss_Buff"; 


    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        distanceBToD = Vector2.Distance(boss.transform.position, dummy.position);
    }

    public void FollowPlayer()
    {
        eMoveDelta = transform.localScale;
        eMoveDelta.z *= -1f;

        if (transform.position.x > dummy.position.x && isFlip)
        {
            transform.localScale = eMoveDelta;
            transform.Rotate(0f, 180f, 0f);
            isFlip = false;
        }

        else if (transform.position.x < dummy.position.x && !isFlip)
        {
            transform.localScale = eMoveDelta;
            transform.Rotate(0f, 180f, 0f);
            isFlip = true;
        }

        // Follow the dummy
        Vector2 target = new Vector2(dummy.position.x, rb.position.y);
        rb.transform.position = Vector3.MoveTowards(rb.position, target, speed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(boss.transform.position, dummy.position);

        Vector3 labelPosition = (boss.transform.position + dummy.position) / 2f;
        UnityEditor.Handles.Label(labelPosition, "Distance: " + distanceBToD.ToString("F2"));
    }

    private void ChangeAnimationState(string newState)
    {
        // Stop the same animation from interrupting itself
        if(currentState == newState) return;

        // Play the animation 
        animator.Play(newState);

        // Reassign the current state
        currentState = newState;
    }
}
