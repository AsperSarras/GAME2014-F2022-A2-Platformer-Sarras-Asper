using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSingleton : MonoBehaviour
{
    public static ScoreSingleton Instance { get; private set; }

    public int Min = 0;
    public int Sec = 0;
    public int Score = 0;
    public int Apple = 0;
    public int Kills = 0;
    public bool isTutorial = false;

    public void ResetStats()
    {
        Min = 0;
        Sec = 0;
        Score = 0;
        Apple = 0;
        Kills = 0;
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
