using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    [SerializeField] private Vector3 movement = new Vector3();

    [Client]
    // Update is called once per frame
    void Update()
    {
        if (!hasAuthority) { return; }

        if (!Input.GetKeyDown(KeyCode.Space)) { return; }

        //transform.Translate(movement);

        CmdMove();
    }

    [Command]
    private void CmdMove()
    {
        //validatelogic here

        RpcMove();
    }

    [ClientRpc]
    private void RpcMove()
    {
        transform.Translate(movement);
    }
}
