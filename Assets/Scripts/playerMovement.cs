using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
//using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{
    //Variables for Player Movement
    private float horizontal;
    private float speed = 2.0f;
    public float jumpingPower = 4.0f;
    private bool isFacingRight = true;
    public bool jumping;
    public bool spindash;
    public bool grounded;
    public bool walking;

    //public GameObject sonic;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    //Variables for controls
    //public KeyCode jumpKey = KeyCode.Space;
    public KeyCode downKey = KeyCode.S;
    
    //Variables for animations
    Animator animator;
    public bool slope = false;

    //variables for health, lives, damage
    public bool IsInvincible = false;
    public int lives = 3;
    public TextMeshProUGUI livesText;

    //variables for cheats
    public bool cheat = false;
    public AudioSource buttonSource; //sound for when you click button
    public GameObject cheatHUD;

    //Variables for Collectibles
    public int coinCount;
    public int total = 100;
    public TextMeshProUGUI coinText;

    //Variables for Timer on gametime
    public float timeRemaining;
    public bool timerIsRunning;
    public float minutes;
    public float seconds;
    public TextMeshProUGUI timeText;
    //variables for damage cooldown
    public bool damageCooldown;
    public float cooldownRemaining;
    public float cooldownMinutes;
    public float cooldownSeconds;

    //Variables for Win/Lose
    public bool win = false;
    public bool lose = false;
    //Variables for win/lose UI
    public GameObject winScreen;
    public GameObject loseScreen;

    //Variables for VFX
    public AudioSource backgroundMusic; //bg music
    public AudioSource jumpSource; //sound for when you jump
    public AudioSource winSource; //sound for when you win the game
    public AudioSource loseSource; //sound for when you lose the game
    public AudioSource coinSource; //sound for +coin
    public AudioSource enemyDamageSource; //sound for enemy destroyed
    public AudioSource runSource; //sound for footsteps
    public AudioSource damageSource; //sound for when u are hurt
    [SerializeField] private ParticleSystem damageParticles; //explosion particles
    [SerializeField] private ParticleSystem jumpParticles; //jump dust particles
    [SerializeField] private ParticleSystem coinParticles; //coins lost (player took damage) particles


    // Start is called before the first frame update
    void Start()
    {
        timeRemaining = 180f;
        timerIsRunning = true;
        damageCooldown = false;
        cooldownRemaining = 2f;
        win = false;
        lose = false;

        grounded = false;
        spindash = false;
        walking = false;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //win lose ui
        if (!win) { winScreen.SetActive(false); }
        if(win || lose)
        {
            horizontal = 0;
            jumping = false;
            spindash = false;
        }
        if (!lose) { loseScreen.SetActive(false); }
 
        //detect movement input
        horizontal = Input.GetAxis("Horizontal");
        if(horizontal < 0) { Flip(); }
        if (horizontal > 0) { Flip(); }

        //speed up more you run
        if(horizontal != 0)
        {
            speed = speed + 2*(Time.deltaTime);
            walking = true;
            //runSource.Play();
        }
        if (horizontal == 0)
        {
            walking = false;
            //runSource.Stop();
        }
        animator.SetBool("walking", walking);
        if (horizontal == 0) { speed = 2; } // reset speed when you come to a stop/hit obstacle

        //check if grounded
        if(IsGrounded()) { grounded = true; }
        if(!IsGrounded()) { grounded = false; }

        // jump
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            jumping = true;
            //vfx
            jumpParticles.Play();
            jumpSource.Play();
        }
        if (Input.GetButtonUp("Jump"))
        {
            jumping = false;
        }
        animator.SetBool("jumping", jumping); //jumping animation

        //jump up more you jumping
        if (jumping)
        {
            jumpingPower = jumpingPower + (3 * (Time.deltaTime)); // higher jump while holding space
            if (jumpingPower >= 5.3) { jumping = false; } // height cap for jumping
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower); // jump movement
        }
        if (!jumping && !grounded) // gravity when in air
        {
            jumpingPower = jumpingPower - (.3f * (Time.deltaTime)); // lower jump if you are not holding space
            //animator.SetBool("jumping", false);
        }
        if (!jumping && grounded) // when you reach the ground
        {
            jumpingPower = 4.3f; // reset jump power when on ground
        }

        // spindash
        if(Input.GetKeyDown(downKey))
        {
            spindash = true;
            IsInvincible = true;
        }
        if(Input.GetKeyUp(downKey))
        {
            spindash = false;
            IsInvincible = false;
        }
        animator.SetBool("spindash", spindash); //spindash animation
        if(spindash) { speed = speed + (2.5f)*(Time.deltaTime); } //faster when spindashing

        //timer on game
        if (timeRemaining > 0 && timerIsRunning) //if timer is running
        {
            timeRemaining -= Time.deltaTime;
        }
        if (timeRemaining <= 0 && timerIsRunning) //if time runs out
        {
            loseSource.Play();
            lose = true;
            Debug.Log("Time has run out!");
            timeRemaining = 0;
            GameLost();
            //timerIsRunning = false;
        }

        //timer display in UI
        minutes = Mathf.FloorToInt(timeRemaining / 60); //calculate minutes
        seconds = Mathf.FloorToInt(timeRemaining % 60); //calculate seconds
        timeText.text = string.Format("TIME {0:00}:{1:00}", minutes, seconds);
        coinText.text = string.Format("RINGS {0}", coinCount);
        livesText.text = string.Format("{0}", lives);

        if (timeRemaining == 0)
        {
            timeText.text = ("0:00");
            GameLost();
        }

        //damage cooldown timer
        if(damageCooldown && (cooldownRemaining > 0))
        {
            IsInvincible = true;
            cooldownRemaining -= Time.deltaTime;
        }
        if(damageCooldown && (cooldownRemaining <= 0))
        {
            IsInvincible = false;
            damageCooldown = false;
        }
        if(!damageCooldown)
        {
            cooldownRemaining = 2f;
        }


        //if cheat is on
        if(cheat)
        {
            // physical effects
            //damageCooldown = false;
            IsInvincible = true;

            // visual effects
            // particle system or some sort of color alt ?

            cheatHUD.SetActive(true); // hud screen that states cheats are on
        }
        if(!cheat)
        {
            // physical effects
            //damageCooldown = true;
            IsInvincible = false;

            // visual effects
            // particle system or some sort of color alt ?

            cheatHUD.SetActive(false); // hud screen that states cheats are on
        }
    }

    // FixedUpdate has the same call rate as the physics system
    void FixedUpdate()

    {
        //physics calculation
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip() //for if animations get added
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // collect coin
        if (other.gameObject.CompareTag("Coin"))
        {
            // vfx
            coinSource.Play();

            //remove collectible
            Destroy(other.gameObject);

            // coin counter change
            coinCount++;
        }

        //damage system
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (coinCount > 0 && (!IsInvincible) && (!damageCooldown))
            {
                coinParticles.Play(); // coins throw around
                damageSource.Play(); // sound for player hurt
                damageCooldown = true;
                coinCount = 0; //reset coin value
            }
            if (coinCount == 0 && (!IsInvincible) && (!damageCooldown))
            {
                // sound for player hurt
                if (lives > 0 && coinCount == 0)
                {
                    // play any character death animation
                    lives = lives - 1; // lives decreased
                    //move player position to start
                    transform.position = new Vector3(-29.2f, -58.17f, 1.14f);
                }
                if (lives == 0)
                {
                    GameLost(); //gameover
                }
            }
            if(spindash || ((IsInvincible) && (!damageCooldown)) || cheat)
            {
                enemyDamageSource.Play(); // sound for enemy destroyed
                Destroy(other.gameObject); //destroy enemy
                damageParticles.Play(); // explosion
            }
        }

        if (other.gameObject.CompareTag("End"))
        {
            GameWon();
            timerIsRunning = false;
        }

        if (other.gameObject.CompareTag("Death"))
        {
            if((lives > 0) && (!cheat))
            {
                damageSource.Play();
                //lose a life and reset position
                lives--;
                transform.position = new Vector3(-29.2f, -58.17f, 1.14f);
            }
            if(cheat) { transform.position = new Vector3(-29.2f, -58.17f, 1.14f); } // no penalty if you have cheat on
            if(lives == 0)
            {
                GameLost();
            }
        }
    }
    //for when working on invincibility cheat
    public void CheatOn()
    {
        cheat = !cheat;
        buttonSource.Play();
    }

    //script for win and lose, where you change scene and play sound
    void GameWon()
    {
        //win script
        backgroundMusic.Stop();
        winSource.Play();
        win = true;
        // UI trigger in UI manager
        winScreen.SetActive(true);
    }
    void GameLost()
    {
        //lose script
        backgroundMusic.Stop();
        loseSource.Play();
        lose = true;
        // UI trigger in UI manager
        loseScreen.SetActive(true);
    }

}
