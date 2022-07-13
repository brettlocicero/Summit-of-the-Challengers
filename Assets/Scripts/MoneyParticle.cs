using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyParticle : MonoBehaviour
{
    [SerializeField] Vector2 velocityRange;
    [SerializeField] Vector2 moneyAmt;
    [SerializeField] Sprite moneyIcon;

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
        float n = Mathf.Lerp(0.25f, 1.25f, t);
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
    }

    void OnCollisionEnter2D (Collision2D col) 
    {
        if (col.gameObject.CompareTag("Player")) 
        {
            MoneyManager.instance.UpdateMoney(money);
            DamageNumberManager.instance.SpawnNumbers(money, Color.yellow, transform.position, symbol: moneyIcon);
            Destroy(gameObject);
        }
    }
}
