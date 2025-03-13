using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BasicPlayerController : MonoBehaviour
{
    
    /* 
    Create a variable called 'rb' that will represent the 
    rigid body of this object.
    */
    private Rigidbody rb;

   
    [SerializeField] private float Speed = 5.0f;


    void Start()
    {
    	// make our rb variable equal the rigid body component
        rb = GetComponent<Rigidbody>();
    }
 
    void Update()
    {
   
        var dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //transform.Translate(dir * Time.deltaTime * Speed);
        rb.velocity = dir * Speed;
        
        //get ther players direction and rotate the player to face that direction
        
        if (dir != Vector3.zero)
        {
            transform.forward =  Quaternion.Euler(0, -90, 0) * dir;
        }
      
    
    }
}