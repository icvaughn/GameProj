using System.Collections.Generic;
using UnityEngine;

public class Room
   { 
       //super class for all rooms
        public string Name { get; set; }
        public GameObject prefab;
        
        //X,Y coordinates of the room in the array of the dungeon
        public int X, Y;
        public List<string> activeDoors;

        public List<Room> Connections { get; set; }

        public Room(string name , int x, int y)
        {
            //initialize the room
            
            Name = name;
            Connections = new List<Room>();
            prefab = Resources.Load("GameProj/Prefabs/Room") as GameObject;
            //X,Y coordinates of the room in the array of the dungeon
            X = x;
            Y = y;
            activeDoors = new List<string>();
        }

        public virtual string Description()
        {
            //return the description of the room (generaly for debugging)
            return "A generic room";
        }

        public void Connect(Room otherRoom)
        {
            if (!Connections.Contains(otherRoom))
            {
                Connections.Add(otherRoom);
                otherRoom.Connect(this);
            }
        }
	
    
protected void ChangeChildMaterial(string childName, string materialPath)
         	{
                //change the material of the assest within the room (currently walls and floor can be expanded to include more)
                //inteded to change based off of dungeon type (forest, ice, etc)
             	            		Transform childTransform = prefab.transform.Find(childName);
            		if (childTransform != null)
            		{
            			Renderer childRenderer = childTransform.GetComponent<Renderer>();
            			childRenderer.material = Resources.Load(materialPath, typeof(Material)) as Material;
            		}
            		else
            		{
            			Debug.LogError($"Child object '{childName}' not found!");
            		}
            	}
}

    public class TreasureRoom : Room
    {
        public TreasureRoom(string name) : base(name, 0, 0)
        {
            prefab = Resources.Load("GameProj/Prefabs/TreasureRoom") as GameObject; 
            
           // ChangeChildMaterial(prefab,  "Texture/Floor1");
        //ChangeChildMaterial(prefab, "Texture/DefaultWallTexture");
        //ChangeChildMaterial(prefab, "Texture/DefaultWallTexture");
            
        }

        public override string Description()
        {
            return $"{Name}: A room filled with treasure!";
        }
    }

    public class MonsterRoom : Room
    {
        public MonsterRoom(string name) : base(name, 0, 0)
        {
            prefab = Resources.Load("GameProj/Prefabs/MonsterRoom") as GameObject;

    	//ChangeChildMaterial(prefab, "Texture/Floor1");
        //ChangeChildMaterial(prefab,  "Texture/DefaultWallTexture");
        //ChangeChildMaterial(prefab,  "Texture/DefaultWallTexture");
        }
        
        public override string Description()
        {
            return $"{Name}: A room with lurking monsters!";
        }
    }

    public class TrapRoom : Room
    {
        public TrapRoom(string name) : base(name, 0, 0)
        {
            prefab = Resources.Load("GameProj/Prefabs/TrapRoom") as GameObject;

     	//ChangeChildMaterial(prefab,  "Texture/Floor1");
        //ChangeChildMaterial(prefab,  "Texture/DefaultWallTexture");
        //ChangeChildMaterial(prefab, "Texture/DefaultWallTexture");
        }

        public override string Description()
        {
            return $"{Name}: A room filled with deadly traps!";
        }

	
    }


    public class StartRoom : Room
    {
        public StartRoom(string name) : base(name, 0, 0)
        {
            prefab = Resources.Load("GameProj/Prefabs/TreasureRoom") as GameObject; 
            
            // ChangeChildMaterial(prefab,  "Texture/Floor1");
            //ChangeChildMaterial(prefab, "Texture/DefaultWallTexture");
            //ChangeChildMaterial(prefab, "Texture/DefaultWallTexture");
            
        }

        public override string Description()
        {
            return $"{Name}: A room you start in!";
        }
    }

    public class BossRoom : Room
    {
        public StartRoom(string name) : base(name, 0, 0)
        {
            prefab = Resources.Load("GameProj/Prefabs/BossRoom") as GameObject; 
            
            // ChangeChildMaterial(prefab,  "Texture/Floor1");
            //ChangeChildMaterial(prefab, "Texture/DefaultWallTexture");
            //ChangeChildMaterial(prefab, "Texture/DefaultWallTexture");
            
        }

        public override string Description()
        {
            return $"{Name}: The Boss Room!";
        }
    }
    