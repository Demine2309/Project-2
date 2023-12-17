using System.Collections;
using UnityEngine;

public class Boss : Enemy
{
    public static Boss Instance;

    [Header("Attack Settings:")]
    public Transform SideAttackTransform; //the middle of the side attack area
    public Vector2 SideAttackArea; //how large the area of side attack is

    public float attackRange;
    public float attackTimer;

    [HideInInspector] public bool attacking;
    [HideInInspector] public float attackCountdown;


    [Header("Ground Check Settings:")]
    [SerializeField] public Transform groundCheckPoint; // Point at which ground check happens
    [SerializeField] private float groundCheckY = 0.2f; // How far down from ground check point is Grounded() checked
    [SerializeField] private float groundCheckX = 0.5f; // How far horizontally from check point to the edge of the dummy is
    [SerializeField] private LayerMask whatIsGround; // Sets the ground layer

    private bool alive;

    [HideInInspector] public bool facingRight;
    [HideInInspector] public float walkSpeed;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    protected override void Start()
    {
        base.Start();

        sr = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        alive = true;

        //ChangeState(EnemyStates.Boss_Stage1);
    }

    protected override void Update()
    {
        base.Update();

        if(!attacking)
        {
            attackCountdown -= Time.deltaTime;
        }
    }


    protected override void UpdateEnemyStates()
    {
        if (DummyController.Instance != null)
        {
            switch (GetCurrentEnemyState)
            {
                case EnemyStates.Boss_Stage1: break;
                case EnemyStates.Boss_Stage2: break;
                case EnemyStates.Boss_Stage3: break;
                case EnemyStates.Boss_Stage4: break;
            }
        }
    }

    #region Boss_Stage1
    IEnumerator SwipeAttack()
    {
        attacking = true;
        rb.velocity = Vector2.zero;

        anim.SetTrigger("Swipe");
        yield return new WaitForSeconds(1.5f);
        anim.ResetTrigger("Swipe");

        anim.SetTrigger("Swipe");
        yield return new WaitForSeconds(0.5f);
        anim.ResetTrigger("Swipe");

        ResetAllAttacks();
    }
    #endregion

    IEnumerator SpitAttack()
    {
        attacking = true;
        rb.velocity = Vector2.zero;

        anim.SetTrigger("Spit");
        yield return new WaitForSeconds(0.3f);
        anim.ResetTrigger("Spit");


        ResetAllAttacks();
    }

    #region Control Attack
    public void AttackHandler()
    {
        if (currentEnemyState == EnemyStates.Boss_Stage1)
        {
            if (Vector2.Distance(DummyController.Instance.transform.position, rb.position) <= attackRange)
            {
                StartCoroutine(SwipeAttack());
            }
        }
    }

    public void ResetAllAttacks()
    {
        attacking = false;

        StopCoroutine(SwipeAttack());
        //StopCoroutine(DoubleSpitAttack());
    }
    #endregion

    public bool Grounded()
    {
        if (Physics2D.Raycast(groundCheckPoint.position, Vector2.down, groundCheckY, whatIsGround)
            || Physics2D.Raycast(groundCheckPoint.position + new Vector3(groundCheckX, 0, 0), Vector2.down, groundCheckY, whatIsGround)
            || Physics2D.Raycast(groundCheckPoint.position + new Vector3(-groundCheckX, 0, 0), Vector2.down, groundCheckY, whatIsGround))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Flip()
    {
        if (DummyController.Instance.transform.position.x < transform.position.x && transform.localScale.x > 0)
        {
            transform.eulerAngles = new Vector2(transform.eulerAngles.x, 180);
            facingRight = true;
        }
        else
        {
            transform.eulerAngles = new Vector2(transform.eulerAngles.x, 0);
            facingRight = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(SideAttackTransform.position, SideAttackArea);
    }

























































































    //private Animator animator;
    //public Transform boss;
    //public Transform dummy;
    //private Rigidbody2D rb;


    //private bool isFlip = false;
    //private Vector3 eMoveDelta;
    //private float distanceBToD;
    //private string currentState;

    //[SerializeField] private int health;
    //[SerializeField] private float speed;

    //// Animation States
    //const string BOSS_IDLE = "Boss_Idle";
    //const string BOSS_WALK = "Boss_Walk";
    //const string BOSS_JUMP = "Boss_Jump";
    //const string BOSS_SWIPE = "Boss_Swipe";
    //const string BOSS_SPIT = "Boss_Spit";
    //const string BOSS_BUFF = "Boss_Buff";

    //private void Awake()
    //{
    //    if(Instance != null && Instance != this)
    //    {
    //        Destroy(gameObject);
    //    }
    //    else
    //    {
    //        Instance = this;
    //    }
    //}

    //private void Start()
    //{
    //    animator = GetComponent<Animator>();
    //    rb = GetComponent<Rigidbody2D>();
    //}

    //private void Update()
    //{
    //    distanceBToD = Vector2.Distance(boss.transform.position, dummy.position);
    //    SwapDirection();


    //    FollowDummy();
    //}

    //private void FollowDummy()
    //{
    //    // Follow the dummy
    //    if (distanceBToD < 18f)
    //    {
    //        ChangeAnimationState(BOSS_WALK);
    //        Vector2 target = new Vector2(dummy.position.x, rb.position.y);
    //        rb.transform.position = Vector3.MoveTowards(rb.position, target, speed * Time.deltaTime);
    //    }
    //    else if (distanceBToD > 18f)
    //    {
    //        ChangeAnimationState(BOSS_IDLE);
    //    }
    //}

    //private void SwapDirection()
    //{
    //    eMoveDelta = transform.localScale;
    //    eMoveDelta.z *= -1f;

    //    if (transform.position.x > dummy.position.x && isFlip)
    //    {
    //        transform.localScale = eMoveDelta;
    //        transform.Rotate(0f, 180f, 0f);
    //        isFlip = false;
    //    }

    //    else if (transform.position.x < dummy.position.x && !isFlip)
    //    {
    //        transform.localScale = eMoveDelta;
    //        transform.Rotate(0f, 180f, 0f);
    //        isFlip = true;
    //    }
    //}

    //private void ChangeAnimationState(string newState)
    //{
    //    // Stop the same animation from interrupting itself
    //    if(currentState == newState) return;

    //    // Play the animation 
    //    animator.Play(newState);

    //    // Reassign the current state
    //    currentState = newState;
    //}

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawLine(boss.transform.position, dummy.position);

    //    Vector3 labelPosition = (boss.transform.position + dummy.position) / 2f;
    //    UnityEditor.Handles.Label(labelPosition, "Distance: " + distanceBToD.ToString("F2"));
    //}
}
