using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class PlayerHealth : NetworkBehaviour
{

    [SyncVar]
    public float health = 100f;

    public Text healthText;

    private void Update()
    {
        healthText.text = health.ToString();
        healthText.transform.parent.LookAt(Camera.main.transform);

        if (isServer)
        {
            if (Random.Range(0f, 1000f) < 2f)
            {
                TakeDamage(1);
            }
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
    }
}
