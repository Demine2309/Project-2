using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 moveDelta;
    protected float x = 0f;

    [SerializeField] private float speed = 20f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        x = Input.GetAxis("Horizontal");
        moveDelta = new Vector2(x, 0f);

        SwapDirection();

        rb.velocity = new Vector2(x * speed, rb.velocity.y);
    }

    private void SwapDirection()
    {
        if (moveDelta.x > 0f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (moveDelta.x < 0f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }
}
