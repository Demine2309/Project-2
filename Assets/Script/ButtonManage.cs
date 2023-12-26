using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManage : MonoBehaviour
{
    public void ReduceHPClick()
    {
        Boss.Instance.SetHealth(100f);

        Debug.Log(Boss.Instance.health);
    }

    public void DeathStateClick()
    { 
        Boss.Instance.SetHealth(Boss.Instance.health - 5);

        Debug.Log("Boss is death!");
    }
}
