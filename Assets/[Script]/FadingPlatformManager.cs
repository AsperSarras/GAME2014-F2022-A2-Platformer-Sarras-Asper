using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingPlatformManager : MonoBehaviour
{
    public GameObject fadingBlock;
    public float timer;
    public float fadeInTimer;
    public float fadeOutTimer;
    public bool isOn;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        isOn = fadingBlock.active;
        
        if(isOn)
        {
            if (timer >= fadeOutTimer)
            {
                FadeOut();
            }
        }
        else
        {
            if (timer >= fadeInTimer)
            {
                FadeIn();
            }
        }

    }

    void FadeIn()
    {
        fadingBlock.SetActive(true);
        timer = 0;
    }

    void FadeOut()
    {
        fadingBlock.SetActive(false);
        timer = 0;
    }

}
