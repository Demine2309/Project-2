using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyController : MonoBehaviour
{
    public static DummyController Instance;

    private Rigidbody2D rb;
    private Vector2 moveDelta;
    protected float x = 0f;
    private long health;


    [SerializeField] private float speed = 20f;


    private void Awake()
    {
        if(Instance == null)
            Instance = this;

        health = long.MaxValue;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        x = Input.GetAxis("Horizontal");
        moveDelta = new Vector2(x, 0f);

        SwapDirection();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(x * speed, rb.velocity.y);
    }

    private void SwapDirection()
    {
        if (moveDelta.x > 0f)
        {
            transform.localScale = new Vector3(1.35f, 1.35f, 1.35f);
        }
        else if (moveDelta.x < 0f)
        {
            transform.localScale = new Vector3(-1.35f, 1.35f, 1.35f);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= Mathf.RoundToInt(damage);
    }
}
