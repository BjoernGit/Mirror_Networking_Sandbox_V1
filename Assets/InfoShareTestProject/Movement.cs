using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Movement : NetworkBehaviour
{
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;
    private Vector3 movement = Vector3.zero;
    private float translation;
    private float rotation;


    // Update is called once per frame
    void Update()
    {
        if (!isClient)
        {
            return;
        }

        float translation = 0f;
        float rotation = 0f;

        if (hasAuthority)
        {
            translation = Input.GetAxis("Vertical") * speed;
            rotation = Input.GetAxis("Horizontal") * rotationSpeed;

            translation *= Time.deltaTime;
            rotation *= Time.deltaTime;

            this.translation = translation;
            this.rotation = rotation;

            CmdMove();
        }

    }

    [Command]
    private void CmdMove()
    {
        RpcMove();
    }

    [ClientRpc]
    private void RpcMove()
    {
        transform.Translate(0, 0, translation);
        transform.Rotate(0, rotation, 0);
    }
}
