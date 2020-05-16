using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace Mirror.Examples.Bjorn
{

    public class AppManager : NetworkBehaviour
    {

        private SyncListUInt playerScores = new SyncListUInt();

        public void Start()
        {
            if (!base.isServer)
                return;

            for (int i = 0; i < 10; i++)
            {
                playerScores.Add((uint)i);
            }
        }

        public SyncListUInt GetAllPlayerScores()
        {
            return playerScores;
        }

        [Server]
        public void CmdWritePlayerScore(int index, uint value)
        {
            Debug.Log(" index: " + index + " value: " + value);
            Debug.Log(playerScores.Count);

            playerScores[index] = value;

        }

    }
}