using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] int selectionInd;
    [SerializeField] Image[] inventorySlots;
    [SerializeField] Color deselectedColor;

    void Update ()
    {
        Selection();
    }

    void Selection () 
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            inventorySlots[selectionInd].rectTransform.sizeDelta = new Vector2(90f, 90f);
            inventorySlots[selectionInd].GetComponent<Outline>().effectColor = deselectedColor;

            selectionInd = 0;
            inventorySlots[selectionInd].rectTransform.sizeDelta = new Vector2(125f, 125f);
            inventorySlots[selectionInd].GetComponent<Outline>().effectColor = Color.white;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) 
        {
            inventorySlots[selectionInd].rectTransform.sizeDelta = new Vector2(90f, 90f);
            inventorySlots[selectionInd].GetComponent<Outline>().effectColor = deselectedColor;

            selectionInd = 1;
            inventorySlots[selectionInd].rectTransform.sizeDelta = new Vector2(125f, 125f);
            inventorySlots[selectionInd].GetComponent<Outline>().effectColor = Color.white;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3)) 
        {
            inventorySlots[selectionInd].rectTransform.sizeDelta = new Vector2(90f, 90f);
            inventorySlots[selectionInd].GetComponent<Outline>().effectColor = deselectedColor;

            selectionInd = 2;
            inventorySlots[selectionInd].rectTransform.sizeDelta = new Vector2(125f, 125f);
            inventorySlots[selectionInd].GetComponent<Outline>().effectColor = Color.white;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4)) 
        {
            inventorySlots[selectionInd].rectTransform.sizeDelta = new Vector2(90f, 90f);
            inventorySlots[selectionInd].GetComponent<Outline>().effectColor = deselectedColor;

            selectionInd = 3;
            inventorySlots[selectionInd].rectTransform.sizeDelta = new Vector2(125f, 125f);
            inventorySlots[selectionInd].GetComponent<Outline>().effectColor = Color.white;
        }
    }
}
