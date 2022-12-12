using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{ 
    public bool checkPointPass = false;
    public SoundManager soundManager;

    public void Start()
    {
            soundManager = FindObjectOfType<SoundManager>();
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (checkPointPass == false)
        {
            if (other.gameObject.name == "Player")
            {
                soundManager.PlaySoundFX(SoundFX.CHECKPOINT, Channel.SCORE_FX);
                FindObjectOfType<DeathPlaneController>().playerSpawnPoint = transform;
                checkPointPass = true;
            }
        }
    }
}
