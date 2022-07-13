using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proj : MonoBehaviour
{
    [SerializeField] string contactTag = "Enemy";
    [SerializeField] int dmg = 15;

    void OnTriggerEnter2D (Collider2D col)
    {
        if (col.gameObject.CompareTag(contactTag)) 
        {
            col.SendMessage("TakeDamage", dmg, SendMessageOptions.DontRequireReceiver);
            DamageNumberManager.instance.SpawnNumbers(dmg, Color.red, transform.position);
            Destroy(gameObject);
        }
    }
}
