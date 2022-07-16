using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] WeaponSO weapon;

    void OnMouseOver () 
    {
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            if (!AuxFunctions.WithinCheckDistance(8f, transform.position, PlayerInstance.instance.transform.position)) return;

            Inventory.instance.ChooseSlot(weapon);
        }
    }
}
