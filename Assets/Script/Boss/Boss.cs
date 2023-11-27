using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    private Animator anim;
    public Transform boss;
    public Transform dummy;

    public int health;
    //public Slider healthBar;
    public bool isDead;
    private float timeBtwDamage = 1.5f;
    private float distanceBToD;

    private void Start()
    {
        anim = GetComponent<Animator>();
        distanceBToD = Vector2.Distance(boss.position, dummy.position);
    }

    private void Update()
    {
        if (health <= 35)
            anim.SetTrigger("buffState");

        if(health <= 0)
            anim.SetTrigger("death");
        
        if(timeBtwDamage > 0)
            timeBtwDamage -= Time.deltaTime;
        
        //healthBar.value = health;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(distanceBToD == 15f && isDead == false)
        {
            
        }
    }

    public void ActiveBoss()
    {

    }
}
