using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    public Transform playerTransform;
    public bool playerDetected;
    public bool LOS;
    public Collider2D ColliderHits;
    public LayerMask collisionLayerMask;
    public Vector2 playerDirectionVector;
    public float playerDirection;
    public float enemyDirection;
    // Start is called before the first frame update
    void Start()
    {
        playerDirectionVector = Vector2.zero;
        playerDirection = 0;
        playerTransform = FindObjectOfType<PlayerBehavior>().transform;
        LOS = false;
        playerDetected = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerDetected)
        {
            var hits = Physics2D.Linecast(transform.position, playerTransform.position, collisionLayerMask);

            ColliderHits = hits.collider;
            playerDirectionVector = playerTransform.position - transform.position;
            playerDirection = (playerDirectionVector.x > 0) ? 1.0f : -1.0f;
            enemyDirection = GetComponentInParent<EnemyController>().direction.x;
            
            
            LOS = ((hits.collider.gameObject.name == "Player") && (playerDirection == enemyDirection));


        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            playerDetected = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if(playerDetected)
        {
            Gizmos.DrawLine(transform.position, playerTransform.position);
        }
        Gizmos.color = (playerDetected) ? Color.red : Color.green;

        Gizmos.DrawWireSphere(transform.position, 15.0f);
    }
}
