using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Boss : Enemy
{
    public static Boss Instance;

    [SerializeField] private float distance;

    [Header("Attack Settings:")]
    public Transform sideAttackTransform1; // For Swipe attacking   
    public Vector2 sideAttackArea1; // For Swipe attacking

    public Transform sideAttackTransform2; // For Spit attacking
    public Vector2 sideAttackArea2; // For Spit attacking

    public Transform landAttackTransform;
    public Vector2 LandAttackArea;

    public float attackRange;
    public float jumpAttackRange;
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
            alive = true;
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
    }

    protected override void Update()
    {
        base.Update();

        if (!attacking)
        {
            attackCountdown -= Time.deltaTime;
        }

        distance = Vector2.Distance(DummyController.Instance.transform.position, rb.position);
    }

    protected override void UpdateEnemyStates()
    {
        if (DummyController.Instance != null)
        {
            switch (GetCurrentEnemyState)
            {
                case EnemyStates.Boss_Stage1:
                    break;
                case EnemyStates.Boss_Stage2:
                    break;
                case EnemyStates.Boss_Stage3:
                    break;
                case EnemyStates.Boss_Stage4:
                    break;
            }
        }
    }

    #region Boss_Stage1
    IEnumerator SwipeAttack()
    {
        attacking = true;
        rb.velocity = Vector2.zero;

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
        yield return new WaitForSeconds(1f);
        anim.ResetTrigger("Spit");

        ResetAllAttacks();
    }

    IEnumerator JumpAttack()
    {
        attacking = true;

        anim.SetTrigger("Jump");
        yield return new WaitForSeconds(2f);

        anim.SetBool("Suspended", true);

        ResetAllAttacks();
    }

    IEnumerator JumpShortAttack()
    {
        attacking = true;
        //rb.velocity = Vector2.zero;

        anim.SetTrigger("JumpShort");
        yield return new WaitForSeconds(1f);
        anim.ResetTrigger("JumpShort");

        ResetAllAttacks();
    }
    #endregion

    #region Control Attack
    public void AttackHandler()
    {
        if (currentEnemyState == EnemyStates.Boss_Stage1)
        {
            float randomValue = Random.value;
            if(randomValue < 0.75f)
            {
                if (Vector2.Distance(DummyController.Instance.transform.position, rb.position) <= attackRange)
                    ManageTypeOfAttack();
            }
            else if (randomValue < 0.95f)
            {
                if (Vector2.Distance(DummyController.Instance.transform.position, rb.position) <= jumpAttackRange)
                    StartCoroutine(JumpShortAttack());
            }
            else
            {
                StartCoroutine(JumpAttack());
            }
        }
    }

    public void ResetAllAttacks()
    {
        attacking = false;

        StopCoroutine(SwipeAttack());
        StopCoroutine(SpitAttack());
        StopCoroutine(JumpAttack());
        StopCoroutine(JumpShortAttack());
    }

    public void ManageTypeOfAttack()
    {
        float randomValue = Random.value;

        if (randomValue < 0.5f)
        {
            StartCoroutine(SwipeAttack());
        }
        else
        {
            StartCoroutine(SpitAttack());
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
        Gizmos.DrawWireCube(sideAttackTransform1.position, sideAttackArea1);
        Gizmos.DrawWireCube(sideAttackTransform2.position, sideAttackArea2);
        Gizmos.DrawWireCube(landAttackTransform.position, LandAttackArea);
    }
}
