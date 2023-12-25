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

    protected float x = 0f;

    public TextMesh popUpText;


    [SerializeField] private float speed = 20f;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        x = Input.GetAxis("Horizontal");

        Flip();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(x * speed, rb.velocity.y);
    }

    private void Flip()
    {
        if (x < 0f)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }
        else if (x > 0f)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }
    }

    public void TakeDamage(float damage)
    {
        popUpText.text = "-" + damage.ToString();
        // Popup
        Instantiate(popupDamagePrefab, transform.position, Quaternion.identity);
    }
}
