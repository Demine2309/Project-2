using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void OptionsScene()
    {
        SceneManager.LoadScene("Options Menu");
    }

    public void CreditsScene()
    {
        SceneManager.LoadScene("Credits Scene");
    }

    public void PlayScene()
    {
        SceneManager.LoadScene("Play");
    }

    public void BackClick()
    {

        SceneManager.LoadScene("Main Menu");
    }

    public void QuitGame()
    {
        Application.Quit(); 
    }
}
