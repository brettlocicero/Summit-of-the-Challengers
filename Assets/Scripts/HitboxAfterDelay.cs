using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxAfterDelay : MonoBehaviour
{
    [SerializeField] Transform trueHitbox;
    [SerializeField] float delay = 1f;
    [SerializeField] LayerMask playerLayer;

    void Start ()
    {
        Invoke("Hitbox", delay);
    }

    void Hitbox ()
    {
        Collider2D hit = Physics2D.OverlapBox(trueHitbox.position, trueHitbox.localScale, trueHitbox.rotation.z, playerLayer);
        
        if (hit) 
        {
            PlayerInstance.instance.TakeDamage();
        }
    }
}
