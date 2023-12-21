using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float health;
    [SerializeField] public float speed;
    [SerializeField] protected float damage;

    protected Rigidbody2D rb;
    protected SpriteRenderer sr;
    protected Animator anim;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            UpdateEnemyStates();
        }
    }

    protected enum EnemyStates
    {
        Boss_Stage1,
        Boss_Stage2,
        Boss_Stage3,
        Boss_Stage4
    }

    protected EnemyStates currentEnemyState;

    protected virtual EnemyStates GetCurrentEnemyState
    {
        get { return currentEnemyState; }
        set
        {
            if (currentEnemyState != value)
            {
                currentEnemyState = value;

                ChangeCurrentAnimation();
            }
        }
    }

    protected virtual void UpdateEnemyStates() { }
    protected virtual void ChangeCurrentAnimation() { }

    protected void ChangeState(EnemyStates newState)
    {
        GetCurrentEnemyState = newState;
    }

    protected virtual void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Dummy"))
        {
            Attack();
        }
    }

    protected virtual void Attack()
    {
        DummyController.Instance.TakeDamage(damage);
    }
}
