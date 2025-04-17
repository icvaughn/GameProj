using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonInteract : InteractalbleObj
{
    public override void Activate()
        {
            DungeonScript dungeon = FindObjectOfType<DungeonScript>();
            RoomScript room = FindObjectOfType<RoomScript>();
            Destroy(room.prefab);
            // increment the dungeon level and generate a new dungeon
            if (dungeon.level <= 3)
            {
                dungeon.level += 1;
                dungeon.generateDungeonInintial();
            }
            else
            {
                // reset the dungeon and opens the starter room
                dungeon = new Dungeon();
            }

            
        }
}
