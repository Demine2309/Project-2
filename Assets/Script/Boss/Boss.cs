using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    private Animator anim;
    public Transform boss;
    public Transform dummy;

    public int health;
    //public Slider healthBar;
    public bool isDead = true;
    private float timeBtwDamage = 1.5f;
    public float distanceBToD = 25;
    private bool isFlip = false;
    private Vector3 eMoveDelta;


    private void Start()
    {
        anim = GetComponent<Animator>();
        distanceBToD = Vector2.Distance(boss.position, dummy.position);
    }

    private void Update()
    {
        if (health <= 35)
            anim.SetTrigger("buffState");

        if (health <= 0)
            anim.SetTrigger("death");

        if (timeBtwDamage > 0)
            timeBtwDamage -= Time.deltaTime;

        //healthBar.value = health;
    }

    public void FollowPlayer()
    {
        eMoveDelta = transform.localScale;
        eMoveDelta.z *= -1f;

        if (transform.position.x > dummy.position.x && isFlip)
        {
            transform.localScale = eMoveDelta;
            transform.Rotate(0f, 180f, 0f);
            isFlip = false;
        }

        else if (transform.position.x < dummy.position.x && !isFlip)
        {
            transform.localScale = eMoveDelta;
            transform.Rotate(0f, 180f, 0f);
            isFlip = true;
        }
    }
}
