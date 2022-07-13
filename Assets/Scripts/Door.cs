using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    bool used;

    void OnMouseOver ()
    {
        if (Input.GetKeyDown(KeyCode.E) && !used) 
        {
            if (!AuxFunctions.WithinCheckDistance(10f, PlayerInstance.instance.transform.position, transform.position))
                return;

            RoomManager.instance.SpawnRoom();
            used = true;
        }
    }
}
