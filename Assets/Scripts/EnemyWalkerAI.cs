using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalkerAI : MonoBehaviour
{
    [SerializeField] Transform target;

    [Header("Base Stats")]
    [SerializeField] float maxSpeed;
    [SerializeField] float health = 50f;
    [SerializeField] int moneyOrbSpawns = 3;

    [Header("Attack Stats")]
    [SerializeField] int damage;
    [SerializeField] float attackTime = 2f;
    [SerializeField] float attackDistance = 3f;
    [SerializeField] Transform attackPoint;
    [SerializeField] float attackRadius;
    [SerializeField] LayerMask hitLayer;

    [Header("VFX")]
    [SerializeField] Animator anim;
    [SerializeField] Material flashMaterial;
    [SerializeField] GameObject moneyOrb;
    
    Rigidbody2D rb;
    bool stunned;
    bool inAttack;
    bool inFlash;
    SpriteRenderer[] sprites;

    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        target = PlayerInstance.instance.transform;
        sprites = GetComponentsInChildren<SpriteRenderer>();
    }

    void FixedUpdate ()
    {
        Movement();
    }

    void Movement () 
    {
        if (stunned || inAttack) return;

        // checking for attack
        float dist = Vector2.Distance(transform.position, target.position);
        if (dist <= attackDistance) 
        {
            rb.velocity = Vector2.zero;
            StartCoroutine(Attack());
        }

        // actual movement
        if (target.position.x >= transform.position.x) 
        {
            rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
            transform.localScale = Vector3.one;
        }

        else 
        {
            rb.velocity = new Vector2(-maxSpeed, rb.velocity.y);
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

    IEnumerator Attack () 
    {
        inAttack = true;
        anim.SetTrigger("Attack");

        yield return new WaitForSeconds(attackTime);

        inAttack = false;
    }

    public void AttackHitbox () 
    {
        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, attackRadius, hitLayer);

        if (hit) 
        {
            hit.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
        }
    }

    public void TakeDamage (int dmg) 
    {
        health -= dmg;
        //StartCoroutine(Stun(0.25f, 12f));
        StartCoroutine(DamageFlash());

        if (health <= 0f) 
        {
            //GameController.instance.DecEnemyCount();

            for (int i = 0; i < moneyOrbSpawns; i++)
                Instantiate(moneyOrb, transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f), Quaternion.identity);

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
        if (inFlash) yield break;

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

    void OnDrawGizmosSelected()
    {
        if (!attackPoint) return;

        Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
        Gizmos.DrawSphere(attackPoint.position, attackRadius);
    }
}
