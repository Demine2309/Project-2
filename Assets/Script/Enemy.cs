using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float health;
    [SerializeField] protected float speed;
    [SerializeField] protected float damage;

    protected Rigidbody2D rb;
    protected SpriteRenderer sr;
    protected Animator anim;

    protected enum EnemyStates
    {
        Boss_Idle,
        Boss_Walk,
        Boss_Spit,
        Boss_Swipe,
        Boss_Jump,
        Boss_Land
    }

    protected virtual void Start()
    {
        
    }

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    protected void OnCollision2D(Collision2D collision)
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
