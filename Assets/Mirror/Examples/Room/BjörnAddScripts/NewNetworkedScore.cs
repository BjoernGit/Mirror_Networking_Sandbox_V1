using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using Mirror.Examples.Bjorn;
using Mirror;

public class NewNetworkedScore : NetworkBehaviour
{
    [SerializeField] private Text scoresText;

    private AppManager appManager;
    private SyncListUInt playerScores = new SyncListUInt();
    private string scoresString;

    private void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            playerScores.Add(0);
        }

        appManager = GameObject.Find("AppManager").GetComponent<AppManager>();
    }


    // Update is called once per frame
    void Update()
    {
        playerScores = appManager.GetAllPlayerScores();

        scoresString = "";
        foreach (uint item in playerScores)
        {
            scoresString += item + " ";
        }
        scoresText.text = scoresString;
    }
}
