using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInstance : MonoBehaviour
{
    public static PlayerInstance instance;
    
    [SerializeField] int health = 6;

    void Awake () => instance = this;
}
