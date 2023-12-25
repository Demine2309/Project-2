using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReduceBossHP : MonoBehaviour
{
    public void ButtonClick()
    {
        Boss.Instance.health -= 239;

        Debug.Log(Boss.Instance.health);
    }
}
