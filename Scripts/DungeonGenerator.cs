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

    public Dungeon()
    {
        //rooms = new room of start room type
        
        rooms = new Room[1, 1];
        rooms[0, 0] = new StartRoom("Room_0_0");
        activeRoom = rooms[0, 0];
    }
    public Dungeon(int size)
    {
		//initialize the size of the dungeon and create a 2D array of rooms
        this.size = size;
        rooms = new Room[size, size];
    }

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