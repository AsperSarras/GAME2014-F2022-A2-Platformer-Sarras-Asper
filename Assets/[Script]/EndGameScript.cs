using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGameScript : MonoBehaviour
{
    public TMP_Text TimeM;
    public TMP_Text TimeS;
    public TMP_Text EKills;
    public TMP_Text Score;
    public TMP_Text Apples;

    // Start is called before the first frame update
    void Start()
    {
        TimeM.text = ScoreSingleton.Instance.Min.ToString();
        TimeS.text = ScoreSingleton.Instance.Sec.ToString();
        EKills.text = ScoreSingleton.Instance.Kills.ToString();
        Score.text = ScoreSingleton.Instance.Score.ToString();
        Apples.text = ScoreSingleton.Instance.Apple.ToString();


    }
}
