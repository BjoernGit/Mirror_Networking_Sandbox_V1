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

        private SyncListUInt playerScores = new SyncListUInt();

        private static Dictionary<string, PlayerScore> playerScoreDict = new Dictionary<string, PlayerScore>();

        public void Start()
        {
            if (!base.isServer)
                return;

            for (int i = 0; i < 10; i++)
            {
                playerScores.Add((uint)i);
            }
        }

        public  void RegisterPlayer(string _netID, PlayerScore _player)
        {
            string _playerID = /*PLAYER_ID_PREFIX + */_netID;
            //CmdDictEntryByServer(_playerID, _player);
        }

        [Command]
        public void CmdDictEntryByServer(string _playerID, PlayerScore _playerScoreVar)
        {
            //playerScoreDict.Add(_playerID, _playerScoreVar);
            //_playerScoreVar.transform.name = _playerID;

        }

        public  void UnRegisterPlayer(string _playerID)
        {
            playerScoreDict.Remove(_playerID);
        }

        public void IncreaseScoreOfPlayer(string _playerID)
        {
            Debug.Log(_playerID);

            foreach (var item in playerScoreDict)
            {
                Debug.Log("dict_item" + item.Key);
            }

            playerScoreDict[_playerID].CmdIncreaseScore();
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