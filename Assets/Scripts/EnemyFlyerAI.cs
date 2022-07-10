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
    [SerializeField] Material flashMaterial;

    Rigidbody2D rb;
    bool stunned;
    bool inFlash;
    SpriteRenderer[] sprites;

    void Start ()
    {
        target = PlayerInstance.instance.transform;
        rb = GetComponent<Rigidbody2D>();
        sprites = GetComponentsInChildren<SpriteRenderer>();

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

    public void TakeDamage (float dmg) 
    {
        health -= dmg;
        StartCoroutine(Stun(0.25f, 12f));
        StartCoroutine(DamageFlash());

        if (health <= 0f) 
        {
            Destroy(gameObject);
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


    IEnumerator DamageFlash () 
    {
        if (inFlash) yield return null;

        inFlash = true;

        Material[] mats = new Material[sprites.Length];
        for (int i = 0; i < sprites.Length; i++) 
        {
            mats[i] = sprites[i].material;
            sprites[i].material = flashMaterial;
        }

        yield return new WaitForSeconds(0.15f);

        for (int i = 0; i < sprites.Length; i++) 
        {
            sprites[i].material = mats[i];
        }

        inFlash = false;
    }
    
    void Attack () 
    {
        Transform t = Instantiate(attackObj, transform.position, Quaternion.identity) as Transform;

        Vector3 dir = target.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        t.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
        Destroy(t.gameObject, 5f);
    }
}
