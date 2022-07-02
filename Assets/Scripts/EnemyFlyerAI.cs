using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyerAI : MonoBehaviour
{
    [SerializeField] Transform target;

    [SerializeField] float updateDirInterval = 5f;
    [SerializeField] float attackSpeed = 4f;
    [SerializeField] Transform attackObj;
    [SerializeField] float health = 25f;
    [SerializeField] Transform spriteVisuals;

    Rigidbody2D rb;
    bool stunned;

    void Start ()
    {
        target = PlayerInstance.instance.transform;
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdateVelocity", 0f, updateDirInterval);
        InvokeRepeating("Attack", attackSpeed / 2, attackSpeed);
    }

    void FixedUpdate () 
    {
        if (target.position.x >= transform.position.x)
            spriteVisuals.localScale = new Vector3(1f, 1f, 1f);
        else 
            spriteVisuals.localScale = new Vector3(-1f, 1f, 1f);
    }

    void UpdateVelocity ()
    {
        if (stunned) return;

        float dist = Vector2.Distance(transform.position, target.position);

        if (dist >= 40f) 
        {
            // if too far move to player
            rb.velocity = (target.transform.position - transform.position).normalized * 3f;
        }

        else 
        {
            float dx = Random.Range(-5f, 5f);
            float dy = Random.Range(-5f, 5f);

            rb.velocity = new Vector2(dx, dy);
        }
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
    
    void Attack () 
    {
        Transform t = Instantiate(attackObj, transform.position, Quaternion.identity) as Transform;

        Vector3 dir = target.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        t.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
        Destroy(t.gameObject, 5f);
    }

    void TakeDamage (float dmg) 
    {
        health -= dmg;
        StartCoroutine(Stun(1f, 4f));

        if (health <= 0f) 
        {
            //GameController.instance.DecEnemyCount();
            Destroy(gameObject);
        }
    }
}
