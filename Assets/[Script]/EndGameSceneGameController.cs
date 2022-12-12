using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameSceneGameController : MonoBehaviour
{
    private SoundManager soundManager;
    // Start is called before the first frame update
    void Start()
    {
        soundManager = GetComponent<SoundManager>();

        soundManager.PlaySoundFX(SoundFX.PLAYERDEATH, Channel.PLAYER_DEATH_FX);
        soundManager.PlayMusic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
