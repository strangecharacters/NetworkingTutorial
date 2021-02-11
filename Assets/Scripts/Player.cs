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
   }
}
