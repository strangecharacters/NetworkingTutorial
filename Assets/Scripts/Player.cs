using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : NetworkBehaviour
{
   void HandleMovement()
   {
       if(isLocalPlayer)
       {
           float speed = 4;
           float moveHorizontal = Input.GetAxis("Horizontal");
           float moveVertical = Input.GetAxis("Vertical");

           Vector3 movement = new Vector3(moveHorizontal * Time.deltaTime * speed,
                                         moveVertical * Time.deltaTime * speed,
                                         0);
           transform.position = transform.position += movement;
           
       }
   }

   void Update() 
   {
       HandleMovement();
       if(isLocalPlayer && Input.GetKeyDown(KeyCode.X))
       {
           Debug.Log("Sending Hola to the server.");
           Hola(); // call it from the client, nothing will happen on the client though
       }
   }

   [Command]
    void Hola()
    {
        Debug.Log("Received Hola from client"); // this will run on the server when it has been called from the client
    }
}
