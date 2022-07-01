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
            RoomManager.instance.SpawnRoom();
            used = true;
        }
    }
}
