using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static RoomManager instance;

    [Header("Runtime")]
    [SerializeField] int roomCount;
    [SerializeField] int regionIndex;
    [SerializeField] GameObject previousRoom;

    [Header("Settings")]
    [SerializeField] Region[] regions;

    void Awake () => instance = this;

    public void SpawnRoom ()
    {
        Destroy(previousRoom.gameObject);

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        roomCount++;

        Room pickedRoom = regions[regionIndex].rooms[Random.Range(0, regions[regionIndex].rooms.Length)];
        GameObject room = Instantiate(pickedRoom.gameObject, Vector3.zero, Quaternion.identity);
        previousRoom = room;

        PlayerInstance.instance.transform.position = room.GetComponent<Room>().playerSpawnPoint.position;
    }
}

[System.Serializable]
struct Region 
{
    public string regionName;
    public Room[] rooms;
}