using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyParticle : MonoBehaviour
{
    [SerializeField] Vector2 velocityRange;
    [SerializeField] Vector2 moneyAmt;

    Rigidbody2D rb;
    int money;

    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();

        float x = Random.Range(velocityRange.x, velocityRange.y);
        float y = Random.Range(velocityRange.x, velocityRange.y);
        rb.velocity = new Vector2(x, y);

        money = Mathf.RoundToInt(Random.Range(moneyAmt.x, moneyAmt.y));
        float t = (money - moneyAmt.x) / (moneyAmt.y - moneyAmt.x);
        float n = Mathf.Lerp(0.5f, 1f, t);
        transform.localScale = new Vector2(n, n);
    }

    void FixedUpdate () 
    {
        Vector2 pos = PlayerInstance.instance.transform.position;
        float dist = Vector2.Distance(transform.position, pos);

        if (dist <= 15f) 
        {
            Vector2 dir = pos - new Vector2(transform.position.x, transform.position.y);
            rb.velocity = dir.normalized * (125f / dist);
        }
        
        else 
        {
            rb.velocity = rb.velocity * 0.99f;
        }
    }

    void OnTriggerEnter2D (Collider2D col) 
    {
        if (col.CompareTag("Player")) 
        {
            MoneyManager.instance.UpdateMoney(money);
            Destroy(gameObject);
        }
    }
}
