using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SpawnShitController : NetworkBehaviour
{
    public GameObject _prefabo;


    [Server]
    public void CmdSpawnShit()
    {
        GameObject GO = Instantiate(_prefabo, transform.position, Quaternion.identity);
        NetworkServer.Spawn(GO);
        //RpcSpawnShit();

    }

    //[ClientRpc]
    //private void RpcSpawnShit()
    //{

    //}
}
