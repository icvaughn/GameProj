using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class RoomScript : MonoBehaviour
{
    public GameObject prefab;
    private int x = 0;

    // Start is called before the first frame update
    void Start()
    {
        //from the dungeon script, find the active room and Activedoors
        DungeonScript dungeon = FindObjectOfType<DungeonScript>();
        Room activeRoom = dungeon.dungeon.activeRoom;

        List<string> activeDoors = activeRoom.activeDoors;
        // go throgh the active doors(string list) and set them to true
        // (this should result in only doors leading to a room to show up in the game)
        Debug.Log($"Ran room script for room {activeRoom.Name}");
        Debug.Log($"active doors: {string.Join(", ", activeDoors)}");
        
        // Ensure the prefab is instantiated
        if (activeRoom.prefab == null)
        {
            Debug.LogError("Active room prefab is not instantiated.");
            return;
        }
        
        foreach (var door in activeDoors)
        {
            Transform doorTransform = activeRoom.prefab.transform.Find(door);
            if (doorTransform != null)
            {
                doorTransform.gameObject.SetActive(true);
                Debug.Log($"{door} is set to active: {doorTransform.gameObject.activeSelf}");
            }
            else
            {
                Debug.LogError($"Door '{door}' not found in the prefab.");
            }
            Debug.Log(doorTransform);
        }

        /*
        foreach (var child in GetChildElements(activeRoom.prefab))
        {

            ChangeMaterial(child, "Texture/" + activeRoom.type + child.name);oor).gameObject.SetActive(t

        }
        
        
        
        /*
        // Instantiate the prefab
        GameObject instance = Instantiate(prefab);

        // Change material for each specified child object
        ChangeChildMaterial(instance, "Floor", "Texture/Floor1");
        ChangeChildMaterial(instance, "Wall1", "Texture/DefaultWallTexture");
        ChangeChildMaterial(instance, "Wall2", "Texture/DefaultWallTexture");
        */
    }

    // Update is called once per frame
    void Update()
    {

    }
    
}

/*
    
    void ChangeMaterial(GameObject parent, string materialPath)
    {
        Transform childTransform = parent.transform.Find(parent.name);

        if (childTransform != null)
        {
            Renderer childRenderer = childTransform.GetComponent<Renderer>();

            // Change material of the child object on a 50 50 chance
            if (Random.Range(0, 2) == 0)
            {
                childRenderer.material = Resources.Load(materialPath, typeof(Material)) as Material;
            }
            else
            {
                childRenderer.material.color = Color.blue;
            }
        }
        else
        {
            Debug.LogError($"Child object '{childName}' not found!");
        }
    }

    static List<GameObject> GetChildElements(GameObject prefab)
    {
        List<GameObject> childElements = new List<GameObject>();

        foreach (Transform child in prefab.transform)
        {
            if (child.gameObject.name[0] == "C")
            {
                childElements.AddRange( GetChildElements(child.gameObject));
                continue;
            }
            childElements.Add(child.gameObject);
        }

        return childElements;
    }
}


*/

