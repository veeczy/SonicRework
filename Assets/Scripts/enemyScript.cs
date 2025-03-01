using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour
{
    //variables for movement
    public float EnemySpeed;
    public int direction = 1;
    public Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform frontCheck;
    [SerializeField] private Transform backCheck;

    [SerializeField] private Transform playerCheck;
    [SerializeField] private LayerMask playerLayer;

    //variables for looks/direction
    public bool isFacingRight;
    public bool blocked;
    // variables for cooldown on flip
    public float timeRemaining;
    public bool timerIsRunning;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        timerIsRunning = false;
        timeRemaining = 1.5f;
        //EnemySpeed = 1.1f;
    }

    // Update is called once per frame
    void Update()
    {
        //check if wall/floor in way
        if ((IsBlockedFront()) || (IsBlockedBack())) { blocked = true; }
        if ((!IsBlockedFront()) && (!IsBlockedBack())) { blocked = false; }

        if((timerIsRunning) && (timeRemaining > 0)) //check if timer is running
        {
            timeRemaining -= Time.deltaTime;
        }
        if(timeRemaining <= 0) //if timer reaches 0
        {
            timerIsRunning = false; //cool down ends
            timeRemaining = 1.5f; //reset timer
        }

        if ((blocked) && (!timerIsRunning))
        {
            Flip(); //change direction if blocked
        }

        if(isFacingRight) { direction = -1; } //move left if facing right
        if(!isFacingRight) { direction = 1; } //move right if facing left

        if (IsPlayerNearby()) { rb.WakeUp(); }
    }

    // FixedUpdate has the same call rate as the physics system
    void FixedUpdate()

    {
        //physics calculation
        rb.velocity = new Vector2(EnemySpeed * rb.velocity.x * direction, rb.velocity.y);
    }
    private void Flip() //for if animations get added
    {
        if (isFacingRight && rb.velocity.x < 0f || !isFacingRight && rb.velocity.x > 0f)
        {
            isFacingRight = !isFacingRight;
            blocked = false;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
            timerIsRunning = true;
        }
    }

    private bool IsBlockedFront()
    {
        return Physics2D.OverlapCircle(frontCheck.position, 0.2f, groundLayer);
    }
    private bool IsBlockedBack()
    {
        return Physics2D.OverlapCircle(backCheck.position, 0.2f, groundLayer);
    }

    private bool IsPlayerNearby()
    {
        return Physics2D.OverlapCircle(playerCheck.position, 4f, playerLayer);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            // test for speeding up when player near
            EnemySpeed = 1.1f;
        }
        if (other.tag == "Ground")
        {
            // make so it doesnt clip through ground

        }
    }
}
