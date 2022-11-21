using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void ToTutorialGame()
    {
        SceneManager.LoadScene(1);
    }
    public void ToMainGame()
    {
        SceneManager.LoadScene(2);
    }
    public void ToEndMenu()
    {
        SceneManager.LoadScene(3);
    }
}
