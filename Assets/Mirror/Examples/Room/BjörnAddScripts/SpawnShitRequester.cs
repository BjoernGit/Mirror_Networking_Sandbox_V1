using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SpawnShitRequester : NetworkBehaviour
{
    GameObject requestTarget;

    private void Start()
    {
        requestTarget = GameObject.Find("SpawnShit");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            CmdSpawnShit();
        }
    }

    [Command]
    public void CmdSpawnShit()
    {
        requestTarget.SendMessage("CmdSpawnShit");
    }
}
