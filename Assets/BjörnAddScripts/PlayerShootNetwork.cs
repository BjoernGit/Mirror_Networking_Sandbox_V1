using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerShootNetwork : NetworkBehaviour
{
    #region Serialized
    [Tooltip("Projectile Prefab")]
    [SerializeField]
    private ProjectileNetwork _projectilePrefab;
    #endregion

    // Update is called once per frame
    void Update()
    {
        ClientUpdate();
    }

    private void ClientUpdate()
    {
        if (!base.isClient)
            return;

        Fire();
    }

    private void Fire()
    {
        if (!base.hasAuthority)
            return;

        if (!Input.GetKeyDown(KeyCode.Mouse0))
            return;

        if (!base.isServer)
        {
            //run this on clients
            ProjectileNetwork p = Instantiate(_projectilePrefab, transform.position + transform.forward, transform.rotation);
            p.Initialize(0f);
        }

        //runs for everyone
        CmdFire(transform.position + transform.forward , NetworkTime.time);
    }

    [Command]
    private void CmdFire(Vector3 position, double networkTime)
    {
        double timePassed = NetworkTime.time - networkTime;

        ProjectileNetwork p = Instantiate(_projectilePrefab, transform.position + transform.forward, transform.rotation);
        p.Initialize((float)timePassed);

        foreach (KeyValuePair<int, NetworkConnectionToClient> item in NetworkServer.connections)
        {
            if (item.Value.connectionId == base.connectionToClient.connectionId)
            {
                continue;
            }
            TargetFire(item.Value, position + transform.forward, networkTime);
        }
    }

    [TargetRpc]
    private void TargetFire(NetworkConnection conn, Vector3 position , double networkTime)
    {
        if (base.isServer)
            return;

        double timePassed = NetworkTime.time - networkTime;

        ProjectileNetwork p = Instantiate(_projectilePrefab, position, transform.rotation);
        p.Initialize((float)timePassed);
    }
}
