using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEvents : MonoBehaviour
{
    void SwipeDamageDummy()
    {
        if (DummyController.Instance.transform.position.x - transform.position.x != 0)
        {
            Hit(Boss.Instance.sideAttackTransform1, Boss.Instance.sideAttackArea1);
        }
    }

    void SpitDamageDummy()
    {
        if (DummyController.Instance.transform.position.x - transform.position.x != 0)
        {
            Hit(Boss.Instance.sideAttackTransform2, Boss.Instance.sideAttackArea2);
        }
    }

    void LandDamageDummy()
    {
        if (DummyController.Instance.transform.position.x - transform.position.x != 0)
        {
            Hit(Boss.Instance.landAttackTransform, Boss.Instance.LandAttackArea);
        }
    }

    void ShortJumpDamageDummy()
    {
        if (DummyController.Instance.transform.position.x - transform.position.x != 0)
        {
            Hit(Boss.Instance.landAttackTransform, Boss.Instance.LandAttackArea);
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
