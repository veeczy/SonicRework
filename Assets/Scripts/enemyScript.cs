using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour
{

    public int EnemySpeed = 0;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.AddComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //rb.velocity = new Vector2(rb.velocity.x * EnemySpeed, rb.velocity.y);
    }

    void OnTriggerEnter2D(Collider2D enemy)
    {
        if (enemy.tag == "player")
        {
            EnemySpeed = 20;
        }
    }
}
