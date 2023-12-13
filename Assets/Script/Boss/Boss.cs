using UnityEngine;

public class Boss : Enemy
{
    public static Boss Instance;

    [Header("Attack Settings:")]

    public float attackRange;
    public float attackTimer;

    [Header("Ground Check Settings:")]
    [SerializeField] private Transform groundCheckPoint; // Point at which ground check happens
    [SerializeField] private float groundCheckY = 0.2f; // How far down from ground check point is Grounded() checked
    [SerializeField] private float groundCheckX = 0.5f; // How far horizontally from check point to the edge of the dummy is
    [SerializeField] private LayerMask whatIsGround; // Sets the ground layer

    private bool alive;

    [SerializeField] private float walkSpeed;

    private void Awake()
    {
        if(Instance!= null && Instance != this)
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
        sr = GetComponentInChildren<>();
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
