using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void ToTutorialGame()
    {
        ScoreSingleton.Instance.ResetStats();
        ScoreSingleton.Instance.isTutorial = true;
        SceneManager.LoadScene(1);
    }
    public void ToMainGame()
    {
        ScoreSingleton.Instance.ResetStats();
        ScoreSingleton.Instance.isTutorial = false;
        SceneManager.LoadScene(2);
    }
    public void ToEndMenu()
    {
        SceneManager.LoadScene(3);
    }
    public void Replay()
    {
        if(ScoreSingleton.Instance.isTutorial == true)
        {
            ToTutorialGame();
        }
        else
        {
            ToMainGame();
        }
    }    
}
