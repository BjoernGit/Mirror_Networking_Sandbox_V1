using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ID_Manager : MonoBehaviour
{
    public static ID_Manager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one ID_Manager in scene.");
        }
        else
        {
            instance = this;
        }
    }

    private const string PLAYER_ID_PREFIX = "Player";

    private static Dictionary<string, PlayerHealth> players = new Dictionary<string, PlayerHealth>();

    public static void RegisterPlayer (string _netID, PlayerHealth _player)
    {
        string _playerID = PLAYER_ID_PREFIX + _netID;
        players.Add(_playerID, _player);
        _player.transform.name = _playerID;
    }

    public static void UnRegisterPlayer (string _playerID)
    {
        players.Remove(_playerID);
    }

    public static PlayerHealth GetPlayer(string _playerID)
    {
        return players[_playerID];
    }

    //private void OnGUI()
    //{
    //    GUILayout.BeginArea(new Rect(200, 200, 200, 500));
    //    GUILayout.BeginVertical();

    //    foreach (string _playerID in players.Keys)
    //    {
    //        GUILayout.Label(_playerID + " - " + players[_playerID].transform.name);
    //    }

    //    GUILayout.EndVertical();
    //    GUILayout.EndArea();

    //}

}
