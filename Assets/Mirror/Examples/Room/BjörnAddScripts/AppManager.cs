using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Mirror.Examples.NetworkRoom;

namespace Mirror.Examples.Bjorn
{

    public class AppManager : NetworkBehaviour
    {
        public static AppManager instance;

        void Awake()
        {
            if (instance != null)
            {
                Debug.LogError("More than one AppManager in scene.");
            }
            else
            {
                instance = this;
            }
        }

        private const string PLAYER_ID_PREFIX = "Player ";

        private static Dictionary<uint, uint> playerScoreDict = new Dictionary<uint, uint>();


        public void RegisterPlayer(uint _netID)
        {
            uint _playerID = /*PLAYER_ID_PREFIX + */_netID;
            CmdDictEntryByServer(_playerID);
        }

        [Server]
        public void CmdDictEntryByServer(uint _playerID)
        {
            playerScoreDict.Add(_playerID, 0);
            playerScoreDict[_playerID] = 0;
        }

        public void UnRegisterPlayer(uint _playerID)
        {
            playerScoreDict.Remove(_playerID);
        }

        public void IncreaseScoreOfPlayer(uint _playerID)
        {
            playerScoreDict[_playerID] += 1;
            Debug.Log(_playerID + " has points: " + playerScoreDict[_playerID]);
            NetworkIdentity.spawned[_playerID].gameObject.GetComponent<PlayerScore>().updatePoints(playerScoreDict[_playerID]);
        }

        //public SyncListUInt GetAllPlayerScores()
        //{
        //    return playerScores;
        //}

        //[Server]
        //public void CmdWritePlayerScore(int index, uint value)
        //{
        //    Debug.Log(" index: " + index + " value: " + value);
        //    Debug.Log(playerScores.Count);

        //    playerScores[index] = value;

        //}

    }
}