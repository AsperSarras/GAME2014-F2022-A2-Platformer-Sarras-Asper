using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControllerPlayer : MonoBehaviour
{
    public Vector2 direction;
    public Rigidbody2D rigidbody2D;
    [Range(1.0f, 30.0f)]
    public float force;
    public Vector3 offset;
    public GameObject TargetObject;
    public Transform TargetTransform;
    public PlayerBehavior player;

    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerBehavior>();
        TargetTransform = TargetObject.transform;
    }

    private void Update()
    {
        TargetTransform = TargetObject.transform;
    }

    public void Activate(bool isRight)
    {
        Vector3 playerPosition = TargetTransform.position + offset;
        direction = (playerPosition - transform.position).normalized;

        if(isRight == true)
        {
            direction.x *= 1;
        }
        else
        {
            direction.x *= -1;
        }


        Rotate();
        Move();
        Invoke("DestroyYourself", 2.0f);
    }


    // Update is called once per frame
    private void Rotate()
    {
        rigidbody2D.AddTorque(Random.Range(5.0f, 15.0f) * direction.x * -1.0f, ForceMode2D.Impulse);
    }

    private void Move()
    {
        rigidbody2D.AddForce(direction * force, ForceMode2D.Impulse);
    }

    private void DestroyYourself()
    {
        if (gameObject.activeInHierarchy)
        {
            Destroy(gameObject);
        }
    }

    //public void ResetAllPhysics()
    //{
    //    rigidbody2D.velocity = Vector2.zero;
    //    rigidbody2D.angularVelocity = 0;
    //    direction = Vector2.zero;
    //}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            player.i_Score += 50;
            player.i_EnemyKilled++;
            Destroy(other.gameObject);
            DestroyYourself();
        }
        else if (other.gameObject.CompareTag("Ground")
            || other.gameObject.CompareTag("Prop")
            || other.gameObject.CompareTag("Platform"))
        {
            DestroyYourself();
        }
    }
}
