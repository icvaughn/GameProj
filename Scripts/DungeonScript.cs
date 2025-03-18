using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonScript : MonoBehaviour
{
	public static GameObject player;
    GameObject instance;
	public Dungeon dungeon;
    // Start is called before the first frame update
    void Start()
    {
		player = FindObjectOfType<BasicPlayerController>().gameObject;
		dungeon = new Dungeon();
    }

	public void generateDungeonInintial(){
				//create a player object and a dungeon object
	    //player = Instantiate(Resources.Load("Prefabs/Player") as GameObject);
        dungeon = new Dungeon(5);
		//generate a dungeon with x number of rooms
        dungeon.GenerateDungeon(5);
		instance = Instantiate(dungeon.activeRoom.prefab);
		MovePlayerToRoomOrigin();
}

    // Update is called once per frame
    void Update()
    {

        
    }
    public void moveroom( int x, int y)
	{

		//move the player to the new room and destroy the old room
		dungeon.MoveToRoom(x, y);
		Destroy(instance);
		instance = Instantiate(dungeon.activeRoom.prefab);
	}
    public static void MovePlayerToRoomOrigin()
    {
	    if (player)
	    {
		    
		    // Move player to the origin of the new room + y offset
		    player.transform.position = Vector3.zero + new Vector3(0, 2, 0);
	    }
	    else
	    {
		    Debug.LogError("Player GameObject is not assigned.");
	    }
    }
}
