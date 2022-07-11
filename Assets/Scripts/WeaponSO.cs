using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponItem", menuName = "ScriptableObjects/WeaponItem", order = 1)]
public class WeaponSO : ScriptableObject
{
    public string weaponName;
    public Rigidbody2D proj;
    public float fireRate = 0.25f;
    public float projSpeed = 1500f;
}
