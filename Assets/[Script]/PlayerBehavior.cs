using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehavior : MonoBehaviour
{
    public float horizontalForce;
    public float horizontalSpeed;
    public float verticalForce;
    public float airFactor;
    public Transform groundPoint;
    public float groundRadius;
    public LayerMask groundLayerMask;
    public bool isGrounded;

    private Rigidbody2D rb;

    public Animator animator;
    public PlayerAnimState playerAnimState;

    public ParticleSystem dustTrail;
    public Color dustTrailColour;

    [Header("HpSystem")]
    public List<GameObject> l_lifes;
    int currentLifes = 3;

    [Header("ScoreSystem")]
    public float f_sec;
    public int i_Sec;
    public int i_Min;
    public int i_AppleGet;
    public int i_EnemyKilled;
    public TMP_Text SecText;
    public TMP_Text MinText;
    public int i_Score;
    public TMP_Text ScoreText;

    public DeathPlaneController deathPlane;

    public Joystick leftJoystick;
    [Range(0.1f, 1.0f)]
    public float verticalThreshold;

    public CinemachineVirtualCamera playerCamera;
    public CinemachineBasicMultiChannelPerlin perlin;
    public float shakeIntensity;
    public float shakeDuration;
    public float shakeTimer;
    public bool isCameraShaking;

    public SoundManager soundManager;

    public GameObject playerBullet;
    public Transform startingPlace;

    public bool lookingRight = true;
    bool won = false;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        deathPlane = FindObjectOfType<DeathPlaneController>();
        soundManager = FindObjectOfType<SoundManager>();
        leftJoystick = (Application.isMobilePlatform) ? GameObject.Find("LeftStick").GetComponent<Joystick>() : null;
        //leftJoystick = GameObject.Find("LeftStick").GetComponent<Joystick>();
        playerCamera = GameObject.Find("PlayerCamera").GetComponent<CinemachineVirtualCamera>();
        perlin = playerCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        isCameraShaking = false;
        shakeTimer = shakeDuration;

        dustTrail = GetComponentInChildren<ParticleSystem>();
    }

    void Update()
    {
        SecText.text = i_Sec.ToString();
        MinText.text = i_Min.ToString();
        ScoreText.text = i_Score.ToString();

        f_sec += Time.deltaTime;

        i_Sec = (int)f_sec;

        if(f_sec >= 60)
        {
            f_sec = 0;
            i_Min++;
        }

        if(Input.GetKeyDown(KeyCode.Z))
        {
            Shoot();
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var hit = Physics2D.OverlapCircle(groundPoint.position, groundRadius, groundLayerMask);
        isGrounded = hit;
        Move();
        Jump();
        AirCheck();

        if (isCameraShaking)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0.0f) // timed out
            {
                perlin.m_AmplitudeGain = 0.0f;
                shakeTimer = shakeDuration;
                isCameraShaking = false;
            }
        }
    }

    private void Move()
    {
        var x = Input.GetAxisRaw("Horizontal") + ((Application.isMobilePlatform) ? leftJoystick.Horizontal : 0.0f);
        //var x = Input.GetAxisRaw("Horizontal") + leftJoystick.Horizontal;

        if (x != 0.0f)
        {
            Flip(x);

            x = ((x > 0.0) ? 1.0f : -1.0f); // if x is greater than 0 is = 1 else if is less than 0 is -1
            rb.AddForce(Vector2.right * x * horizontalForce * ((isGrounded) ? 1.0f : airFactor));

            var clampXVel = Mathf.Clamp(rb.velocity.x, -horizontalSpeed, horizontalSpeed);
            rb.velocity = new Vector2(clampXVel, rb.velocity.y);

            ChangeAnimation(PlayerAnimState.RUN);

            if (isGrounded)
            {
                CreateDustTrail();
            }
        }

        if ((isGrounded) && (x == 0))
        {
            ChangeAnimation(PlayerAnimState.IDLE);
        }
    }

    private void CreateDustTrail()
    {
        dustTrail.GetComponent<Renderer>().material.SetColor("_Color", dustTrailColour);
        dustTrail.Play();
    }

    private void ShakeCamera()
    {
        perlin.m_AmplitudeGain = shakeIntensity;
        isCameraShaking = true;
    }

    public void Jump()
    {
        var y = Input.GetAxis("Jump") + ((Application.isMobilePlatform) ? leftJoystick.Vertical : 0.0f);

        if ((isGrounded) && y > verticalThreshold)
        {
            rb.AddForce(Vector2.up * verticalForce, ForceMode2D.Impulse);
            soundManager.PlaySoundFX(SoundFX.JUMP, Channel.PLAYER_FX);
        }
    }

    public void JumpB()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector2.up * verticalForce, ForceMode2D.Impulse);
            soundManager.PlaySoundFX(SoundFX.JUMP, Channel.PLAYER_FX);
        }
    }

    public void Flip(float x)
    {
        if(x > 0)
        { 
            lookingRight = true; 
        }
        else if(x < 0)
        {
            lookingRight = false;
        }

        if (x != 0.0f)
        {
            transform.localScale = new Vector3((x > 0.0f) ? 1.0f : -1.0f, 1.0f, 1.0f);
        }
    }

    public void Shoot()
    {
        GameObject bullet = Instantiate(playerBullet);
        bullet.transform.position = startingPlace.transform.position;
        bullet.GetComponent<BulletControllerPlayer>().Activate(lookingRight);
    }

    private void ChangeAnimation(PlayerAnimState animState)
    {
        // Change the Animation to RUN
        //state = animState;
        playerAnimState = animState;
        animator.SetInteger("AnimState", (int)playerAnimState);
    }

    private void AirCheck()
    {
        if (!isGrounded)
        {
            ChangeAnimation(PlayerAnimState.JUMP);
        }
    }

    public void TakeDamage()
    {
        soundManager.PlaySoundFX(SoundFX.PLAYERDEATH, Channel.PLAYER_DEATH_FX);
        ShakeCamera();

        currentLifes--;
        if (currentLifes <= 0)
        {
            BulletManager.Instance().DestroyPool();
            //Load End Scene
            StartCoroutine(GameTermination());

        }
        else
        {
            Destroy(l_lifes[currentLifes]);
            l_lifes.RemoveAt(currentLifes);
            deathPlane.ReSpawn(gameObject);
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(groundPoint.position,groundRadius);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage();
        }
        if (collision.gameObject.CompareTag("Hazard"))
        {
            TakeDamage();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Apple"))
        {
            i_AppleGet++;
            soundManager.PlaySoundFX(SoundFX.GETPOINTS, Channel.SCORE_FX);
            Destroy(other.gameObject);
            i_Score += 20;
        }

        if (other.gameObject.CompareTag("EnemyBullet"))
        {
            TakeDamage();
        }

        if (other.gameObject.CompareTag("Goal"))
        {
            won = true;
            soundManager.PlaySoundFX(SoundFX.STAGECLEAR, Channel.SCORE_FX);
            StartCoroutine(GameTermination());
        }

    }

    public IEnumerator GameTermination()
    {
        yield return new WaitForSeconds(1.0f);

        int score = i_Score;
        int secReduce = 0;
        int timeScore = 0;
        if (won == true)
        {
            score += currentLifes * 100;
            secReduce = (i_Sec / 10);
            timeScore = 300 - (i_Min * 60) - secReduce;
        }
        if(timeScore < 0)
        {
            timeScore = 0;
        }
        score += timeScore;

        i_Score = score;

        ScoreSingleton.Instance.Score = score;
        ScoreSingleton.Instance.Min = i_Min;
        ScoreSingleton.Instance.Sec = i_Sec;
        ScoreSingleton.Instance.Kills = i_EnemyKilled;
        ScoreSingleton.Instance.Apple = i_AppleGet;

        BulletManager.Instance().DestroyPool();

        SceneManager.LoadScene(3);

    }
}
