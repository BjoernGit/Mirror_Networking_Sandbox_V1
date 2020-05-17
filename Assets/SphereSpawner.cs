using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SphereSpawner : NetworkBehaviour
{
    [SerializeField] private GameObject sphere;

    private float timer = 1f;
    public float repeatTime = 5f;

    // Update is called once per frame
    void Update()
    {
        if (isServer)
        {
            if (timer > 0f)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                SpawnSphere();
                timer = repeatTime;
            }
        }
    }

    void SpawnSphere()
    {
        GameObject GO = Instantiate(sphere, transform.position, transform.rotation);
        NetworkServer.Spawn(GO);

    }
}
