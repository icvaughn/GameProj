using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractalbleObj : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Activate the interactable object
	    public virtual void Activate()
    { 
        Debug.Log("Interactable object activated!");
    }
}
