using System.Collections;
using UnityEngine;

public class Boss : Enemy
{
    public static Boss Instance;

    [Header("Attack Settings:")]
    public Transform SideAttackTransform1; // For Swipe attacking   
    public Vector2 SideAttackArea1; // For Swipe attacking

    public Transform SideAttackTransform2; // For Spit attacking
    public Vector2 SideAttackArea2; // For Spit attacking

    public float attackRange1; // For Swipe and spit attacking   
    public float attackTimer;

    [HideInInspector] public bool attacking;
    [HideInInspector] public float attackCountdown;


    [Header("Ground Check Settings:")]
    [SerializeField] public Transform groundCheckPoint; 
    [SerializeField] private float groundCheckY = 0.2f; 
    [SerializeField] private float groundCheckX = 0.5f; 
    [SerializeField] private LayerMask whatIsGround; 

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
        yield return new WaitForSeconds(1f);
        anim.ResetTrigger("Swipe");

        ResetAllAttacks();
    }

    IEnumerator SpitAttack()
    {
        attacking = true;
        rb.velocity = Vector2.zero;

        anim.SetTrigger("Spit");
        yield return new WaitForSeconds(0.3f);
        anim.ResetTrigger("Spit");

        ResetAllAttacks();
    }

    IEnumerator Combo1()
    {
        attacking = true;
        rb.velocity = Vector2.zero;

        anim.SetTrigger("Swipe");
        yield return new WaitForSeconds(2f);
        anim.ResetTrigger("Swipe");

        anim.SetTrigger("Spit");
        yield return new WaitForSeconds(1f);
        anim.ResetTrigger("Spit");

        ResetAllAttacks();
    }
    #endregion

    #region Control Attack
    public void AttackHandler()
    {
        if (currentEnemyState == EnemyStates.Boss_Stage1)
        {
            if (Vector2.Distance(DummyController.Instance.transform.position, rb.position) <= attackRange1)
            {
                ManageTypeOfAttack();
            }
        }
    }

    public void ResetAllAttacks()
    {
        attacking = false;

        StopCoroutine(SwipeAttack());
        StopCoroutine(SpitAttack());
        StopCoroutine(Combo1());
    }

    public void ManageTypeOfAttack()
    {
        float randomValue = Random.value;

        if (randomValue < 0.33f)
        {
            StartCoroutine(SwipeAttack());
        }
        else if(randomValue < 0.66f)
        {
            StartCoroutine(SpitAttack());
        }
        else
        {
            StartCoroutine(Combo1());
        }
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
        Gizmos.DrawWireCube(SideAttackTransform1.position, SideAttackArea1);
        Gizmos.DrawWireCube(SideAttackTransform2.position, SideAttackArea2);
    }
}
