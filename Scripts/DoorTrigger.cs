using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;


public class DoorTrigger : MonoBehaviour
{
    private DungeonScript dungeonScript;
    public int XValue;
    public int YValue;

    void Start()
    {
        // Find the DungeonScript component in the scene
        dungeonScript = FindObjectOfType<DungeonScript>();
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the other collider is the player
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player collided with door trigger!");
            // Call the MoveToRoom method
            dungeonScript.moveroom(XValue,YValue);
            //dungeonScript.MovePlayerToRoomOrigin();
        }
    }
}
