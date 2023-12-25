using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEvents : MonoBehaviour
{
    void SwipeDamageDummy()
    {
        if (DummyController.Instance.transform.position.x - transform.position.x != 0)
        {
            SwipeHit(Boss.Instance.sideAttackTransform1, Boss.Instance.sideAttackArea1);
        }
    }

    void SpitDamageDummy()
    {
        if (DummyController.Instance.transform.position.x - transform.position.x != 0)
        {
            SpitHit(Boss.Instance.sideAttackTransform2, Boss.Instance.sideAttackArea2);
        }
    }

    void HighJumpDamageDummy()
    {
        if (DummyController.Instance.transform.position.x - transform.position.x != 0)
        {
            HighJumpHit(Boss.Instance.landAttackTransform, Boss.Instance.LandAttackArea);
        }
    }

    void ShortJumpDamageDummy()
    {
        if (DummyController.Instance.transform.position.x - transform.position.x != 0)
        {
            ShortJumpHit(Boss.Instance.landAttackTransform, Boss.Instance.LandAttackArea);
        }
    }

    void SwipeHit(Transform attackTransform, Vector2 attackArea)
    {
        Collider2D objectsToHit = Physics2D.OverlapBox(attackTransform.position, attackArea, 0);

        if (objectsToHit.GetComponent<DummyController>() != null)
        {
            objectsToHit.GetComponent<DummyController>().TakeDamage(Boss.Instance.damageSwipe);
        }
    }

    void SpitHit(Transform attackTransform, Vector2 attackArea)
    {
        Collider2D objectsToHit = Physics2D.OverlapBox(attackTransform.position, attackArea, 0);

        if (objectsToHit.GetComponent<DummyController>() != null)
        {
            objectsToHit.GetComponent<DummyController>().TakeDamage(Boss.Instance.damageSpit);
        }
    }

    void HighJumpHit(Transform attackTransform, Vector2 attackArea)
    {
        Collider2D objectsToHit = Physics2D.OverlapBox(attackTransform.position, attackArea, 0);

        if (objectsToHit.GetComponent<DummyController>() != null)
        {
            objectsToHit.GetComponent<DummyController>().TakeDamage(Boss.Instance.damageHighJump);
        }
    }

    void ShortJumpHit(Transform attackTransform, Vector2 attackArea)
    {
        Collider2D objectsToHit = Physics2D.OverlapBox(attackTransform.position, attackArea, 0);

        if (objectsToHit.GetComponent<DummyController>() != null)
        {
            objectsToHit.GetComponent<DummyController>().TakeDamage(Boss.Instance.damageShortJump);
        }
    }

    void DestroyAfterDeath()
    {
        Boss.Instance.DestroyAfterDeath();
    }
}
