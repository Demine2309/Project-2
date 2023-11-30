using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Dummy"))
        {
            ActivateBoss();
            Destroy(this.gameObject);
        }
    }

    private void ActivateBoss()
    {
        Boss boss = GetComponent<Boss>();   

    }
}
