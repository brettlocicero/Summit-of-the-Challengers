using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] WeaponSO weapon;

    void OnMouseOver () 
    {
        MouseHoverInfo.instance.ShowHoverText(weapon.weaponName, new Vector3(transform.position.x, transform.position.y + 3f, transform.position.z));

        if (Input.GetKeyDown(KeyCode.E)) 
        {
            if (!AuxFunctions.WithinCheckDistance(8f, transform.position, PlayerInstance.instance.transform.position)) return;

            int success = Inventory.instance.ChooseSlot(weapon);
            if (success == 0) 
            {
                Destroy(gameObject);
                MouseHoverInfo.instance.HideHoverText();
            }
        }
    }

    void OnMouseExit () 
    {
        MouseHoverInfo.instance.HideHoverText();
    }
}
