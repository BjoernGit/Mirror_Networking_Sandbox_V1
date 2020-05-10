using UnityEngine;

namespace Mirror.Examples.NetworkRoom
{
    public class PlayerScore : NetworkBehaviour
    {
        [SyncVar]
        public int index;

        [SyncVar]
        public uint score;

        public void CmdIncreaseScore()
        {

            uint points = 1;
            score += points;
        }

        void OnGUI()
        {
            GUI.Box(new Rect(10f + (index * 110), 10f, 100f, 25f), score.ToString().PadLeft(10));
        }
    }
}
