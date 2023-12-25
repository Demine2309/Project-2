using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Boss : Enemy
{
    public static Boss Instance;

    [SerializeField] private float distance;

    public float runSpeed;
    private bool alive;
    [HideInInspector] public bool facingRight;

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

    [Header("Damage Settings:")]
    public float damageSwipe = 20f;
    public float damageSpit = 15;
    public float damageShortJump = 40f;
    public float damageHighJump = 55f;

    [Header("Ground Check Settings:")]
    [SerializeField] public Transform groundCheckPoint;
    [SerializeField] private float groundCheckY = 0.2f;
    [SerializeField] private float groundCheckX = 0.5f;
    [SerializeField] private LayerMask whatIsGround;

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
        ChangeState(EnemyStates.Boss_State1);
        alive = true;
    }

    protected override void Update()
    {
        base.Update();

        if (!attacking)
        {
            attackCountdown -= Time.deltaTime;
        }

        distance = Vector2.Distance(DummyController.Instance.transform.position, rb.position);

        //if (health < 2309 / 3)
        //{
        //    ChangeState(EnemyStates.Boss_State2);
        //    anim.SetTrigger("Buff");
        //}

        //if (health <= 0)
        //{
        //    Death(0);
        //    anim.SetTrigger("Death");
        //}
    }

    protected override void UpdateEnemyStates()
    {
        if (DummyController.Instance != null)
        {
            switch (GetCurrentEnemyState)
            {
                case EnemyStates.Boss_State1:
                    attackTimer = 3;
                    runSpeed = speed;
                    break;
                case EnemyStates.Boss_State2:
                    attackTimer = 1;
                    runSpeed = 3.5f;

                    damageSwipe = 35;
                    damageSpit = 30;
                    damageShortJump = 65;
                    break;
            }
        }
    }

    #region Boss_State1
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

    IEnumerator HighJumpAttack()
    {
        attacking = true;

        anim.SetTrigger("Jump");
        yield return new WaitForSeconds(2f);

        anim.SetTrigger("Suspended");
        yield return new WaitForSeconds(1f);

        if (Grounded() == true)
        {
            anim.SetTrigger("Land");
        }

        ResetAllAttacks();
    }

    IEnumerator ShortJumpAttack()
    {
        attacking = true;
        rb.velocity = Vector2.zero;

        anim.SetTrigger("JumpShort");
        yield return new WaitForSeconds(1f);
        anim.ResetTrigger("JumpShort");

        ResetAllAttacks();
    }
    #endregion

    #region Boss State 2
    IEnumerator TripleSwipeAttack()
    {
        attacking = true;
        rb.velocity = Vector2.zero;

        anim.SetTrigger("Swipe");
        yield return new WaitForSeconds(2f);
        anim.ResetTrigger("Swipe");

        anim.SetTrigger("Swipe");
        yield return new WaitForSeconds(1.5f);
        anim.ResetTrigger("Swipe");

        anim.SetTrigger("Swipe");
        yield return new WaitForSeconds(1f);
        anim.ResetTrigger("Swipe");

        ResetAllAttacks();
    }

    IEnumerator DoubleSpitAttack()
    {
        attacking = true;
        rb.velocity = Vector2.zero;

        anim.SetTrigger("Spit");
        yield return new WaitForSeconds(1.5f);
        anim.ResetTrigger("Spit");

        anim.SetTrigger("Spit");
        anim.ResetTrigger("Spit");

        ResetAllAttacks();
    }

    IEnumerator DoubleShortJumpAttack()
    {
        attacking = true;
        rb.velocity = Vector2.zero;

        anim.SetTrigger("JumpShort");
        anim.ResetTrigger("JumpShort");

        anim.SetTrigger("JumpShort");
        yield return new WaitForSeconds(3f);
        anim.ResetTrigger("JumpShort");

        ResetAllAttacks();
    }
    #endregion

    #region Control Attack
    public void AttackHandler()
    {
        if (currentEnemyState == EnemyStates.Boss_State1)
        {
            float randomValue = Random.value;

            if (randomValue < 0.75f)
            {
                if (Vector2.Distance(DummyController.Instance.transform.position, rb.position) <= attackRange)
                    ManageTypeOfAttack1();
            }
            else if (randomValue < 0.9f)
            {
                if (Vector2.Distance(DummyController.Instance.transform.position, rb.position) <= jumpAttackRange)
                    StartCoroutine(ShortJumpAttack());
            }
            else
            {
                StartCoroutine(HighJumpAttack());
            }
        }

        if (currentEnemyState == EnemyStates.Boss_State2)
        {
            float randomValue = Random.value;

            if (randomValue < 0.3f)
            {
                if (Vector2.Distance(DummyController.Instance.transform.position, rb.position) <= attackRange)
                    ManageTypeOfAttack1();
            }
            else if (randomValue < 0.6f)
            {
                if (Vector2.Distance(DummyController.Instance.transform.position, rb.position) <= attackRange)
                    StartCoroutine(TripleSwipeAttack());
            }
            else
            {
                if (Vector2.Distance(DummyController.Instance.transform.position, rb.position) <= jumpAttackRange)
                    StartCoroutine(DoubleShortJumpAttack());
            }
        }
    }

    public void ResetAllAttacks()
    {
        attacking = false;

        StopCoroutine(SwipeAttack());
        StopCoroutine(SpitAttack());
        StopCoroutine(HighJumpAttack());
        StopCoroutine(ShortJumpAttack());

        StopCoroutine(TripleSwipeAttack());
        StopCoroutine(DoubleSpitAttack());
        StopCoroutine(DoubleShortJumpAttack());
    }

    public void ManageTypeOfAttack1()
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

    public void ManageTypeOfAttack2()
    {
        float randomValue = Random.value;

        if (randomValue < 0.5f)
        {
            StartCoroutine(TripleSwipeAttack());
        }
        else
        {
            StartCoroutine(DoubleSpitAttack());
        }
    }
    #endregion

    protected override void Death(float _destroyTime)
    {
        ResetAllAttacks();
        alive = false;
        rb.velocity = new Vector2(rb.velocity.x, -25);
        anim.SetTrigger("Death");
    }

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
            facingRight = false;
        }
        else
        {
            transform.eulerAngles = new Vector2(transform.eulerAngles.x, 0);
            facingRight = true;
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
