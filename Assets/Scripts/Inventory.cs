using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    [SerializeField] int selectionInd;
    [SerializeField] InventorySlot[] inventorySlots;
    [SerializeField] Color deselectedColor;
    [SerializeField] WeaponSO testWeapon;

    void Awake () => instance = this;

    void Start () 
    {
        //for (int i = 0; i < 4; i++)
            //UpdateSlot(i, testWeapon);
    }

    void Update ()
    {
        Selection();
    }

    void Selection () 
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) 
            ChangeSelection(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) 
            ChangeSelection(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) 
            ChangeSelection(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) 
            ChangeSelection(3);
    }

    void ChangeSelection (int i) 
    {
        if (inventorySlots[i].weapon == null) return;

        inventorySlots[selectionInd].slot.rectTransform.sizeDelta = new Vector2(90f, 90f);
        inventorySlots[selectionInd].slot.GetComponent<Outline>().effectColor = deselectedColor;
        inventorySlots[selectionInd].slot.transform.GetChild(0).GetComponent<Image>().rectTransform.sizeDelta = new Vector2(90f, 90f);

        selectionInd = i;
        inventorySlots[selectionInd].slot.rectTransform.sizeDelta = new Vector2(125f, 125f);
        inventorySlots[selectionInd].slot.GetComponent<Outline>().effectColor = Color.white;
        inventorySlots[selectionInd].slot.transform.GetChild(0).GetComponent<Image>().rectTransform.sizeDelta = new Vector2(125f, 125f);
    }

    public void ChooseSlot (WeaponSO weapon) 
    {
        for (int i = 0; i < 4; i++) 
        {
            if (inventorySlots[i].weapon != null) continue;

            UpdateSlot(i, testWeapon);
            break;
        }
    }

    void UpdateSlot (int i, WeaponSO weapon) 
    {
        inventorySlots[i].weapon = weapon;
        inventorySlots[i].slot.transform.GetChild(0).GetComponent<Image>().sprite = weapon.icon;
    }
}

[System.Serializable]
struct InventorySlot 
{
    public Image slot;
    public WeaponSO weapon;
}