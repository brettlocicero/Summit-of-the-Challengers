using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoomManager : MonoBehaviour
{
    public static RoomManager instance;

    [Header("Runtime")]
    [SerializeField] int roomCount;
    [SerializeField] int regionIndex;
    [SerializeField] GameObject previousRoom;

    [Header("")]
    [SerializeField] Region[] regions;

    [Header("References")]
    [SerializeField] TextMeshProUGUI depthText;

    bool spawnedBossRoom;
    bool spawnedShop = true;

    void Awake () => instance = this;

    public void SpawnRoom ()
    {
        // destroy previous room
        Destroy(previousRoom.gameObject);

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // increment room count
        roomCount++;
        depthText.text = "Depth: " + roomCount;

        // if spawnedBossRoom = true, then last room was boss
        // either spawn shop or campfire depending on spawnedShop bool
        if (spawnedBossRoom) 
        {
            // if spawnedShop = false, need to spawn shop now then
            if (!spawnedShop) 
            {
                Room pickedRoom = regions[regionIndex].shopRoom;
                GameObject room = Instantiate(pickedRoom.gameObject, Vector3.zero, Quaternion.identity);
                previousRoom = room;

                PlayerInstance.instance.transform.position = room.GetComponent<Room>().playerSpawnPoint.position;
            }

            // otherwise spawning healing room
            else 
            {
                Room pickedRoom = regions[regionIndex].healingRoom;
                GameObject room = Instantiate(pickedRoom.gameObject, Vector3.zero, Quaternion.identity);
                previousRoom = room;

                PlayerInstance.instance.transform.position = room.GetComponent<Room>().playerSpawnPoint.position;
            }

            spawnedShop = !spawnedShop;
            spawnedBossRoom = false;
        }

        // on boss room if multiple of right number
        else if (roomCount % regions[regionIndex].roomsPerBoss == 0) 
        {
            Room pickedRoom = regions[regionIndex].bossRooms[Random.Range(0, regions[regionIndex].bossRooms.Length)];
            GameObject room = Instantiate(pickedRoom.gameObject, Vector3.zero, Quaternion.identity);
            previousRoom = room;

            PlayerInstance.instance.transform.position = room.GetComponent<Room>().playerSpawnPoint.position;
            spawnedBossRoom = true;
        }
        
        // else normal room
        else 
        {
            Room pickedRoom = regions[regionIndex].rooms[Random.Range(0, regions[regionIndex].rooms.Length)];
            GameObject room = Instantiate(pickedRoom.gameObject, Vector3.zero, Quaternion.identity);
            previousRoom = room;

            PlayerInstance.instance.transform.position = room.GetComponent<Room>().playerSpawnPoint.position;
        }
    }
}

[System.Serializable]
struct Region 
{
    public string regionName;
    public Room[] rooms;
    public Room[] bossRooms;
    public Room shopRoom;
    public Room healingRoom;

    [Header("")]
    public int roomsPerBoss;
}