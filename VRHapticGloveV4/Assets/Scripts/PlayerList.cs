using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerList : MonoBehaviourPunCallbacks
{
    Player player;
    public void SetUp(Player _player)
    {
        player = _player;
    }
}
