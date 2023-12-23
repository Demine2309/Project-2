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

    [Header("Recoil Settings:")]
    [HideInInspector] public bool recoilingX;
    [HideInInspector] public bool lookingRight;
    [SerializeField] private float recoilXSpeed = 100;
    [SerializeField] private int recoilXSteps = 5;
    private int stepsXRecoiled;
    [Space(5)]

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

        Flip();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(x * speed, rb.velocity.y);
        Recoil();
    }

    private void Flip()
    {
        if (x < 0f)
        {
            transform.eulerAngles = new Vector2(0, 180);
            lookingRight = false;
        }
        else if (x > 0f)
        {
            transform.eulerAngles = new Vector2(0, 0);
            lookingRight = true;
        }
    }

    private void Recoil()
    {
        if(recoilingX)
        {
            if (lookingRight)
            {
                rb.velocity = new Vector2(-recoilXSpeed, 0);
            }
            else
            {
                rb.velocity = new Vector2(recoilXSpeed, 0);
            }
        }

        // Stop recoil
        if(recoilingX && stepsXRecoiled < recoilXSteps)
        {
            stepsXRecoiled++;
        }
        else
        {
            StopRecoilX();
        }
    }

    private void StopRecoilX()
    {
        stepsXRecoiled = 0;
        recoilingX = false;
    }

    public void TakeDamage(float damage)
    {
        health -= Mathf.RoundToInt(damage);

        popUpText.text = "-" + damage.ToString();
        // Popup
        Instantiate(popupDamagePrefab, transform.position, Quaternion.identity);
    }
}
