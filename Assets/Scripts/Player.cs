using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnHolaCountChanged))]
    int holaCount = 0;

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
/* Silly to call this in an update loop, spamming the network
        if(isServer && transform.position.y > 50)
        {
        TooHigh(); // this is called on server but runs on all clients [ClientRPC]
        }
*/
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
holaCount += 1;

        ReplyHola(); // we call via TargetRPC and this will run on the calling client - sends to the original calling client by default
            // but the function can take a conn so the client can be specified
    }

    [ClientRpc]
    void TooHigh()
    {
        Debug.Log("Too High");
    }

    [TargetRpc]
    void ReplyHola()
    {
        Debug.Log("Received Hola from server");
    }

    void OnHolaCountChanged(int oldCount, int newCount)
    {
        Debug.Log("old count was " + oldCount.ToString() + "New count is " + newCount.ToString());
    }

}
