using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManage : MonoBehaviour
{
    public void ReduceHPClick()
    {
        Boss.Instance.health -= 100;

        Debug.Log(Boss.Instance.health);
    }

    public void DeathStateClick()
    {
        Boss.Instance.health = 1;

        Debug.Log("Boss is death!");
    }
}
