using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlaneController : MonoBehaviour
{
    public Transform playerSpawnPoint;
    private SoundManager soundManager;

    private void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            //other.gameObject.GetComponent<PlayerBehavior>().life.LoseLife();
            //other.gameObject.GetComponent<PlayerBehavior>().health.ReserHP();


            //if(other.gameObject.GetComponent<PlayerBehavior>().life.value > 0)
            //{
            //    ReSpawn(other.gameObject);
            //    //PlaySound
            //    soundManager.PlaySoundFX(SoundFX.DEATH, Channel.PLAYER_DEATH_FX);
            //}
        }
    }

    public void ReSpawn(GameObject player)
    {
        player.transform.position = playerSpawnPoint.position;
    }
}
