using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalkerAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float maxSpeed;
    [SerializeField] float health = 50f;
    
    Rigidbody2D rb;
    bool stunned;

    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        target = PlayerInstance.instance.transform;
    }

    void FixedUpdate ()
    {
        Movement();
    }

    void Movement () 
    {
        if (stunned) return;

        if (target.position.x >= transform.position.x)
            rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
        else 
            rb.velocity = new Vector2(-maxSpeed, rb.velocity.y);
    }

    public void TakeDamage (float dmg) 
    {
        health -= dmg;
        StartCoroutine(Stun(1f, 4f));

        if (health <= 0f) 
            Destroy(gameObject);
    }

    IEnumerator Stun (float duration, float stunAmount) 
    {
        stunned = true;
        rb.velocity = Vector2.zero;
        if (target.position.x >= transform.position.x)
            rb.velocity = new Vector2(-stunAmount, rb.velocity.y);
        else 
            rb.velocity = new Vector2(stunAmount, rb.velocity.y);

        yield return new WaitForSeconds(duration);
        stunned = false;
    }
}
