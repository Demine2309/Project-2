using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEvents : MonoBehaviour
{
    void SwipeDamageDummy()
    {
        if (DummyController.Instance.transform.position.x - transform.position.x != 0)
        {
            Hit(Boss.Instance.SideAttackTransform1, Boss.Instance.SideAttackArea1);
        }
    }

    void SpitDamageDummy()
    {
        if (DummyController.Instance.transform.position.x - transform.position.x != 0)
        {
            Hit(Boss.Instance.SideAttackTransform2, Boss.Instance.SideAttackArea2);
        }

    }

    void Hit(Transform attackTransform, Vector2 attackArea)
    {
        Collider2D objectsToHit = Physics2D.OverlapBox(attackTransform.position, attackArea, 0);

        if(objectsToHit.GetComponent<DummyController>() != null)
        {
            objectsToHit.GetComponent<DummyController>().TakeDamage(Boss.Instance.damage);
        }
    }
}
