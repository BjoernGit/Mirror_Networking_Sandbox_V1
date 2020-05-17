using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Mirror.Examples.Bjorn;

public class PlayerSetup : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void OnStartClient()
    {
        if (hasAuthority)
        {
            base.OnStartClient();

            string _netID = GetComponent<NetworkIdentity>().netId.ToString();
            PlayerHealth _player = GetComponent<PlayerHealth>();

            Debug.Log(_player.name);

            ID_Manager.RegisterPlayer(_netID, _player);

        }
    }

    // When we are destroyed
    void OnDisable()
    {
        ID_Manager.UnRegisterPlayer(transform.name);
    }
}
