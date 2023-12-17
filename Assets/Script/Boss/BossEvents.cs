using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEvents : MonoBehaviour
{
    void SwipeDamagePlayer()
    {
        if (DummyController.Instance.transform.position.x - transform.position.x != 0)
        {

        }
    }

    void Hit(Transform attackTransform, Vector2 attackArea)
    {
        Collider2D objectsToHit = Physics2D.OverlapBox(attackTransform.position, attackArea, 0);

        if(objectsToHit.GetComponent)
    }
}
