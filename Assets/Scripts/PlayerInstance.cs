using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInstance : MonoBehaviour
{
    public static PlayerInstance instance;
    
    [SerializeField] int health = 6;
    [SerializeField] Animator anim;

    void Awake () => instance = this;

    public void TakeDamage (int dmg = 1) 
    {
        health -= dmg;
        anim.SetTrigger("TakeDamage");
    }
}
