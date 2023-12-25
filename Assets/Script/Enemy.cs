using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public float health;
    [SerializeField] public float speed;
    protected float damage;

    [HideInInspector] public Rigidbody2D rb;
    protected SpriteRenderer sr;
    [HideInInspector] public Animator anim;

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
        Boss_State1,
        Boss_State2
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

    protected virtual void Death(float _destroyTime)
    {
        Destroy(gameObject, _destroyTime);
    }

    protected virtual void Attack()
    {
        DummyController.Instance.TakeDamage(damage);
    }
}
