using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;
using Random = System.Random;

public class Dungeon
{
    private Room[,] rooms;
    public Room activeRoom = new Room("Room_0_0",0,0);
    private int size;
    bool startRoom = true;
    private bool BossRoom = true;
    
    public int rank = 0;
    
    Random random = new Random();

    private static readonly List<int[,]> StaticEntryLayouts = new List<int[,]>
    {
        new int[,] { { 0, 0, 2, 0, 0 }, { 0, 1, 5, 1, 2 }, { 0, 1, 0, 0, 5 } },
        new int[,] { { 5, 0, 0, 3 }, { 1, 2, 0, 1 }, { 0, 4, 1, 5 } }
    };
    
    private static readonly List<int[,]> StaticIntermediateLayouts = new List<int[,]>
    {
        new int[,] { { 0, 0, 2, 0,2,3,5,2,1, 0 }, { 0, 1, 1, 1, 2, 0,0,0,1,2}, { 0, 1, 0, 0, 5,0,0,1,4,2 } },
        new int[,] { { 1, 0, 0, 3,1,1,5 }, { 1, 2, 0, 1,5,1,1 }, { 0, 4, 1, 1,1,1,1 } }
    };
    
    private static readonly List<int[,]> StaticAdvanceLayouts = new List<int[,]>
    {
        new int[,] { { 0, 0, 2, 0,2,3,1 }, { 0, 1, 1, 1, 2, 0,0 }, { 0, 1, 0, 0, 5,1,0 } },
        new int[,] { { 5, 0, 0, 3,1,1 }, { 1, 2, 0, 1,1,1 }, { 0, 4, 1, 1,1,5 } }
    };

       public Dungeon()
        {
            //rooms = new room of start room type
            
            rooms = new Room[1, 1];
            rooms[0, 0] = new StartRoom("Room_0_0");
            activeRoom = rooms[0, 0];
        }
        public Dungeon(int size)
        {
    		 GenerateDungeon(size);
        }

    public void GenerateDungeon(int level)
    {
        // Select a random layout
        int[,]  selectedLayout = StaticEntryLayouts[random.Next(StaticEntryLayouts.Count)];
        //a case where it gets a layout based off of the value of level
        if (level == 1)
        {
            selectedLayout = StaticEntryLayouts[random.Next(StaticEntryLayouts.Count)];
        }
        else if (level == 2)
        {
            selectedLayout = StaticIntermediateLayouts[random.Next(StaticIntermediateLayouts.Count)];
        }
        else if (level == 3)
        {
            selectedLayout = StaticAdvanceLayouts[random.Next(StaticAdvanceLayouts.Count)];
        }
        

        size = selectedLayout.GetLength(0);
        rooms = new Room[selectedLayout.GetLength(0), selectedLayout.GetLength(1)];

        // Populate rooms based on the layout
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < selectedLayout.GetLength(1); j++)
            {
                int roomType = selectedLayout[i, j];
                if (roomType != 0) // 0 represents an empty space
                {
                    string roomName = $"Room_{i}_{j}";
                    rooms[i, j] = CreateRoomByType(roomType, roomName);
                }
            }
        }

        ConnectRooms();
    } 
    private Room CreateRoomByType(int type, string name)
    {


        // Define possible room types for each layout value
   
        var roomOptions = new Dictionary<int, List<Type>>
        {
            { 1, new List<Type> { typeof(TreasureRoom), typeof(Room) } },
            { 2, new List<Type> { typeof(MonsterRoom), typeof(Room) } },
            { 3, new List<Type> { typeof(TrapRoom), typeof(TrapRoom) } },
            { 4, new List<Type> { typeof(TrapRoom), typeof(TreasureRoom) } },
            { 5, new List<Type> {typeof(BossRoom), typeof(StartRoom) } }
        };

        // Check if the type exists in the dictionary
        if (roomOptions.ContainsKey(type))
    {
        var possibleRooms = roomOptions[type];
        Type selectedRoomType = possibleRooms[random.Next(possibleRooms.Count)];
        if (type == 5)
        {
            if (selectedRoomType == typeof(StartRoom) && startRoom)
            {
                startRoom = false;
                selectedRoomType = typeof(StartRoom);
                //sets the active room to the start room during initilization of the new dungeon
                activeRoom = (Room)Activator.CreateInstance(selectedRoomType, name);
            }
            else if ( selectedRoomType == typeof(BossRoom) && BossRoom)
            {
                BossRoom = false;
                selectedRoomType = typeof(BossRoom);
            }
            else
            {
                selectedRoomType = typeof(Room);
            }
        }

        return (Room)Activator.CreateInstance(selectedRoomType, name);
    }

        // Default to null if the type is not in the dictionary
        return null;
    }

    /*
    public void GenerateDungeon(int numRooms)
    {
        rooms = new Room[numRooms, numRooms];
		//generate rooms with random types
        Random random = new Random();
        
        
        
        
        
        
        
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)	
            {
                string roomName = $"Room_{i}_{j}";
                Type roomType =
                    new List<Type> { typeof(TreasureRoom), typeof(MonsterRoom), typeof(TrapRoom) }[random.Next(3)];
                Room room = (Room)Activator.CreateInstance(roomType, roomName);
                rooms[i, j] = room;
            }
        }

        ConnectRooms();
        activeRoom = rooms[0, 0];
        Debug.LogError("Active room Assigned: " + activeRoom.Name);
        PrintRoomLayout();
    }
*/
    private void ConnectRooms()
    {
        Random random = new Random();
		//connect rooms to adgacent rooms by checking if they are adjacent to each other
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                Room room = rooms[i, j];
             
                if (room == null) continue;
  				room.X = i;
                room.Y = j;
                List<Room> neighbors = new List<Room>();

                if (i > 0 && rooms[i - 1, j] != null)
                {
                    neighbors.Add(rooms[i - 1, j]);
                    
                    //Add doorW to the active room 
                    room.activeDoors.Add("DoorW");
                    
                }

                if (i < size - 1 && rooms[i + 1, j] != null)
                {
                    neighbors.Add(rooms[i + 1, j]);
                    //add doorE to the active room 
                    room.activeDoors.Add("DoorE");
                    
                }

                if (j > 0 && rooms[i, j - 1] != null)
                {
                    neighbors.Add(rooms[i, j - 1]);
                    //add doorS to the active room 
                    room.activeDoors.Add("DoorS");
                    
                }

                if (j < size - 1 && rooms[i, j + 1] != null)
                {
                    neighbors.Add(rooms[i, j + 1]);
                    //add doorN to the active room
                    room.activeDoors.Add("DoorN");
                    
                }

                if (neighbors.Count > 0)
                {
                    Room connectedRoom = neighbors[random.Next(neighbors.Count)];
                    room.Connect(connectedRoom);
                }
            }
        }
    }

    public void MoveToRoom(int deltaX, int deltaY)
    {

		//change the active room to the new room
        int newX = activeRoom.X + deltaX;
        int newY = activeRoom.Y + deltaY;

        // Ensure the new coordinates are within the bounds of the dungeon
        if (newX >= 0 && newX < size && newY >= 0 && newY < size && rooms[newX, newY] != null)
        {
            activeRoom = rooms[newX, newY];
			//move the player to the new room
            DungeonScript.MovePlayerToRoomOrigin();
            
            activeRoom.X = newX;
            activeRoom.Y = newY;
            
        }
        else
        {
            Debug.LogError("Invalid room coordinates or room does not exist.");
        }
    }
    public void PrintRoomLayout()
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (rooms[i, j] != null)
                {
                    Debug.Log($"Room at ({i}, {j}): {rooms[i, j].Name}");
                }
                else
                {
                    Debug.Log($"Room at ({i}, {j}): Empty");
                }
            }
        }
    }
}