using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInstance : MonoBehaviour
{
    public static PlayerInstance instance;
    
    [SerializeField] int health = 6;
    [SerializeField] int maxHealth = 6;
    [SerializeField] Animator anim;

    [Header("References")]
    [SerializeField] Image[] healthChunks;
    [SerializeField] Image[] lineChunks;

    void Awake () => instance = this;

    public void TakeDamage (int dmg = 1) 
    {
        health -= dmg;
        anim.SetTrigger("TakeDamage");
        SetHealthChunks();
    }

    void SetHealthChunks () 
    {
        for (int i = 0; i < maxHealth; i++) 
        {
            if (i < health) 
            {
                healthChunks[i].color = Color.white;
                lineChunks[i].color = Color.white;
            }

            else 
            {
                healthChunks[i].color = Color.black;
                lineChunks[i].color = Color.black;
            }
        }
    }
}
