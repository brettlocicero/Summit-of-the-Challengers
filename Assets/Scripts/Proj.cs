using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proj : MonoBehaviour
{
    [SerializeField] string contactTag = "Enemy";
    [SerializeField] float dmg = 15f;

    void OnTriggerEnter2D (Collider2D col)
    {
        if (col.gameObject.CompareTag(contactTag)) 
        {
            col.SendMessage("TakeDamage", dmg, SendMessageOptions.DontRequireReceiver);
            Destroy(gameObject);
        }
    }
}
