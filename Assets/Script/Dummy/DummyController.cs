using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DummyController : MonoBehaviour
{
    public static DummyController Instance;

    public GameObject popupDamagePrefab;

    private Rigidbody2D rb;
    public SpriteRenderer sr;

    private Vector2 moveDelta;
    protected float x = 0f;
    private long health;

    public TextMesh popUpText;


    [SerializeField] private float speed = 20f;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        health = long.MaxValue;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        x = Input.GetAxis("Horizontal");
        moveDelta = new Vector2(x, 0f);

        Flip();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(x * speed, rb.velocity.y);
    }

    private void Flip()
    {
        if (moveDelta.x > 0f)
        {
            sr.flipX = false;
        }
        else if (moveDelta.x < 0f)
        {
            sr.flipX = true;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= Mathf.RoundToInt(damage);

        popUpText.text = "-" + damage.ToString();
        // Popup
        Instantiate(popupDamagePrefab, transform.position, Quaternion.identity);
    }
}
