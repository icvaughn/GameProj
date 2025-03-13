using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public InteractalbleObj interactableObj;
    
    // Start is called before the first frame update
    void Start()
    {
        interactableObj = null;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player is colliding with an interactable object
        if (interactableObj != null)
        {
            // Check if the player pressed the interact button
            if (Input.GetKeyDown(KeyCode.E))
            {
                // Call the Interact method on the interactable object
                interactableObj.Activate();
            }
        }
        
    }
    // Check if the player is colliding with Nothing
    
    private void OnTriggerExit(Collider other)
    {
        // Check if the other collider is the player
        if (other.CompareTag("Interact"))
        {
            interactableObj = null;
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        // Check if the other collider is the player
        if (other.CompareTag("Interact"))    
        {
            interactableObj = other.GetComponent<InteractalbleObj>();
        }

    }
}
