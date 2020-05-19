using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using Mirror.Examples.Bjorn;

namespace Mirror.Examples.NetworkRoom
{
    public class PlayerScore : NetworkBehaviour
    {

        [SyncVar]
        public int index;

        [SyncVar]
        public uint score;

        private AppManager appManager;
        private int[] playerScores = new int[10];
        private string scoresString;

        private void Start()
        {
            appManager = GameObject.Find("AppManager").GetComponent<AppManager>();

            if (isClient)
            {
                //InvokeRepeating("CmdInvokeMe", 1f, 1f);

            }
        }

        private void CmdInvokeMe()
        {
            CmdIncreaseScore();
        }

        [Server]
        public void CmdIncreaseScore()
        {

            uint points = 1;
            score += points;
            //CmdSendScore();
        }

        [Server]
        public void updatePoints(uint score)
        {
            this.score = score;
        }

        //[Server]
        //private void CmdSendScore()
        //{
        //    appManager.CmdWritePlayerScore(index, score);
        //}



        void OnGUI()
        {
            GUI.Box(new Rect(10f + (index * 110), 10f, 100f, 25f), score.ToString().PadLeft(10));
        }

    }
}
