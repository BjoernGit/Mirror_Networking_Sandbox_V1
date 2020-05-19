using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Mirror.Examples.Tanks;

public class ShootProjectilesSynced : NetworkBehaviour
{
    [Header("Firing")]
    public GameObject projectilePrefab;
    public Transform projectileMount;


    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
        {
            if (Input.GetMouseButtonDown(0))
            {
                CmdSpawnProjectile();
            }
        }
    }

    [Command]
    private void CmdSpawnProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, projectileMount.position, transform.rotation);
        projectile.GetComponent<Projectile>().shooterGO = gameObject;
        projectile.GetComponent<Projectile>().shooterID = GetComponent<NetworkIdentity>().netId;
        Debug.Log(GetComponent<NetworkIdentity>().netId);

        NetworkServer.Spawn(projectile);
    }


}
